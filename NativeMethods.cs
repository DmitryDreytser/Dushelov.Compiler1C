// Type: Compiler1C.NativeMethods
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using System.Runtime.InteropServices;
using System.Text;

namespace Compiler1C
{
  internal class NativeMethods
  {
    private NativeMethods()
    {
    }

    [DllImport("mscoree.dll")]
    private static string GetCORSystemDirectory([MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer, int buflen, ref int numbytes);

    public static string SystemDirectory()
    {
      StringBuilder buffer = new StringBuilder(1024);
      int numbytes = 0;
      NativeMethods.GetCORSystemDirectory(buffer, buffer.Capacity, ref numbytes);
      return ((object) buffer).ToString().Substring(0, numbytes - 1);
    }
  }
}
