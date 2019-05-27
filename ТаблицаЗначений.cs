// Type: Compiler1C.ТаблицаЗначений
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

namespace Compiler1C
{
  internal static class ТаблицаЗначений
  {
    internal static void Code(CodeStruct node, ref string code, Объект1С lst, ref int index)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local1 = @code;
      // ISSUE: explicit reference operation
      string str1 = ^local1 + "((ListView)" + lst.Имя + ")";
      // ISSUE: explicit reference operation
      ^local1 = str1;
      if (index + 1 > node.Objects.Count || !(((Объект1С) node.Objects[index + 1]).Имя == "."))
        return;
      Объект1С объект1С1 = (Объект1С) node.Objects[index + 2];
      if (объект1С1.Имя.ToLower() == ((object) ТаблицаЗначений.Методы.добавить).ToString())
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local2 = @code;
        // ISSUE: explicit reference operation
        string str2 = ^local2 + ".Add";
        // ISSUE: explicit reference operation
        ^local2 = str2;
        int num = 0;
        for (int index1 = index + 3; index1 < node.Objects.Count; ++index1)
        {
          Объект1С объект1С2 = (Объект1С) node.Objects[index1];
          if (объект1С2.Имя == "(")
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local3 = @code;
            // ISSUE: explicit reference operation
            string str3 = ^local3 + "(";
            // ISSUE: explicit reference operation
            ^local3 = str3;
            ++num;
          }
          else
          {
            if (объект1С2.Имя == ")")
              --num;
            if (num > 0)
              объект1С2.Code(node, ref code);
            if (num == 0)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local3 = @code;
              // ISSUE: explicit reference operation
              string str3 = ^local3 + ")";
              // ISSUE: explicit reference operation
              ^local3 = str3;
              index = index1;
              break;
            }
          }
        }
      }
      else
      {
        if (!(объект1С1.Имя.ToLower() == ((object) ТаблицаЗначений.Методы.получить).ToString()))
          return;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local2 = @code;
        // ISSUE: explicit reference operation
        string str2 = ^local2 + "[";
        // ISSUE: explicit reference operation
        ^local2 = str2;
        int num = 0;
        for (int index1 = index + 3; index1 < node.Objects.Count; ++index1)
        {
          Объект1С объект1С2 = (Объект1С) node.Objects[index1];
          if (объект1С2.Имя == "(")
          {
            ++num;
          }
          else
          {
            if (объект1С2.Имя == ")")
              --num;
            if (num > 0)
              объект1С2.Code(node, ref code);
            if (num == 0)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local3 = @code;
              // ISSUE: explicit reference operation
              string str3 = ^local3 + "]";
              // ISSUE: explicit reference operation
              ^local3 = str3;
              index = index1;
              if (index1 + 2 > node.Objects.Count || !(((Объект1С) node.Objects[index1 + 1]).Имя == "."))
                break;
              ТаблицаЗначений.СтрокаТЗ.Code(node, ref code, (Объект1С) null, ref index);
              break;
            }
          }
        }
      }
    }

    private enum Методы
    {
      добавить,
      получить,
    }

    internal static class СтрокаТЗ
    {
      internal static void Code(CodeStruct node, ref string code, Объект1С elem, ref int index)
      {
        if (elem != null)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local = @code;
          // ISSUE: explicit reference operation
          string str = ^local + elem.Имя;
          // ISSUE: explicit reference operation
          ^local = str;
        }
        if (index + 1 > node.Objects.Count || !(((Объект1С) node.Objects[index + 1]).Имя == ".") || !(((Объект1С) node.Objects[index + 2]).Имя.ToLower() == ((object) ТаблицаЗначений.СтрокаТЗ.Свойства.значение).ToString()))
          return;
        index = index + 2;
      }

      private enum Свойства
      {
        значение,
      }
    }
  }
}
