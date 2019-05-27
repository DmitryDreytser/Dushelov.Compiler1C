// Type: Compiler1C.Объекты1С.Условие
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using Compiler1C;
using System;
using System.Collections;

namespace Compiler1C.Объекты1С
{
  internal class Условие
  {
    public ArrayList Conditions { get; set; }

    public int TypeCondition { get; set; }

    public override string ToString()
    {
      if (this.TypeCondition == 2)
        return "Иначе";
      string str = this.TypeCondition == 0 ? "Если " : "ИначеЕсли ";
      foreach (object obj in this.Conditions)
      {
        if (obj is string)
        {
          str = string.Concat(new object[4]
          {
            (object) str,
            (object) " ",
            obj,
            (object) " "
          });
        }
        else
        {
          foreach (Объект1С объект1С in (ArrayList) obj)
            str = str + объект1С.Имя;
        }
      }
      return str + " Тогда";
    }

    internal void Code(CodeStruct node, ref string code)
    {
      for (int index = -1; index < node.Level; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local = @code;
        // ISSUE: explicit reference operation
        string str = ^local + "    ";
        // ISSUE: explicit reference operation
        ^local = str;
      }
      switch (this.TypeCondition)
      {
        case 0:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local1 = @code;
          // ISSUE: explicit reference operation
          string str1 = ^local1 + "if (";
          // ISSUE: explicit reference operation
          ^local1 = str1;
          break;
        case 1:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local2 = @code;
          // ISSUE: explicit reference operation
          string str2 = ^local2 + "else if (";
          // ISSUE: explicit reference operation
          ^local2 = str2;
          break;
        default:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local3 = @code;
          // ISSUE: explicit reference operation
          string str3 = ^local3 + "else ";
          // ISSUE: explicit reference operation
          ^local3 = str3;
          break;
      }
      if (this.Conditions != null)
      {
        foreach (object obj in this.Conditions)
        {
          switch (obj.ToString())
          {
            case "И":
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local4 = @code;
              // ISSUE: explicit reference operation
              string str4 = ^local4 + " && ";
              // ISSUE: explicit reference operation
              ^local4 = str4;
              continue;
            case "ИЛИ":
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local5 = @code;
              // ISSUE: explicit reference operation
              string str5 = ^local5 + " || ";
              // ISSUE: explicit reference operation
              ^local5 = str5;
              continue;
            default:
              IEnumerator enumerator = ((ArrayList) obj).GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  Объект1С объект1С = (Объект1С) enumerator.Current;
                  switch (объект1С.Имя)
                  {
                    case "=":
                      // ISSUE: explicit reference operation
                      // ISSUE: variable of a reference type
                      string& local6 = @code;
                      // ISSUE: explicit reference operation
                      string str6 = ^local6 + "==";
                      // ISSUE: explicit reference operation
                      ^local6 = str6;
                      continue;
                    case "<>":
                      // ISSUE: explicit reference operation
                      // ISSUE: variable of a reference type
                      string& local7 = @code;
                      // ISSUE: explicit reference operation
                      string str7 = ^local7 + "!=";
                      // ISSUE: explicit reference operation
                      ^local7 = str7;
                      continue;
                    default:
                      объект1С.Code(node, ref code);
                      continue;
                  }
                }
                continue;
              }
              finally
              {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                  disposable.Dispose();
              }
          }
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local8 = @code;
        // ISSUE: explicit reference operation
        string str8 = ^local8 + ")";
        // ISSUE: explicit reference operation
        ^local8 = str8;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local9 = @code;
      // ISSUE: explicit reference operation
      string str9 = ^local9 + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local9 = str9;
      for (int index = -1; index < node.Level; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local4 = @code;
        // ISSUE: explicit reference operation
        string str4 = ^local4 + "    ";
        // ISSUE: explicit reference operation
        ^local4 = str4;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local10 = @code;
      // ISSUE: explicit reference operation
      string str10 = ^local10 + "{" + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local10 = str10;
      foreach (CodeStruct node1 in node.Nodes)
        GenerateCode.GenerateBodyCode(node1, ref code);
      for (int index = -1; index < node.Level; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local4 = @code;
        // ISSUE: explicit reference operation
        string str4 = ^local4 + "    ";
        // ISSUE: explicit reference operation
        ^local4 = str4;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local11 = @code;
      // ISSUE: explicit reference operation
      string str11 = ^local11 + "}" + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local11 = str11;
    }
  }
}
