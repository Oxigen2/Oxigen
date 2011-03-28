using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace XmlSerializableSortableGenericList
{
  /// <summary>
  /// A Generic class which works as a Generic List but upon deserialization it sorts its contents
  /// </summary>
  /// <typeparam name="T">A Comparable type whose data to contain in this XmlSortableGenericList</typeparam>
  [Serializable]
  public class XmlSortableGenericList<T> : List<T>, IXmlSerializable where T : IComparable<T>
  {
    public System.Xml.Schema.XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(System.Xml.XmlReader reader)
    {
      XmlSerializer tSerializer = new XmlSerializer(typeof(T));

      bool wasEmpty = reader.IsEmptyElement;

      reader.Read();

      if (wasEmpty)
        return;

      while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
      {
        T t = (T)tSerializer.Deserialize(reader);

        this.Add(t);

        reader.MoveToContent();
      }

      reader.ReadEndElement();

      this.Sort();
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
      XmlSerializer tSerializer = new XmlSerializer(typeof(T));

      foreach (T t in this)
        tSerializer.Serialize(writer, t);
    }
  }
}
