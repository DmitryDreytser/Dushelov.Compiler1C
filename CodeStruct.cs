// Type: Compiler1C.CodeStruct
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using System.Collections;
using System.Windows.Forms;

namespace Compiler1C
{
  public class CodeStruct : TreeNode
  {
    public object Object1C { get; set; }

    public Compiler.TypeObject1C TypeObject1C { get; set; }

    public ArrayList Objects { get; set; }

    public CodeStruct.TypeOperator OperatorType { get; set; }

    public CodeStruct(string Text)
    {
      this.Text = Text;
      this.Objects = new ArrayList();
    }

    public enum TypeOperator
    {
      Неопределен,
      Присваивание,
      Цикл,
      Условие,
      ВызовПроцедуры,
      ВозвратФункции,
    }
  }
}
