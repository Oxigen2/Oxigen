using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LINQtoDL
{
  /// <summary>
  /// Based on code found at
  /// http://knab.ws/blog/index.php?/archives/3-DL-file-parser-and-writer-in-C-Part-1.html
  /// and
  /// http://knab.ws/blog/index.php?/archives/10-DL-file-parser-and-writer-in-C-Part-2.html
  /// </summary>
  internal class DLStream
  {
    private TextReader _instream;
    private TextWriter _outStream;
    private char _SeparatorChar;
    private char[] _SpecialChars;

    // Current line number in the file. Only used when reading a file, not when writing a file.
    private int _lineNbr;

    /// ///////////////////////////////////////////////////////////////////////
    /// DLStream
    /// 
    public DLStream(TextReader inStream, TextWriter outStream, char SeparatorChar)
    {
      _instream = inStream;
      _outStream = outStream;
      _SeparatorChar = SeparatorChar;
      _SpecialChars = ("\"\x0A\x0D" + _SeparatorChar.ToString()).ToCharArray();
      _lineNbr = 1;
    }

    public void WriteRow(List<string> row, bool quoteAllFields)
    {
      bool firstItem = true;
      foreach (string item in row)
      {
        if (!firstItem) { _outStream.Write(_SeparatorChar); }

        // If the item is null, don't write anything.
        if (item != null)
        {
          // If user always wants quoting, or if the item has special chars
          // (such as "), or if item is the empty string or consists solely of
          // white space, surround the item with quotes.

          if ((quoteAllFields ||
              (item.IndexOfAny(_SpecialChars) > -1) ||
              (item.Trim() == "")))
          {
            _outStream.Write("\"" + item.Replace("\"", "\"\"") + "\"");
          }
          else
          {
            _outStream.Write(item);
          }
        }

        firstItem = false;
      }

      _outStream.WriteLine("");
    }


    /// ///////////////////////////////////////////////////////////////////////
    /// ReadRow
    /// 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="row">
    /// Contains the values in the current row, in the order in which they 
    /// appear in the file.
    /// </param>
    /// <returns>
    /// True if a row was returned in parameter "row".
    /// False if no row returned. In that case, you're at the end of the file.
    /// </returns>
    public bool ReadRow(ref List<DataRowItem> row)
    {
      row.Clear();

      while (true)
      {
        // Number of the line where the item starts. Note that an item
        // can span multiple lines.
        int startingLineNbr = _lineNbr;

        string item = null;

        bool moreAvailable = GetNextItem(ref item);
        if (!moreAvailable)
        {
          return (row.Count > 0);
        }
        row.Add(new DataRowItem(item, startingLineNbr));
      }
    }

    private bool EOS = false;
    private bool EOL = false;
    private bool previousWasCr = false;

    private bool GetNextItem(ref string itemString)
    {
      itemString = null;
      if (EOL)
      {
        // previous item was last in line, start new line
        EOL = false;
        return false;
      }

      bool itemFound = false;
      bool quoted = false;
      bool predata = true;
      bool postdata = false;
      StringBuilder item = new StringBuilder();

      while (true)
      {
        char c = GetNextChar(true);
        if (EOS)
        {
          if (itemFound) { itemString = item.ToString(); }
          return itemFound;
        }

        // ---------
        // Keep track of line number. 
        // Note that line breaks can happen within a quoted field, not just at the
        // end of a record.

        // Don't count 0D0A as two line breaks.
        if ((!previousWasCr) && (c == '\x0A'))
        {
          _lineNbr++;
        }

        if (c == '\x0D')
        {
          _lineNbr++;
          previousWasCr = true;
        }
        else
        {
          previousWasCr = false;
        }

        // ---------

        if ((postdata || !quoted) && c == _SeparatorChar)
        {
          // end of item, return
          if (itemFound) { itemString = item.ToString(); }
          return true;
        }

        if ((predata || postdata || !quoted) && (c == '\x0A' || c == '\x0D'))
        {
          // we are at the end of the line, eat newline characters and exit
          EOL = true;
          if (c == '\x0D' && GetNextChar(false) == '\x0A')
          {
            // new line sequence is 0D0A
            GetNextChar(true);
          }

          if (itemFound) { itemString = item.ToString(); }
          return true;
        }

        if (predata && c == ' ')
          // whitespace preceeding data, discard
          continue;

        if (predata && c == '"')
        {
          // quoted data is starting
          quoted = true;
          predata = false;
          itemFound = true;
          continue;
        }

        if (predata)
        {
          // data is starting without quotes
          predata = false;
          item.Append(c);
          itemFound = true;
          continue;
        }

        if (c == '"' && quoted)
        {
          if (GetNextChar(false) == '"')
          {
            // double quotes within quoted string means add a quote       
            item.Append(GetNextChar(true));
          }
          else
          {
            // end-quote reached
            postdata = true;
          }

          continue;
        }

        // all cases covered, character must be data
        item.Append(c);
      }
    }

    private char[] buffer = new char[4096];
    private int pos = 0;
    private int length = 0;

    private char GetNextChar(bool eat)
    {
      if (pos >= length)
      {
        length = _instream.ReadBlock(buffer, 0, buffer.Length);
        if (length == 0)
        {
          EOS = true;
          return '\0';
        }
        pos = 0;
      }
      if (eat)
        return buffer[pos++];
      else
        return buffer[pos];
    }
  }
}
