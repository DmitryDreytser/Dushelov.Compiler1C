// Type: Compiler1C.Объект1С
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

namespace Compiler1C
{
  internal class Объект1С
  {
    public string Имя { get; set; }

    public object Объект { get; set; }

    public Compiler.TypeObject1C Тип { get; set; }

    public Объект1С(string name)
    {
      this.Имя = name;
    }

    public Объект1С(string name, Compiler.TypeObject1C type)
    {
      this.Имя = name;
      this.Тип = type;
    }

    public override string ToString()
    {
      return string.Concat(new object[4]
      {
        (object) this.Имя,
        (object) " (",
        (object) this.Тип,
        (object) ")"
      });
    }

    internal void Code(CodeStruct node, ref string code)
    {
      if (this.Тип == Compiler.TypeObject1C.Неопределено)
      {
        switch (this.Имя.ToLower())
        {
          case "ложь":
            this.Тип = Compiler.TypeObject1C.Булево;
            this.Имя = "false";
            break;
          case "истина":
            this.Тип = Compiler.TypeObject1C.Булево;
            this.Имя = "true";
            break;
          case "не":
            this.Тип = Compiler.TypeObject1C.НЕ;
            this.Имя = "!";
            break;
          default:
            try
            {
              int.Parse(this.Имя);
              this.Тип = Compiler.TypeObject1C.Число;
              break;
            }
            catch
            {
              this.Тип = Compiler.TypeObject1C.Неопределено;
              break;
            }
        }
        if (Global.perems.ContainsKey((object) this.Имя))
        {
          Переменная переменная = (Переменная) Global.perems[(object) this.Имя];
          if (переменная.TypeObject1C == Compiler.TypeObject1C.Неопределено)
          {
            if (Global.ravno.ContainsKey((object) this.Имя))
            {
              Объект1С объект1С = (Объект1С) Global.ravno[(object) this.Имя];
              переменная.TypeObject1C = объект1С.Тип;
              this.Тип = объект1С.Тип;
              Global.perems[(object) this.Имя] = (object) переменная;
            }
          }
          else
            this.Тип = переменная.TypeObject1C;
        }
        else if (Global.procedures.ContainsKey((object) this.Имя))
        {
          this.Тип = Compiler.TypeObject1C.Процедура;
          this.Объект = Global.procedures[(object) this.Имя];
        }
        else if (this.Имя.StartsWith("\"") && this.Имя.EndsWith("\""))
        {
          this.Тип = Compiler.TypeObject1C.Строка;
        }
        else
        {
          Процедура процедура = (Процедура) Compiler.GetParent(node);
          foreach (string str1 in процедура.Parametrs)
          {
            if (str1 == this.Имя)
            {
              if (Global.callsProc.ContainsKey((object) процедура.Name))
              {
                CodeStruct codeStruct = (CodeStruct) Global.callsProc[(object) процедура.Name];
                bool flag = false;
                int num1 = 0;
                int num2 = 0;
                for (int index = 0; index < codeStruct.Objects.Count; ++index)
                {
                  Объект1С объект1С = (Объект1С) codeStruct.Objects[index];
                  if (объект1С.Имя == процедура.Name)
                    flag = true;
                  else if (flag)
                  {
                    if (объект1С.Имя == "(")
                    {
                      // ISSUE: explicit reference operation
                      // ISSUE: variable of a reference type
                      string& local = @code;
                      // ISSUE: explicit reference operation
                      string str2 = ^local + "(";
                      // ISSUE: explicit reference operation
                      ^local = str2;
                      ++num1;
                    }
                    else
                    {
                      if (объект1С.Имя == ")")
                        --num1;
                      if (num1 > 0)
                      {
                        ++num2;
                        if (процедура.Parametrs.IndexOf((object) str1) + 1 == num2)
                        {
                          switch (объект1С.Тип)
                          {
                            case Compiler.TypeObject1C.Строка:
                              // ISSUE: explicit reference operation
                              // ISSUE: variable of a reference type
                              string& local1 = @code;
                              // ISSUE: explicit reference operation
                              string str2 = ^local1 + "string";
                              // ISSUE: explicit reference operation
                              ^local1 = str2;
                              break;
                            case Compiler.TypeObject1C.Число:
                              // ISSUE: explicit reference operation
                              // ISSUE: variable of a reference type
                              string& local2 = @code;
                              // ISSUE: explicit reference operation
                              string str3 = ^local2 + "int";
                              // ISSUE: explicit reference operation
                              ^local2 = str3;
                              break;
                            default:
                              // ISSUE: explicit reference operation
                              // ISSUE: variable of a reference type
                              string& local3 = @code;
                              // ISSUE: explicit reference operation
                              string str4 = ^local3 + ((object) объект1С.Тип).ToString();
                              // ISSUE: explicit reference operation
                              ^local3 = str4;
                              break;
                          }
                        }
                      }
                      if (num1 == 0)
                      {
                        // ISSUE: explicit reference operation
                        // ISSUE: variable of a reference type
                        string& local = @code;
                        // ISSUE: explicit reference operation
                        string str2 = ^local + ")";
                        // ISSUE: explicit reference operation
                        ^local = str2;
                        break;
                      }
                    }
                  }
                }
                break;
              }
              else
                break;
            }
          }
        }
      }
      switch (this.Тип)
      {
        case Compiler.TypeObject1C.Новый:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local4 = @code;
          // ISSUE: explicit reference operation
          string str5 = ^local4 + " new";
          // ISSUE: explicit reference operation
          ^local4 = str5;
          break;
        case Compiler.TypeObject1C.Строка:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local5 = @code;
          // ISSUE: explicit reference operation
          string str6 = ^local5 + this.Имя;
          // ISSUE: explicit reference operation
          ^local5 = str6;
          break;
        case Compiler.TypeObject1C.Число:
          if (this.Объект != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + " (int)" + this.Имя;
            // ISSUE: explicit reference operation
            ^local1 = str1;
            break;
          }
          else
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + this.Имя;
            // ISSUE: explicit reference operation
            ^local1 = str1;
            break;
          }
        case Compiler.TypeObject1C.Процедура:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local6 = @code;
          // ISSUE: explicit reference operation
          string str7 = ^local6 + " " + this.Имя;
          // ISSUE: explicit reference operation
          ^local6 = str7;
          if (Global.callsProc.ContainsKey((object) this.Имя))
            break;
          Global.callsProc.Add((object) this.Имя, (object) node);
          break;
        case Compiler.TypeObject1C.СписокЗначений:
          if (node.OperatorType == CodeStruct.TypeOperator.Присваивание)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + " ArrayList()";
            // ISSUE: explicit reference operation
            ^local1 = str1;
            break;
          }
          else
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + " ((ArrayList)" + this.Имя + ")";
            // ISSUE: explicit reference operation
            ^local1 = str1;
            break;
          }
        case Compiler.TypeObject1C.ТаблицаЗначений:
          if (node.OperatorType == CodeStruct.TypeOperator.Присваивание)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + " ListView()";
            // ISSUE: explicit reference operation
            ^local1 = str1;
            break;
          }
          else
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + " ((ListView)" + this.Имя + ")";
            // ISSUE: explicit reference operation
            ^local1 = str1;
            break;
          }
        default:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local7 = @code;
          // ISSUE: explicit reference operation
          string str8 = ^local7 + this.Имя;
          // ISSUE: explicit reference operation
          ^local7 = str8;
          break;
      }
    }
  }
}
