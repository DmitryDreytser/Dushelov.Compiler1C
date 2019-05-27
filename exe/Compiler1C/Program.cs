// Type: Compiler1C.Program
// Assembly: Compiler1C, Version=1.0.3287.23284, Culture=neutral, PublicKeyToken=null
// MVID: 4BC048EC-7D96-40FB-B15F-2CEE447B8B96
// Assembly location: C:\Current Version\Compiler1C.exe

using System;

namespace Compiler1C
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Program.About();
      if (args.Length != 2)
      {
        Console.WriteLine("Использовать: Compiler1C.exe <Файл с кодом 1С> <имя выходного exe-файла>");
      }
      else
      {
        Compiler compiler = new Compiler();
        if (!compiler.LoadCode1C(args[0]))
        {
          Console.WriteLine("Ошибка загрузки " + args[0]);
        }
        else
        {
          compiler.ParseCode();
          compiler.CompileCode(args[1], false);
        }
      }
    }

    internal static void About()
    {
      Console.WriteLine("Compiler1C (с) Душелов'2008");
    }
  }
}
