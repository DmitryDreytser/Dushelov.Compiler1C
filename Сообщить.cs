// Type: Compiler1C.Сообщить
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

namespace Compiler1C
{
  internal class Сообщить
  {
    public string ТекстСообщения { get; set; }

    public string Статус { get; set; }

    public override string ToString()
    {
      return "Сообщить(" + this.ТекстСообщения + ");";
    }

    internal static void Code(CodeStruct node, ref string code, Объект1С param, ref int index)
    {
      for (int index1 = -1; index1 <= node.Level; ++index1)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local = @code;
        // ISSUE: explicit reference operation
        string str = ^local + "    ";
        // ISSUE: explicit reference operation
        ^local = str;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local1 = @code;
      // ISSUE: explicit reference operation
      string str1 = ^local1 + "Console.WriteLine(";
      // ISSUE: explicit reference operation
      ^local1 = str1;
      Процедура процедура = (Процедура) Compiler.GetParent(node);
      for (int index1 = index + 1; index1 < node.Objects.Count; ++index1)
      {
        Объект1С elem = (Объект1С) node.Objects[index1];
        if (процедура.Objects.ContainsKey((object) elem.Имя) && процедура.Objects[(object) elem.Имя] != null)
        {
          switch ((Compiler.TypeObject1C) процедура.Objects[(object) elem.Имя])
          {
            case Compiler.TypeObject1C.ЭлементСпискаЗначений:
              СписокЗначений.ЭлементСпискаЗначений.Code(node, ref code, elem, ref index1);
              break;
            case Compiler.TypeObject1C.СтрокаТаблицыЗначений:
              ТаблицаЗначений.СтрокаТЗ.Code(node, ref code, elem, ref index1);
              break;
            default:
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local2 = @code;
              // ISSUE: explicit reference operation
              string str2 = ^local2 + elem.Имя;
              // ISSUE: explicit reference operation
              ^local2 = str2;
              break;
          }
        }
        else
        {
          GenerateCode.FingTypeObject1C(elem);
          elem.Code(node, ref code);
        }
        index = index1;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local3 = @code;
      // ISSUE: explicit reference operation
      string str3 = ^local3 + ")";
      // ISSUE: explicit reference operation
      ^local3 = str3;
    }
  }
}
