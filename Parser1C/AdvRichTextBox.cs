// Type: Parser1C.AdvRichTextBox
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Parser1C
{
  public class AdvRichTextBox : RichTextBox
  {
    public const uint CFE_AUTOCOLOR = 1073741824U;
    public const uint CFE_BOLD = 1U;
    public const uint CFE_ITALIC = 2U;
    public const uint CFE_LINK = 32U;
    public const uint CFE_PROTECTED = 16U;
    public const uint CFE_STRIKEOUT = 8U;
    public const uint CFE_SUBSCRIPT = 65536U;
    public const uint CFE_SUPERSCRIPT = 131072U;
    public const uint CFE_UNDERLINE = 4U;
    public const int CFM_ALLCAPS = 128;
    public const int CFM_ANIMATION = 262144;
    public const int CFM_BACKCOLOR = 67108864;
    public const uint CFM_BOLD = 1U;
    public const uint CFM_CHARSET = 134217728U;
    public const uint CFM_COLOR = 1073741824U;
    public const int CFM_DISABLED = 8192;
    public const int CFM_EMBOSS = 2048;
    public const uint CFM_FACE = 536870912U;
    public const int CFM_HIDDEN = 256;
    public const int CFM_IMPRINT = 4096;
    public const uint CFM_ITALIC = 2U;
    public const int CFM_KERNING = 1048576;
    public const int CFM_LCID = 33554432;
    public const uint CFM_LINK = 32U;
    public const uint CFM_OFFSET = 268435456U;
    public const int CFM_OUTLINE = 512;
    public const uint CFM_PROTECTED = 16U;
    public const int CFM_REVAUTHOR = 32768;
    public const int CFM_REVISED = 16384;
    public const int CFM_SHADOW = 1024;
    public const uint CFM_SIZE = 2147483648U;
    public const int CFM_SMALLCAPS = 64;
    public const int CFM_SPACING = 2097152;
    public const uint CFM_STRIKEOUT = 8U;
    public const int CFM_STYLE = 524288;
    public const uint CFM_SUBSCRIPT = 196608U;
    public const uint CFM_SUPERSCRIPT = 196608U;
    public const uint CFM_UNDERLINE = 4U;
    public const int CFM_UNDERLINETYPE = 8388608;
    public const int CFM_WEIGHT = 4194304;
    public const byte CFU_UNDERLINE = (byte) 1;
    public const byte CFU_UNDERLINEDASH = (byte) 5;
    public const byte CFU_UNDERLINEDASHDOT = (byte) 6;
    public const byte CFU_UNDERLINEDASHDOTDOT = (byte) 7;
    public const byte CFU_UNDERLINEDOTTED = (byte) 4;
    public const byte CFU_UNDERLINEDOUBLE = (byte) 3;
    public const byte CFU_UNDERLINEHAIRLINE = (byte) 10;
    public const byte CFU_UNDERLINENONE = (byte) 0;
    public const byte CFU_UNDERLINETHICK = (byte) 9;
    public const byte CFU_UNDERLINEWAVE = (byte) 8;
    public const byte CFU_UNDERLINEWORD = (byte) 2;
    private const int EM_FORMATRANGE = 1081;
    private const int EM_GETCHARFORMAT = 1082;
    private const int EM_GETPARAFORMAT = 1085;
    private const int EM_SETCHARFORMAT = 1092;
    private const int EM_SETEVENTMASK = 1073;
    private const int EM_SETPARAFORMAT = 1095;
    private const int EM_SETTYPOGRAPHYOPTIONS = 1226;
    public const short FW_BLACK = (short) 900;
    public const short FW_BOLD = (short) 700;
    public const short FW_DEMIBOLD = (short) 600;
    public const short FW_DONTCARE = (short) 0;
    public const short FW_EXTRABOLD = (short) 800;
    public const short FW_EXTRALIGHT = (short) 200;
    public const short FW_HEAVY = (short) 900;
    public const short FW_LIGHT = (short) 300;
    public const short FW_MEDIUM = (short) 500;
    public const short FW_NORMAL = (short) 400;
    public const short FW_REGULAR = (short) 400;
    public const short FW_SEMIBOLD = (short) 600;
    public const short FW_THIN = (short) 100;
    public const short FW_ULTRABOLD = (short) 800;
    public const short FW_ULTRALIGHT = (short) 200;
    public const int LF_FACESIZE = 32;
    public const ushort PFA_CENTER = (ushort) 3;
    public const ushort PFA_LEFT = (ushort) 1;
    public const ushort PFA_RIGHT = (ushort) 2;
    public const uint PFM_ALIGNMENT = 8U;
    public const uint PFM_NUMBERING = 32U;
    public const uint PFM_OFFSET = 4U;
    public const uint PFM_OFFSETINDENT = 2147483648U;
    public const uint PFM_RIGHTINDENT = 2U;
    public const uint PFM_STARTINDENT = 1U;
    public const uint PFM_TABSTOPS = 16U;
    public const ushort PFN_BULLET = (ushort) 1;
    private const int SCF_ALL = 4;
    private const int SCF_SELECTION = 1;
    private const int SCF_WORD = 2;
    private const int TO_ADVANCEDTYPOGRAPHY = 1;
    private const int WM_SETREDRAW = 11;
    private const int WM_USER = 1024;
    private int oldEventMask;
    private int updating;

    public bool InternalUpdating
    {
      get
      {
        return this.updating != 0;
      }
    }

    public AdvRichTextBox.PARAFORMAT ParaFormat
    {
      get
      {
        AdvRichTextBox.PARAFORMAT lp = new AdvRichTextBox.PARAFORMAT();
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1085, 1, ref lp);
        return lp;
      }
      set
      {
        AdvRichTextBox.PARAFORMAT lp = value;
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1095, 1, ref lp);
      }
    }

    public AdvRichTextBox.PARAFORMAT DefaultParaFormat
    {
      get
      {
        AdvRichTextBox.PARAFORMAT lp = new AdvRichTextBox.PARAFORMAT();
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1085, 4, ref lp);
        return lp;
      }
      set
      {
        AdvRichTextBox.PARAFORMAT lp = value;
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1095, 4, ref lp);
      }
    }

    public AdvRichTextBox.CHARFORMAT CharFormat
    {
      get
      {
        AdvRichTextBox.CHARFORMAT lp = new AdvRichTextBox.CHARFORMAT();
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1082, 1, ref lp);
        return lp;
      }
      set
      {
        AdvRichTextBox.CHARFORMAT lp = value;
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1092, 1, ref lp);
      }
    }

    public AdvRichTextBox.CHARFORMAT DefaultCharFormat
    {
      get
      {
        AdvRichTextBox.CHARFORMAT lp = new AdvRichTextBox.CHARFORMAT();
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1082, 4, ref lp);
        return lp;
      }
      set
      {
        AdvRichTextBox.CHARFORMAT lp = value;
        lp.cbSize = Marshal.SizeOf((object) lp);
        AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1092, 4, ref lp);
      }
    }

    private Color GetColor(int crColor)
    {
      return Color.FromArgb((int) (byte) crColor, (int) (byte) (crColor >> 8), (int) (byte) (crColor >> 16));
    }

    private int GetCOLORREF(int r, int g, int b)
    {
      return r | g << 8 | b << 16;
    }

    private int GetCOLORREF(Color color)
    {
      return this.GetCOLORREF((int) color.R, (int) color.G, (int) color.B);
    }

    public string GetHTML(bool bHTML, bool bParaFormat)
    {
      AdvRichTextBox.ctformatStates ctformatStates1 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates2 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates3 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates4 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates5 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates6 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates7 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates8 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates9 = AdvRichTextBox.ctformatStates.nctNone;
      AdvRichTextBox.ctformatStates ctformatStates10 = AdvRichTextBox.ctformatStates.nctNone;
      string str1 = "";
      int crColor = 0;
      Color color1 = new Color();
      int num1 = 0;
      ArrayList arrayList = new ArrayList();
      string str2 = "";
      this.HideSelection = true;
      this.BeginUpdate();
      int selectionStart = this.SelectionStart;
      int selectionLength = this.SelectionLength;
      try
      {
        AdvRichTextBox.cMyREFormat cMyReFormat1;
        if (bHTML)
        {
          char[] chArray = new char[5]
          {
            '&',
            '<',
            '>',
            '"',
            '\''
          };
          string[] strArray = new string[5]
          {
            "&amp;",
            "&lt;",
            "&gt;",
            "&quot;",
            "&apos;"
          };
          for (int index1 = 0; index1 < chArray.Length; ++index1)
          {
            char[] characterSet = new char[1]
            {
              chArray[index1]
            };
            for (int index2 = this.Find(characterSet, 0); index2 != -1; index2 = this.Find(characterSet, index2 + 1))
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = index2;
              cMyReFormat1.nLen = 1;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_ENTITY;
              cMyReFormat1.strValue = strArray[index1];
              arrayList.Add((object) cMyReFormat1);
            }
          }
        }
        string str3 = "";
        int textLength = this.TextLength;
        char[] chArray1 = new char[2]
        {
          ' ',
          char.MinValue
        };
        int start;
        for (start = 0; start < textLength; ++start)
        {
          this.Select(start, 1);
          string selectedText = this.SelectedText;
          if (bHTML)
          {
            AdvRichTextBox.CHARFORMAT charFormat = this.CharFormat;
            AdvRichTextBox.PARAFORMAT paraFormat = this.ParaFormat;
            string str4 = new string(charFormat.szFaceName).Trim(chArray1);
            if (str1 != str4 || crColor != charFormat.crTextColor || num1 != charFormat.yHeight)
            {
              if (str1 != "")
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</font>";
                arrayList.Add((object) cMyReFormat1);
              }
              str1 = str4;
              crColor = charFormat.crTextColor;
              num1 = charFormat.yHeight;
              int num2 = num1 / 100 + 1;
              Color color2 = this.GetColor(crColor);
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              string str5 = "#" + (color2.ToArgb() & 16777215).ToString("X6");
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<font face=\"" + (object) str1 + "\" color=\"" + str5 + "\" size=\"" + (string) (object) num2 + "\">";
              arrayList.Add((object) cMyReFormat1);
            }
            if (selectedText == "\r" || selectedText == "\n")
            {
              if (bParaFormat)
              {
                ctformatStates10 = AdvRichTextBox.ctformatStates.nctNone;
                ctformatStates8 = AdvRichTextBox.ctformatStates.nctNone;
                ctformatStates9 = AdvRichTextBox.ctformatStates.nctNone;
                ctformatStates7 = AdvRichTextBox.ctformatStates.nctNone;
              }
              if (ctformatStates2 != AdvRichTextBox.ctformatStates.nctNone)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</i>";
                arrayList.Add((object) cMyReFormat1);
                ctformatStates2 = AdvRichTextBox.ctformatStates.nctNone;
              }
              if (ctformatStates1 != AdvRichTextBox.ctformatStates.nctNone)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</b>";
                arrayList.Add((object) cMyReFormat1);
                ctformatStates1 = AdvRichTextBox.ctformatStates.nctNone;
              }
              if (ctformatStates4 != AdvRichTextBox.ctformatStates.nctNone)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</u>";
                arrayList.Add((object) cMyReFormat1);
                ctformatStates4 = AdvRichTextBox.ctformatStates.nctNone;
              }
              if (ctformatStates3 != AdvRichTextBox.ctformatStates.nctNone)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</s>";
                arrayList.Add((object) cMyReFormat1);
                ctformatStates3 = AdvRichTextBox.ctformatStates.nctNone;
              }
              if (ctformatStates5 != AdvRichTextBox.ctformatStates.nctNone)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</sup>";
                arrayList.Add((object) cMyReFormat1);
                ctformatStates5 = AdvRichTextBox.ctformatStates.nctNone;
              }
              if (ctformatStates6 != AdvRichTextBox.ctformatStates.nctNone)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "</sub>";
                arrayList.Add((object) cMyReFormat1);
                ctformatStates6 = AdvRichTextBox.ctformatStates.nctNone;
              }
            }
            if (bParaFormat)
            {
              if ((int) paraFormat.wAlignment == 3)
                ctformatStates7 = ctformatStates7 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
              else if (ctformatStates7 != AdvRichTextBox.ctformatStates.nctNone)
                ctformatStates7 = AdvRichTextBox.ctformatStates.nctReset;
              if (ctformatStates7 == AdvRichTextBox.ctformatStates.nctNew)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "<p align=\"center\">";
                arrayList.Add((object) cMyReFormat1);
              }
              else if (ctformatStates7 == AdvRichTextBox.ctformatStates.nctReset)
                ctformatStates7 = AdvRichTextBox.ctformatStates.nctNone;
              if ((int) paraFormat.wAlignment == 1)
                ctformatStates8 = ctformatStates8 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
              else if (ctformatStates8 != AdvRichTextBox.ctformatStates.nctNone)
                ctformatStates8 = AdvRichTextBox.ctformatStates.nctReset;
              if (ctformatStates8 == AdvRichTextBox.ctformatStates.nctNew)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "<p align=\"left\">";
                arrayList.Add((object) cMyReFormat1);
              }
              else if (ctformatStates8 == AdvRichTextBox.ctformatStates.nctReset)
                ctformatStates8 = AdvRichTextBox.ctformatStates.nctNone;
              if ((int) paraFormat.wAlignment == 2)
                ctformatStates9 = ctformatStates9 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
              else if (ctformatStates9 != AdvRichTextBox.ctformatStates.nctNone)
                ctformatStates9 = AdvRichTextBox.ctformatStates.nctReset;
              if (ctformatStates9 == AdvRichTextBox.ctformatStates.nctNew)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "<p align=\"right\">";
                arrayList.Add((object) cMyReFormat1);
              }
              else if (ctformatStates9 == AdvRichTextBox.ctformatStates.nctReset)
                ctformatStates9 = AdvRichTextBox.ctformatStates.nctNone;
              if ((int) paraFormat.wNumbering == 1)
                ctformatStates10 = ctformatStates10 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
              else if (ctformatStates10 != AdvRichTextBox.ctformatStates.nctNone)
                ctformatStates10 = AdvRichTextBox.ctformatStates.nctReset;
              if (ctformatStates10 == AdvRichTextBox.ctformatStates.nctNew)
              {
                cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
                cMyReFormat1.nPos = start;
                cMyReFormat1.nLen = 0;
                cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
                cMyReFormat1.strValue = "<li>";
                arrayList.Add((object) cMyReFormat1);
              }
              else if (ctformatStates10 == AdvRichTextBox.ctformatStates.nctReset)
                ctformatStates10 = AdvRichTextBox.ctformatStates.nctNone;
            }
            if (((int) charFormat.dwEffects & 1) == 1)
              ctformatStates1 = ctformatStates1 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
            else if (ctformatStates1 != AdvRichTextBox.ctformatStates.nctNone)
              ctformatStates1 = AdvRichTextBox.ctformatStates.nctReset;
            if (ctformatStates1 == AdvRichTextBox.ctformatStates.nctNew)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<b>";
              arrayList.Add((object) cMyReFormat1);
            }
            else if (ctformatStates1 == AdvRichTextBox.ctformatStates.nctReset)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "</b>";
              arrayList.Add((object) cMyReFormat1);
              ctformatStates1 = AdvRichTextBox.ctformatStates.nctNone;
            }
            if (((int) charFormat.dwEffects & 2) == 2)
              ctformatStates2 = ctformatStates2 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
            else if (ctformatStates2 != AdvRichTextBox.ctformatStates.nctNone)
              ctformatStates2 = AdvRichTextBox.ctformatStates.nctReset;
            if (ctformatStates2 == AdvRichTextBox.ctformatStates.nctNew)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<i>";
              arrayList.Add((object) cMyReFormat1);
            }
            else if (ctformatStates2 == AdvRichTextBox.ctformatStates.nctReset)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "</i>";
              arrayList.Add((object) cMyReFormat1);
              ctformatStates2 = AdvRichTextBox.ctformatStates.nctNone;
            }
            if (((int) charFormat.dwEffects & 8) == 8)
              ctformatStates3 = ctformatStates3 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
            else if (ctformatStates3 != AdvRichTextBox.ctformatStates.nctNone)
              ctformatStates3 = AdvRichTextBox.ctformatStates.nctReset;
            if (ctformatStates3 == AdvRichTextBox.ctformatStates.nctNew)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<s>";
              arrayList.Add((object) cMyReFormat1);
            }
            else if (ctformatStates3 == AdvRichTextBox.ctformatStates.nctReset)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "</s>";
              arrayList.Add((object) cMyReFormat1);
              ctformatStates3 = AdvRichTextBox.ctformatStates.nctNone;
            }
            if (((int) charFormat.dwEffects & 4) == 4)
              ctformatStates4 = ctformatStates4 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
            else if (ctformatStates4 != AdvRichTextBox.ctformatStates.nctNone)
              ctformatStates4 = AdvRichTextBox.ctformatStates.nctReset;
            if (ctformatStates4 == AdvRichTextBox.ctformatStates.nctNew)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<u>";
              arrayList.Add((object) cMyReFormat1);
            }
            else if (ctformatStates4 == AdvRichTextBox.ctformatStates.nctReset)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "</u>";
              arrayList.Add((object) cMyReFormat1);
              ctformatStates4 = AdvRichTextBox.ctformatStates.nctNone;
            }
            if (((int) charFormat.dwEffects & 131072) == 131072)
              ctformatStates5 = ctformatStates5 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
            else if (ctformatStates5 != AdvRichTextBox.ctformatStates.nctNone)
              ctformatStates5 = AdvRichTextBox.ctformatStates.nctReset;
            if (ctformatStates5 == AdvRichTextBox.ctformatStates.nctNew)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<sup>";
              arrayList.Add((object) cMyReFormat1);
            }
            else if (ctformatStates5 == AdvRichTextBox.ctformatStates.nctReset)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "</sup>";
              arrayList.Add((object) cMyReFormat1);
              ctformatStates5 = AdvRichTextBox.ctformatStates.nctNone;
            }
            if (((int) charFormat.dwEffects & 65536) == 65536)
              ctformatStates6 = ctformatStates6 != AdvRichTextBox.ctformatStates.nctNone ? AdvRichTextBox.ctformatStates.nctContinue : AdvRichTextBox.ctformatStates.nctNew;
            else if (ctformatStates6 != AdvRichTextBox.ctformatStates.nctNone)
              ctformatStates6 = AdvRichTextBox.ctformatStates.nctReset;
            if (ctformatStates6 == AdvRichTextBox.ctformatStates.nctNew)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "<sub>";
              arrayList.Add((object) cMyReFormat1);
            }
            else if (ctformatStates6 == AdvRichTextBox.ctformatStates.nctReset)
            {
              cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
              cMyReFormat1.nPos = start;
              cMyReFormat1.nLen = 0;
              cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
              cMyReFormat1.strValue = "</sub>";
              arrayList.Add((object) cMyReFormat1);
              ctformatStates6 = AdvRichTextBox.ctformatStates.nctNone;
            }
          }
          str3 = str3 + selectedText;
        }
        if (bHTML)
        {
          if (ctformatStates1 != AdvRichTextBox.ctformatStates.nctNone)
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</b>";
            arrayList.Add((object) cMyReFormat1);
          }
          if (ctformatStates2 != AdvRichTextBox.ctformatStates.nctNone)
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</i>";
            arrayList.Add((object) cMyReFormat1);
          }
          if (ctformatStates3 != AdvRichTextBox.ctformatStates.nctNone)
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</s>";
            arrayList.Add((object) cMyReFormat1);
          }
          if (ctformatStates4 != AdvRichTextBox.ctformatStates.nctNone)
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</u>";
            arrayList.Add((object) cMyReFormat1);
          }
          if (ctformatStates5 != AdvRichTextBox.ctformatStates.nctNone)
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</sup>";
            arrayList.Add((object) cMyReFormat1);
          }
          if (ctformatStates6 != AdvRichTextBox.ctformatStates.nctNone)
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</sub>";
            arrayList.Add((object) cMyReFormat1);
          }
          if (str1 != "")
          {
            cMyReFormat1 = new AdvRichTextBox.cMyREFormat();
            cMyReFormat1.nPos = start;
            cMyReFormat1.nLen = 0;
            cMyReFormat1.nType = AdvRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
            cMyReFormat1.strValue = "</font>";
            arrayList.Add((object) cMyReFormat1);
          }
        }
        int count = arrayList.Count;
        for (int index1 = 0; index1 < count - 1; ++index1)
        {
          for (int index2 = index1 + 1; index2 < count; ++index2)
          {
            cMyReFormat1 = (AdvRichTextBox.cMyREFormat) arrayList[index1];
            AdvRichTextBox.cMyREFormat cMyReFormat2 = (AdvRichTextBox.cMyREFormat) arrayList[index2];
            if (cMyReFormat2.nPos < cMyReFormat1.nPos)
            {
              arrayList.RemoveAt(index2);
              arrayList.Insert(index1, (object) cMyReFormat2);
              --index2;
            }
            else if (cMyReFormat2.nPos == cMyReFormat1.nPos && cMyReFormat2.nLen < cMyReFormat1.nLen)
            {
              arrayList.RemoveAt(index2);
              arrayList.Insert(index1, (object) cMyReFormat2);
              --index2;
            }
          }
        }
        int startIndex = 0;
        for (int index = 0; index < count; ++index)
        {
          cMyReFormat1 = (AdvRichTextBox.cMyREFormat) arrayList[index];
          string str4 = str3.Substring(startIndex, cMyReFormat1.nPos - startIndex);
          if (str4 == "\t")
            str4 = "&nbsp;&nbsp;&nbsp;";
          str2 = str2 + str4 + cMyReFormat1.strValue;
          startIndex = cMyReFormat1.nPos + cMyReFormat1.nLen;
        }
        if (startIndex < str3.Length)
          str2 = str2 + str3.Substring(startIndex);
      }
      catch
      {
      }
      finally
      {
        this.SelectionStart = selectionStart;
        this.SelectionLength = selectionLength;
        this.EndUpdate();
        this.HideSelection = false;
      }
      return str2;
    }

    public void AddHTML(string strHTML)
    {
      AdvRichTextBox.CHARFORMAT defaultCharFormat = this.DefaultCharFormat;
      AdvRichTextBox.PARAFORMAT defaultParaFormat = this.DefaultParaFormat;
      char[] chArray = new char[2]
      {
        ' ',
        char.MinValue
      };
      this.HideSelection = true;
      this.BeginUpdate();
      try
      {
        while (strHTML.Length > 0)
        {
          string str1 = strHTML;
          do
          {
            int num1 = strHTML.IndexOf('<');
            if (num1 >= 0)
            {
              if (num1 > 0)
              {
                str1 = strHTML.Substring(0, num1);
                strHTML = strHTML.Substring(num1);
                break;
              }
              else
              {
                int num2 = strHTML.IndexOf('>', num1);
                if (num2 > num1)
                {
                  if (num2 - num1 > 0)
                  {
                    string str2 = strHTML.Substring(num1, num2 - num1 + 1).ToLower();
                    if (str2 == "<b>")
                    {
                      defaultCharFormat.dwMask |= 4194305U;
                      defaultCharFormat.dwEffects |= 1U;
                      defaultCharFormat.wWeight = (short) 700;
                    }
                    else if (str2 == "<i>")
                    {
                      defaultCharFormat.dwMask |= 2U;
                      defaultCharFormat.dwEffects |= 2U;
                    }
                    else if (str2 == "<u>")
                    {
                      defaultCharFormat.dwMask |= 8388612U;
                      defaultCharFormat.dwEffects |= 4U;
                      defaultCharFormat.bUnderlineType = (byte) 1;
                    }
                    else if (str2 == "<s>")
                    {
                      defaultCharFormat.dwMask |= 8U;
                      defaultCharFormat.dwEffects |= 8U;
                    }
                    else if (str2 == "<sup>")
                    {
                      defaultCharFormat.dwMask |= 196608U;
                      defaultCharFormat.dwEffects |= 131072U;
                    }
                    else if (str2 == "<sub>")
                    {
                      defaultCharFormat.dwMask |= 196608U;
                      defaultCharFormat.dwEffects |= 65536U;
                    }
                    else if (str2.Length > 2 && str2.Substring(0, 2) == "<p")
                    {
                      if (str2.IndexOf("align=\"left\"") > 0)
                      {
                        defaultParaFormat.dwMask |= 8U;
                        defaultParaFormat.wAlignment = (short) 1;
                      }
                      else if (str2.IndexOf("align=\"right\"") > 0)
                      {
                        defaultParaFormat.dwMask |= 8U;
                        defaultParaFormat.wAlignment = (short) 2;
                      }
                      else if (str2.IndexOf("align=\"center\"") > 0)
                      {
                        defaultParaFormat.dwMask |= 8U;
                        defaultParaFormat.wAlignment = (short) 3;
                      }
                    }
                    else if (str2.Length > 5 && str2.Substring(0, 5) == "<font")
                    {
                      string str3 = new string(defaultCharFormat.szFaceName).Trim(chArray);
                      int num3 = defaultCharFormat.crTextColor;
                      int num4 = defaultCharFormat.yHeight;
                      int num5 = str2.IndexOf("face=");
                      if (num5 > 0)
                      {
                        int num6 = str2.IndexOf('"', num5 + 6);
                        if (num6 > num5)
                          str3 = str2.Substring(num5 + 6, num6 - num5 - 6);
                      }
                      int num7 = str2.IndexOf("size=");
                      if (num7 > 0)
                      {
                        int num6 = str2.IndexOf('"', num7 + 6);
                        if (num6 > num7)
                          num4 = int.Parse(str2.Substring(num7 + 6, num6 - num7 - 6)) * 100;
                      }
                      int num8 = str2.IndexOf("color=");
                      if (num8 > 0)
                      {
                        int num6 = str2.IndexOf('"', num8 + 7);
                        if (num6 > num8)
                          num3 = !(str2.Substring(num8 + 7, 1) == "#") ? int.Parse(str2.Substring(num8 + 7, num6 - num8 - 7)) : this.GetCOLORREF(Color.FromArgb(Convert.ToInt32(str2.Substring(num8 + 8, num6 - num8 - 8), 16)));
                      }
                      defaultCharFormat.szFaceName = new char[32];
                      str3.CopyTo(0, defaultCharFormat.szFaceName, 0, Math.Min(31, str3.Length));
                      defaultCharFormat.crTextColor = num3;
                      defaultCharFormat.yHeight = num4;
                      defaultCharFormat.dwMask |= 3758096384U;
                      defaultCharFormat.dwEffects &= 3221225471U;
                    }
                    else if (str2 == "<li>")
                    {
                      if ((int) defaultParaFormat.wNumbering != 1)
                      {
                        defaultParaFormat.dwMask |= 32U;
                        defaultParaFormat.wNumbering = (short) 1;
                      }
                    }
                    else if (str2 == "</b>")
                    {
                      defaultCharFormat.dwEffects &= 4294967294U;
                      defaultCharFormat.wWeight = (short) 400;
                    }
                    else if (str2 == "</i>")
                      defaultCharFormat.dwEffects &= 4294967293U;
                    else if (str2 == "</u>")
                      defaultCharFormat.dwEffects &= 4294967291U;
                    else if (str2 == "</s>")
                      defaultCharFormat.dwEffects &= 4294967287U;
                    else if (str2 == "</sup>")
                      defaultCharFormat.dwEffects &= 4294836223U;
                    else if (str2 == "</sub>")
                      defaultCharFormat.dwEffects &= 4294901759U;
                    else if (!(str2 == "</font>") && !(str2 == "</p>"))
                    {
                      int num9 = str2 == "</li>" ? 1 : 0;
                    }
                    int startIndex = strHTML.IndexOf("<", num2 + 1);
                    if (startIndex > 0)
                    {
                      str1 = strHTML.Substring(num2 + 1, startIndex - num2 - 1);
                      strHTML = strHTML.Substring(startIndex);
                    }
                    else
                    {
                      str1 = num2 + 1 >= strHTML.Length ? "" : strHTML.Substring(num2 + 1);
                      strHTML = "";
                    }
                  }
                  else
                    goto label_59;
                }
                else
                  goto label_60;
              }
            }
            else
              goto label_61;
          }
          while (str1.Length > 0 && (int) str1[0] == 60);
          goto label_62;
label_59:
          strHTML = "";
          goto label_62;
label_60:
          strHTML = "";
          goto label_62;
label_61:
          strHTML = "";
label_62:
          if (str1.Length > 0)
          {
            string str2 = str1.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&apos;", "'").Replace("&quot;", "\"");
            string str3 = str2;
            while (str3.Length > 0)
            {
              int length = str3.Length;
              int selectionStart = this.SelectionStart;
              string str4 = str3.Substring(0, length);
              this.SelectedText = str4;
              str3 = str3.Remove(0, length);
              this.SelectionStart = selectionStart;
              this.SelectionLength = str4.Length;
              this.ParaFormat = defaultParaFormat;
              this.CharFormat = defaultCharFormat;
              this.SelectionStart = this.TextLength + 1;
              this.SelectionLength = 0;
            }
            this.SelectionStart = this.TextLength + 1;
            this.SelectionLength = 0;
            if (str2.IndexOf("\r\n", 0) >= 0 || str2.IndexOf("\n", 0) >= 0)
            {
              defaultParaFormat.dwMask = 40U;
              defaultParaFormat.wAlignment = (short) 1;
              defaultParaFormat.wNumbering = (short) 0;
            }
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      finally
      {
        this.SelectionStart = this.TextLength + 1;
        this.SelectionLength = 0;
        this.EndUpdate();
        this.HideSelection = false;
      }
    }

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static int SendMessage(HandleRef hWnd, int msg, int wParam, ref AdvRichTextBox.PARAFORMAT lp);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static int SendMessage(HandleRef hWnd, int msg, int wParam, ref AdvRichTextBox.CHARFORMAT lp);

    public void BeginUpdate()
    {
      ++this.updating;
      if (this.updating > 1)
        return;
      this.oldEventMask = AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1073, 0, 0);
      AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 11, 0, 0);
    }

    public void EndUpdate()
    {
      --this.updating;
      if (this.updating > 0)
        return;
      AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 11, 1, 0);
      AdvRichTextBox.SendMessage(new HandleRef((object) this, this.Handle), 1073, 0, this.oldEventMask);
    }

    public void SetSuperScript(bool bSet)
    {
      AdvRichTextBox.CHARFORMAT charFormat = this.CharFormat;
      if (bSet)
      {
        charFormat.dwMask |= 196608U;
        charFormat.dwEffects |= 131072U;
      }
      else
        charFormat.dwEffects &= 4294836223U;
      this.CharFormat = charFormat;
    }

    public void SetSubScript(bool bSet)
    {
      AdvRichTextBox.CHARFORMAT charFormat = this.CharFormat;
      if (bSet)
      {
        charFormat.dwMask |= 196608U;
        charFormat.dwEffects |= 65536U;
      }
      else
        charFormat.dwEffects &= 4294901759U;
      this.CharFormat = charFormat;
    }

    public bool IsSuperScript()
    {
      return ((int) this.CharFormat.dwEffects & 131072) == 131072;
    }

    public bool IsSubScript()
    {
      return ((int) this.CharFormat.dwEffects & 65536) == 65536;
    }

    private struct cMyREFormat
    {
      public int nLen;
      public int nPos;
      public AdvRichTextBox.uMyREType nType;
      public string strValue;
    }

    private enum ctformatStates
    {
      nctNone,
      nctNew,
      nctContinue,
      nctReset,
    }

    private enum uMyREType
    {
      U_MYRE_TYPE_TAG,
      U_MYRE_TYPE_EMO,
      U_MYRE_TYPE_ENTITY,
    }

    public struct CHARFORMAT
    {
      public int cbSize;
      public uint dwMask;
      public uint dwEffects;
      public int yHeight;
      public int yOffset;
      public int crTextColor;
      public byte bCharSet;
      public byte bPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
      public char[] szFaceName;
      public short wWeight;
      public short sSpacing;
      public int crBackColor;
      public uint lcid;
      public uint dwReserved;
      public short sStyle;
      public short wKerning;
      public byte bUnderlineType;
      public byte bAnimation;
      public byte bRevAuthor;
      public byte bReserved1;
    }

    public struct PARAFORMAT
    {
      public int cbSize;
      public uint dwMask;
      public short wNumbering;
      public short wReserved;
      public int dxStartIndent;
      public int dxRightIndent;
      public int dxOffset;
      public short wAlignment;
      public short cTabCount;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
      public int[] rgxTabs;
      public int dySpaceBefore;
      public int dySpaceAfter;
      public int dyLineSpacing;
      public short sStyle;
      public byte bLineSpacingRule;
      public byte bOutlineLevel;
      public short wShadingWeight;
      public short wShadingStyle;
      public short wNumberingStart;
      public short wNumberingStyle;
      public short wNumberingTab;
      public short wBorderSpace;
      public short wBorderWidth;
      public short wBorders;
    }
  }
}
