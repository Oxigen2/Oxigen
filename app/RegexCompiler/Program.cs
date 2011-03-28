using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexCompiler
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        RegexCompilationInfo[] RE = new RegexCompilationInfo[3] { new RegexCompilationInfo(@"(?:<=|>=|!=|<|>|=)", RegexOptions.Compiled, "EqualityOperators", "OxigenCompiledRegexes", true),
                                                                  new RegexCompilationInfo(@"[a-zA-Z]:\\[a-zA-Z](?:(?:\w|\s|\\)+).[a-zA-Z]+", RegexOptions.Compiled, "PCFileNamePattern", "OxigenCompiledRegexes", true),
                                                                  new RegexCompilationInfo(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b", RegexOptions.Compiled, "EmailPattern", "OxigenCompiledRegexes", true)
        };

        System.Reflection.AssemblyName aName = new System.Reflection.AssemblyName();

        aName.Name = "OxigenCompiledRegexes";

        Regex.CompileToAssembly(RE, aName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}
