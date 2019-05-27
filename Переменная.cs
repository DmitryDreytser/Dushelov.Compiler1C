// Type: Compiler1C.Переменная
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

namespace Compiler1C
{
  internal class Переменная
  {
    public string Имя { get; set; }

    public Compiler.TypeObject1C TypeObject1C { get; set; }

    public string Text { get; set; }

    public override string ToString()
    {
      return string.Concat(new object[4]
      {
        (object) this.Имя,
        (object) "(",
        (object) this.TypeObject1C,
        (object) ")"
      });
    }
  }
}
