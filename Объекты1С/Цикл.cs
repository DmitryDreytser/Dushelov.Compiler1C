// Type: Compiler1C.Объекты1С.Цикл
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using Compiler1C;
using System;

namespace Compiler1C.Объекты1С
{
  internal class Цикл
  {
    public string ObjectCollection { get; set; }

    public string Collection { get; set; }

    public Объект1С ValueBegin { get; set; }

    public Объект1С ValueMax { get; set; }

    public override string ToString()
    {
      string str;
      if (this.Collection != null)
        str = "Для Каждого " + this.ObjectCollection + " Из " + this.Collection + " Цикл";
      else
        str = "Для " + (object) this.ObjectCollection + " = " + (string) (object) this.ValueBegin + " По " + (string) (object) this.ValueMax + " Цикл";
      return str;
    }

    internal void Code(CodeStruct node, ref string code)
    {
      Процедура процедура = (Процедура) Compiler.GetParent(node);
      string str1 = "Array";
      if (this.Collection != null)
      {
        if (Global.perems.ContainsKey((object) this.Collection))
        {
          Переменная переменная = (Переменная) Global.perems[(object) this.Collection];
          if (переменная.TypeObject1C == Compiler.TypeObject1C.Неопределено && Global.ravno.ContainsKey((object) this.Collection))
          {
            Объект1С объект1С = (Объект1С) Global.ravno[(object) this.Collection];
            переменная.TypeObject1C = объект1С.Тип;
            Global.perems[(object) this.Collection] = (object) переменная;
            switch (переменная.TypeObject1C)
            {
              case Compiler.TypeObject1C.СписокЗначений:
                str1 = "ArrayList";
                break;
              case Compiler.TypeObject1C.ТаблицаЗначений:
                str1 = "ListView";
                break;
              default:
                str1 = ((object) объект1С.Тип).ToString();
                break;
            }
          }
        }
        if (процедура.Objects.ContainsKey((object) this.ObjectCollection))
          процедура.Objects[(object) this.ObjectCollection] = (object) Compiler.TypeObject1C.ЭлементСпискаЗначений;
        else
          процедура.Objects.Add((object) this.ObjectCollection, (object) Compiler.TypeObject1C.ЭлементСпискаЗначений);
        for (int index = -1; index <= node.Level; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local = @code;
          // ISSUE: explicit reference operation
          string str2 = ^local + "    ";
          // ISSUE: explicit reference operation
          ^local = str2;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local1 = @code;
        // ISSUE: explicit reference operation
        string str3 = ^local1 + "foreach (var " + this.ObjectCollection + " in ((" + str1 + ")" + this.Collection + "))" + Environment.NewLine;
        // ISSUE: explicit reference operation
        ^local1 = str3;
        for (int index = -1; index <= node.Level; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local2 = @code;
          // ISSUE: explicit reference operation
          string str2 = ^local2 + "    ";
          // ISSUE: explicit reference operation
          ^local2 = str2;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local3 = @code;
        // ISSUE: explicit reference operation
        string str4 = ^local3 + "{" + Environment.NewLine;
        // ISSUE: explicit reference operation
        ^local3 = str4;
        foreach (CodeStruct node1 in node.Nodes)
          GenerateCode.GenerateBodyCode(node1, ref code);
      }
      else
      {
        for (int index = -1; index <= node.Level; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local = @code;
          // ISSUE: explicit reference operation
          string str2 = ^local + "    ";
          // ISSUE: explicit reference operation
          ^local = str2;
        }
        GenerateCode.FingTypeObject1C(this.ValueBegin);
        GenerateCode.FingTypeObject1C(this.ValueMax);
        string str3 = this.ValueBegin.Тип != Compiler.TypeObject1C.Число || this.ValueBegin.Объект != null ? "Int32.Parse(" + this.ValueBegin.Имя + ".ToString())" : this.ValueBegin.Имя;
        string str4 = this.ValueMax.Тип != Compiler.TypeObject1C.Число || this.ValueMax.Объект != null ? "Int32.Parse(" + this.ValueMax.Имя + ".ToString())" : this.ValueMax.Имя;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local1 = @code;
        // ISSUE: explicit reference operation
        string str5 = ^local1 + "for (var " + this.ObjectCollection + " = " + str3 + "; " + this.ObjectCollection + " <= " + str4 + "; " + this.ObjectCollection + "++)" + Environment.NewLine;
        // ISSUE: explicit reference operation
        ^local1 = str5;
        for (int index = -1; index <= node.Level; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local2 = @code;
          // ISSUE: explicit reference operation
          string str2 = ^local2 + "    ";
          // ISSUE: explicit reference operation
          ^local2 = str2;
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local3 = @code;
        // ISSUE: explicit reference operation
        string str6 = ^local3 + "{" + Environment.NewLine;
        // ISSUE: explicit reference operation
        ^local3 = str6;
        foreach (CodeStruct node1 in node.Nodes)
          GenerateCode.GenerateBodyCode(node1, ref code);
      }
      for (int index = -1; index <= node.Level; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local = @code;
        // ISSUE: explicit reference operation
        string str2 = ^local + "    ";
        // ISSUE: explicit reference operation
        ^local = str2;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local4 = @code;
      // ISSUE: explicit reference operation
      string str7 = ^local4 + "}" + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local4 = str7;
    }
  }
}
