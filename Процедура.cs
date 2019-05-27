// Type: Compiler1C.Процедура
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using System;
using System.Collections;

namespace Compiler1C
{
  internal class Процедура
  {
    public string Name { get; set; }

    public ArrayList Parametrs { get; set; }

    public Hashtable Objects { get; set; }

    public Процедура()
    {
      this.Parametrs = new ArrayList();
      this.Objects = new Hashtable();
    }

    internal void Code(CodeStruct node, ref string code)
    {
      code = code + Environment.NewLine + "        private static void " + this.Name + "(";
      for (int index = 0; index < this.Parametrs.Count; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local1 = @code;
        // ISSUE: explicit reference operation
        string str1 = ^local1 + (object) "object " + (string) this.Parametrs[index];
        // ISSUE: explicit reference operation
        ^local1 = str1;
        if (index < this.Parametrs.Count - 1)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local2 = @code;
          // ISSUE: explicit reference operation
          string str2 = ^local2 + ",";
          // ISSUE: explicit reference operation
          ^local2 = str2;
        }
      }
      code = code + ")" + Environment.NewLine + "        {" + Environment.NewLine;
      foreach (CodeStruct node1 in node.Nodes)
        GenerateCode.GenerateBodyCode(node1, ref code);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local = @code;
      // ISSUE: explicit reference operation
      string str = ^local + "        }" + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local = str;
    }
  }
}
