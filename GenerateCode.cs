// Type: Compiler1C.GenerateCode
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using Compiler1C.Объекты1С;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Compiler1C
{
  internal static class GenerateCode
  {
    public static void CompileToExe(string code, string nativeFile, bool showErrors)
    {
      if (File.Exists(nativeFile))
        File.Delete(nativeFile);
      CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider((IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          "CompilerVersion",
          "v3.5"
        }
      });
      CompilerParameters options = new CompilerParameters()
      {
        GenerateExecutable = true,
        OutputAssembly = nativeFile
      };
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        options.ReferencedAssemblies.Add(assembly.Location);
      CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(options, new string[1]
      {
        code
      });
      if (compilerResults.Errors.Count <= 0)
      {
        Console.WriteLine("Выполнено: " + nativeFile);
        if (!showErrors)
          return;
        int num = (int) MessageBox.Show("Выполнено: " + nativeFile);
      }
      else
      {
        string text = "Ошибки компиляции:\n";
        foreach (CompilerError compilerError in (CollectionBase) compilerResults.Errors)
          text = text + (object) compilerError + "\n";
        Console.WriteLine(text);
        if (!showErrors)
          return;
        int num = (int) MessageBox.Show(text);
      }
    }

    internal static string GenerateCSharpCode(TreeView treeCode, ArrayList parametrs)
    {
      string str1 = "using System;" + Environment.NewLine + "using System.Collections;" + Environment.NewLine + "using System.Collections.Generic;" + Environment.NewLine + "using System.Text;" + Environment.NewLine + Environment.NewLine + "namespace Compiler1C" + Environment.NewLine + "{" + Environment.NewLine + "    public class Code1C" + Environment.NewLine + "    {" + Environment.NewLine;
      string str2;
      if (parametrs.Count > 0)
      {
        foreach (Переменная переменная in parametrs)
          str1 = str1 + "        private static object " + переменная.Имя + ";" + Environment.NewLine;
        string str3 = str1 + (object) Environment.NewLine + "        public static void Main(string[] args)" + Environment.NewLine + "        {" + Environment.NewLine + "           if(args.Length != " + (string) (object) parametrs.Count + ")" + Environment.NewLine + "           {" + Environment.NewLine + "               Console.WriteLine(\"Использовать с параметрами: ";
        foreach (Переменная переменная in parametrs)
          str3 = str3 + "<" + переменная.Text + "> ";
        str2 = str3 + "\");" + Environment.NewLine + "           Console.WriteLine(\"\");" + Environment.NewLine + "           Console.WriteLine(\"Для завершения нажмите любую клавишу.\");" + Environment.NewLine + "           Console.ReadLine();" + Environment.NewLine + "               return;" + Environment.NewLine + "           }" + Environment.NewLine + Environment.NewLine;
        int num = 0;
        foreach (Переменная переменная in parametrs)
        {
          string str4 = "args[" + (object) num + "]";
          switch (переменная.TypeObject1C)
          {
            case Compiler.TypeObject1C.Число:
              str4 = "Int32.Parse(args[" + (object) num + "])";
              break;
            case Compiler.TypeObject1C.Булево:
              str4 = "bool.Parse(args[" + (object) num + "])";
              break;
          }
          str2 = str2 + "           " + переменная.Имя + " = " + str4 + ";" + Environment.NewLine;
          ++num;
        }
      }
      else
        str2 = str1 + "        public static void Main()" + Environment.NewLine + "        {" + Environment.NewLine;
      string code = str2 + Environment.NewLine + "           try" + Environment.NewLine + "           {" + Environment.NewLine + "               ПриОткрытии();" + Environment.NewLine + "           }" + Environment.NewLine + "           catch(Exception ex)" + Environment.NewLine + "           {" + Environment.NewLine + "               Console.WriteLine(\"Ошибка выполнения кода: \" + ex.Message);" + Environment.NewLine + "               Console.WriteLine(\"Трассировка: \" + ex.StackTrace);" + Environment.NewLine + "           }" + Environment.NewLine + "           Console.WriteLine(\"\");" + Environment.NewLine + "           Console.WriteLine(\"Для завершения нажмите любую клавишу.\");" + Environment.NewLine + "           Console.ReadLine();" + Environment.NewLine + "        }" + Environment.NewLine;
      foreach (CodeStruct node in treeCode.Nodes)
        GenerateCode.GenerateCSharpCode(node, ref code);
      return code + "    }" + Environment.NewLine + "}";
    }

    private static void GenerateCSharpCode(CodeStruct node, ref string code)
    {
      switch (node.TypeObject1C)
      {
        case Compiler.TypeObject1C.Переменная:
          Переменная переменная = (Переменная) node.Object1C;
          code = code + Environment.NewLine + "        private static object " + переменная.Имя + ";" + Environment.NewLine;
          break;
        case Compiler.TypeObject1C.Процедура:
          ((Процедура) node.Object1C).Code(node, ref code);
          break;
        case Compiler.TypeObject1C.Функция:
          ((Функция) node.Object1C).Code(node, ref code);
          break;
      }
    }

    internal static void GenerateBodyCode(CodeStruct node, ref string code)
    {
      switch (node.OperatorType)
      {
        case CodeStruct.TypeOperator.Присваивание:
          GenerateCode.GenerateПрисваивание(node, ref code);
          break;
        case CodeStruct.TypeOperator.Цикл:
          ((Цикл) node.Object1C).Code(node, ref code);
          break;
        case CodeStruct.TypeOperator.Условие:
          if (node.Object1C != null)
            break;
          IEnumerator enumerator = node.Nodes.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              CodeStruct node1 = (CodeStruct) enumerator.Current;
              Условие условие = (Условие) node1.Object1C;
              if (условие != null)
                условие.Code(node1, ref code);
            }
            break;
          }
          finally
          {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
              disposable.Dispose();
          }
        case CodeStruct.TypeOperator.ВызовПроцедуры:
          GenerateCode.GenerateCallProcedure(node, ref code);
          break;
        case CodeStruct.TypeOperator.ВозвратФункции:
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local = @code;
          // ISSUE: explicit reference operation
          string str = ^local + "               return ";
          // ISSUE: explicit reference operation
          ^local = str;
          GenerateCode.GenerateCallProcedure(node, ref code);
          break;
      }
    }

    private static void GenerateПрисваивание(CodeStruct node, ref string code)
    {
      Процедура процедура = (Процедура) Compiler.GetParent(node);
      int index1;
      for (index1 = 0; index1 < node.Objects.Count; ++index1)
      {
        Объект1С объект1С = (Объект1С) node.Objects[index1];
        if (объект1С.Тип != Compiler.TypeObject1C.Равно)
        {
          for (int index2 = -1; index2 <= node.Level; ++index2)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local = @code;
            // ISSUE: explicit reference operation
            string str = ^local + "    ";
            // ISSUE: explicit reference operation
            ^local = str;
          }
          if (объект1С.Тип == Compiler.TypeObject1C.Неопределено)
          {
            if (процедура.Objects.ContainsKey((object) объект1С.Имя))
            {
              if (процедура.Objects[(object) объект1С.Имя] == null)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                string& local = @code;
                // ISSUE: explicit reference operation
                string str = ^local + "var ";
                // ISSUE: explicit reference operation
                ^local = str;
                GenerateCode.FingTypeObject1C(объект1С);
                процедура.Objects[(object) объект1С.Имя] = (object) объект1С.Тип;
              }
            }
            else
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local = @code;
              // ISSUE: explicit reference operation
              string str = ^local + "var ";
              // ISSUE: explicit reference operation
              ^local = str;
              GenerateCode.FingTypeObject1C(объект1С);
              процедура.Objects.Add((object) объект1С.Имя, (object) объект1С.Тип);
            }
          }
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local1 = @code;
          // ISSUE: explicit reference operation
          string str1 = ^local1 + объект1С.Имя;
          // ISSUE: explicit reference operation
          ^local1 = str1;
        }
        else
          break;
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local2 = @code;
      // ISSUE: explicit reference operation
      string str2 = ^local2 + " = ";
      // ISSUE: explicit reference operation
      ^local2 = str2;
      for (int index2 = index1 + 1; index2 < node.Objects.Count; ++index2)
        ((Объект1С) node.Objects[index2]).Code(node, ref code);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local3 = @code;
      // ISSUE: explicit reference operation
      string str3 = ^local3 + ";" + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local3 = str3;
    }

    internal static void FingTypeObject1C(Объект1С obj)
    {
      if (obj.Тип == Compiler.TypeObject1C.Неопределено)
      {
        switch (obj.Имя.ToLower())
        {
          case "ложь":
            obj.Тип = Compiler.TypeObject1C.Булево;
            obj.Имя = "false";
            break;
          case "истина":
            obj.Тип = Compiler.TypeObject1C.Булево;
            obj.Имя = "true";
            break;
          case "не":
            obj.Тип = Compiler.TypeObject1C.НЕ;
            obj.Имя = "!";
            break;
          default:
            try
            {
              int.Parse(obj.Имя);
              obj.Тип = Compiler.TypeObject1C.Число;
              break;
            }
            catch
            {
              obj.Тип = Compiler.TypeObject1C.Неопределено;
              break;
            }
        }
      }
      if (obj.Тип != Compiler.TypeObject1C.Неопределено)
        return;
      if (Global.perems.ContainsKey((object) obj.Имя))
      {
        Переменная переменная = (Переменная) Global.perems[(object) obj.Имя];
        if (переменная.TypeObject1C == Compiler.TypeObject1C.Неопределено)
        {
          if (!Global.ravno.ContainsKey((object) obj.Имя))
            return;
          Объект1С объект1С = (Объект1С) Global.ravno[(object) obj.Имя];
          переменная.TypeObject1C = объект1С.Тип;
          obj.Тип = объект1С.Тип;
          obj.Объект = (object) объект1С;
          Global.perems[(object) obj.Имя] = (object) переменная;
        }
        else
        {
          obj.Тип = переменная.TypeObject1C;
          obj.Объект = (object) переменная;
        }
      }
      else
      {
        if (!Global.procedures.ContainsKey((object) obj.Имя))
          return;
        obj.Тип = Compiler.TypeObject1C.Процедура;
        obj.Объект = Global.procedures[(object) obj.Имя];
      }
    }

    private static void GenerateCallProcedure(CodeStruct node, ref string code)
    {
      for (int index = 0; index < node.Objects.Count; ++index)
      {
        Объект1С lst = (Объект1С) node.Objects[index];
        GenerateCode.FingTypeObject1C(lst);
        switch (lst.Тип)
        {
          case Compiler.TypeObject1C.Процедура:
            Процедура процедура = (Процедура) lst.Объект;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local1 = @code;
            // ISSUE: explicit reference operation
            string str1 = ^local1 + процедура.Name;
            // ISSUE: explicit reference operation
            ^local1 = str1;
            if (процедура.Parametrs.Count == 0)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              string& local2 = @code;
              // ISSUE: explicit reference operation
              string str2 = ^local2 + "()";
              // ISSUE: explicit reference operation
              ^local2 = str2;
              index += 2;
              break;
            }
            else
              break;
          case Compiler.TypeObject1C.СписокЗначений:
            СписокЗначений.Code(node, ref code, lst, ref index);
            break;
          case Compiler.TypeObject1C.ТаблицаЗначений:
            ТаблицаЗначений.Code(node, ref code, lst, ref index);
            break;
          case Compiler.TypeObject1C.Сообщить:
            Сообщить.Code(node, ref code, lst, ref index);
            break;
          default:
            lst.Code(node, ref code);
            break;
        }
      }
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      string& local = @code;
      // ISSUE: explicit reference operation
      string str = ^local + ";" + Environment.NewLine;
      // ISSUE: explicit reference operation
      ^local = str;
    }
  }
}
