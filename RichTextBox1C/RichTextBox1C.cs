// Type: Compiler1C.RichTextBox1C.RichTextBox1C
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll

using Parser1C;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Compiler1C.RichTextBox1C
{
  public class RichTextBox1C : AdvRichTextBox
  {
    private static bool m_bPaint = true;
    private bool detectTextChanged = true;
    private int tekSelect;
    private int prevSelect;
    public ListView words;
    private IContainer components;

    public int PrevSelect
    {
      get
      {
        return this.prevSelect;
      }
      set
      {
        this.prevSelect = value;
      }
    }

    public bool DetectTextChanged
    {
      get
      {
        return this.detectTextChanged;
      }
      set
      {
        this.detectTextChanged = value;
      }
    }

    public Version1C Version1C { get; set; }

    public event RichTextBox1C.ParseDelegate OnProgressParse;

    static RichTextBox1C()
    {
    }

    public RichTextBox1C()
    {
      this.InitializeComponent();
      this.words = new ListView();
      this.words.Columns.Add("isWork");
    }

    private void RichTextBox1C_TextChanged(object sender, EventArgs e)
    {
      if (!this.detectTextChanged)
        return;
      this.tekSelect = this.SelectionStart;
      int selectionStart = this.SelectionStart;
      RichTextBox1C.m_bPaint = false;
      int start = selectionStart;
      while (start > 0 && (int) this.Text[start - 1] != 10)
        --start;
      int end = selectionStart;
      while (end < this.Text.Length && (int) this.Text[end] != 10)
        ++end;
      this.ParseCode(start, end, false);
      this.prevSelect = this.tekSelect;
      RichTextBox1C.m_bPaint = true;
    }

    public void ParseCode()
    {
      this.ParseCode(0, this.Text.Length, true);
    }

    private void ParseCode(int start, int end, bool showProgress)
    {
      RichTextBox1C richTextBox1C = this;
      string str1 = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЪЭЮЯABCDEFGHIJKLMNOPQRSTUVWXYZ";
      string str2 = "0123456789";
      string str3 = (string) null;
      string str4 = "";
      string str5 = (string) null;
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = false;
      for (int index = start; index <= end; ++index)
      {
        if (showProgress && this.OnProgressParse != null)
          this.OnProgressParse(index, end);
        string str6;
        string str7;
        if (index >= this.Text.Length)
        {
          flag3 = true;
          str6 = "";
          str7 = "";
        }
        else
        {
          if (index == end)
            flag3 = true;
          str6 = richTextBox1C.Text.Substring(index, 1);
          str7 = str6.ToUpper();
        }
        int num = str6 == ";" ? 1 : 0;
        bool flag4 = false;
        bool flag5 = false;
        bool flag6 = false;
        bool flag7 = false;
        if (str1.IndexOf(str7) > -1)
          flag7 = true;
        else if (str2.IndexOf(str6) > -1)
          flag4 = true;
        if (str6 == "\n" || str6 == "\r" || str6 == " ")
          flag5 = true;
        if (str6 == "\n" || index == end)
          flag6 = true;
        if (str6 == "|")
        {
          flag1 = false;
          str3 = "S";
        }
        if (str6 == "#")
          str3 = "P";
        if (str3 == "S")
        {
          switch (str6)
          {
            case "\"":
              flag1 = !flag1;
              str4 = str4 + "\"";
              break;
            case "|":
              flag1 = false;
              str4 = str4 + "|";
              break;
            default:
              if (flag1)
              {
                this.SetColor(str4, index, Color.Black);
                this.AddWord(str4, index - str4.Length);
                str4 = "";
                str3 = (string) null;
                break;
              }
              else
              {
                str4 = str4 + str6;
                break;
              }
          }
          if (flag3)
            this.SetColor(str4, index, Color.Black);
        }
        if (str3 == "K")
        {
          if (flag6)
          {
            this.SetColor(str4, index, Color.Green);
            str4 = "";
            str3 = (string) null;
            flag6 = false;
          }
          else
            str4 = str4 + str6;
          if (flag3)
            this.SetColor(str4, index, Color.Green);
        }
        if (str3 == "#")
        {
          if (flag6)
          {
            if (this.Version1C == Version1C.v8)
              this.SetColor(str4, index, Color.FromArgb((int) byte.MaxValue, 150, 50, 0));
            else
              this.SetColor(str4, index, Color.Black);
            str4 = "";
            str3 = (string) null;
            flag6 = false;
          }
          else
            str4 = str4 + str6;
          if (flag3)
          {
            if (this.Version1C == Version1C.v8)
              this.SetColor(str4, index, Color.FromArgb((int) byte.MaxValue, 150, 50, 0));
            else
              this.SetColor(str4, index, Color.Black);
          }
        }
        if (str3 == "I")
        {
          if (flag7 || flag4)
          {
            str4 = str4 + str6;
          }
          else
          {
            if (this.Version1C == Version1C.v8)
            {
              if (RichTextBox1C.isWorkV8(str4))
                this.SetColor(str4, index, Color.Red);
              else
                this.SetColor(str4, index, Color.Blue);
            }
            else if (RichTextBox1C.isWorkV7(str4))
              this.SetColor(str4, index, Color.Red);
            else
              this.SetColor(str4, index, Color.Blue);
            this.AddWord(str4, index - str4.Length);
            str4 = "";
            str3 = (string) null;
          }
          if (flag3)
          {
            if (this.Version1C == Version1C.v8)
            {
              if (RichTextBox1C.isWorkV8(str4))
              {
                this.SetColor(str4, index, Color.Red);
                this.AddWord(str4, index - str4.Length);
              }
              else
                this.SetColor(str4, index, Color.Blue);
            }
            else if (RichTextBox1C.isWorkV7(str4))
            {
              this.SetColor(str4, index, Color.Red);
              this.AddWord(str4, index - str4.Length);
            }
            else
              this.SetColor(str4, index, Color.Blue);
          }
        }
        if (str3 == "C")
        {
          this.SetColor(str4, index, Color.Black);
          str4 = "";
          str3 = (string) null;
        }
        if (str3 == "P")
        {
          if (str6 == "/" && str5 == "/")
          {
            str4 = "//";
            str3 = "K";
            str5 = str6;
            continue;
          }
          else if (str6 == "#")
          {
            str4 = "#";
            str3 = "#";
            continue;
          }
          else if (str6 == "\"")
          {
            richTextBox1C.Select(index - 1, 1);
            richTextBox1C.SelectionColor = Color.Red;
            str4 = "";
            str3 = (string) null;
          }
          else if (flag5 || flag6)
          {
            richTextBox1C.Select(index - 1, 1);
            richTextBox1C.SelectionColor = Color.Red;
            str4 = "";
            str3 = (string) null;
          }
          else if (flag7 || flag4)
          {
            this.SetColor(str4, index, Color.Red);
            str4 = "";
            str3 = (string) null;
          }
          else
          {
            str4 = str4 + str6;
            richTextBox1C.Select(index - str4.Length + 1, str4.Length);
            richTextBox1C.SelectionColor = Color.Red;
          }
        }
        if (str3 == null)
        {
          if (flag5)
            str3 = "R";
          else if (flag7)
            str3 = "I";
          else if (flag4)
          {
            str3 = "C";
          }
          else
          {
            switch (str6)
            {
              case "\"":
                str3 = "S";
                flag1 = false;
                break;
              case "|":
                str3 = "S";
                flag1 = true;
                break;
              case "'":
                str3 = "D";
                flag2 = true;
                break;
              default:
                str3 = "P";
                break;
            }
          }
          str4 = str4 + str6;
        }
        if (str3 == "D")
        {
          if (!flag2)
          {
            str4 = str4 + str6;
            if (str6 == "'")
            {
              this.SetColor(str4, index, Color.Black);
              str4 = "";
              str3 = (string) null;
            }
          }
          flag2 = false;
        }
        if (str3 == "R")
        {
          str4 = "";
          str3 = (string) null;
        }
        str5 = str6;
      }
      richTextBox1C.Select(this.tekSelect, 0);
    }

    private void AddWord(string txt, int index)
    {
      if (this.words.Items.Find(index.ToString(), false).Length != 0)
        return;
      ListViewItem listViewItem1 = new ListViewItem()
      {
        Name = index.ToString(),
        Text = txt
      };
      listViewItem1.SubItems.Add(RichTextBox1C.isWorkV8(txt).ToString());
      this.words.Items.Add(listViewItem1);
      if (listViewItem1.Index <= 0)
        return;
      ListViewItem listViewItem2 = this.words.Items[listViewItem1.Index - 1];
      for (int startIndex = int.Parse(listViewItem2.Name) + listViewItem2.Text.Length; startIndex < index; ++startIndex)
      {
        string str = this.Text.Substring(startIndex, 1);
        if (!(str.Trim() == ""))
        {
          ListViewItem listViewItem3 = new ListViewItem()
          {
            Name = startIndex.ToString(),
            Text = str
          };
          listViewItem3.SubItems.Add("");
          this.words.Items.Insert(listViewItem1.Index, listViewItem3);
        }
      }
    }

    private static bool isWorkV8(string token)
    {
      string[] strArray = new string[71]
      {
        "if",
        "если",
        "then",
        "тогда",
        "elsif",
        "иначеесли",
        "else",
        "иначе",
        "endif",
        "конецесли",
        "do",
        "цикл",
        "for",
        "для",
        "to",
        "по",
        "each",
        "каждого",
        "in",
        "из",
        "while",
        "пока",
        "endDo",
        "конеццикла",
        "procedure",
        "процедура",
        "endprocedure",
        "конецпроцедуры",
        "function",
        "функция",
        "endfunction",
        "конецфункции",
        "var",
        "перем",
        "export",
        "экспорт",
        "goto",
        "перейти",
        "and",
        "и",
        "or",
        "или",
        "not",
        "не",
        "val",
        "знач",
        "break",
        "прервать",
        "continue",
        "продолжить",
        "return",
        "возврат",
        "try",
        "попытка",
        "except",
        "исключение",
        "endTry",
        "конецпопытки",
        "raise",
        "вызватьисключение",
        "false",
        "ложь",
        "true",
        "истина",
        "undefined",
        "неопределено",
        "null",
        "new",
        "новый",
        "execute",
        "выполнить"
      };
      for (int index = 0; index < strArray.GetLength(0); ++index)
      {
        if (strArray[index].ToUpper() == token.ToUpper())
          return true;
      }
      return false;
    }

    private static bool isWorkV7(string token)
    {
      string[] strArray = new string[373]
      {
        "_getperformancecounter",
        "_idtostr",
        "_strtoid",
        "accessright",
        "accountbycode",
        "addmonth",
        "algorithmgroup",
        "and",
        "asc",
        "attachaddin",
        "autonumprefix",
        "basiccalcjournal",
        "beep",
        "beginofperiodbt",
        "begintransaction",
        "begofmonth",
        "begofquart",
        "begofweek",
        "begofyear",
        "bindir",
        "birthibofobject",
        "break",
        "calcregsonbeg",
        "calcregsonend",
        "calculation",
        "calculationkind",
        "calendars",
        "centralibcode",
        "chartsofaccounts",
        "chr",
        "clearmessagewindow",
        "committransaction",
        "computername",
        "const",
        "context",
        "continue",
        "createobject",
        "curdate",
        "currentibcode",
        "currentibdescr",
        "currentibstatus",
        "currenttime",
        "date",
        "dbdir",
        "defaultchartofaccounts",
        "defaultchartofaccounts",
        "deleteobjects",
        "dim",
        "do",
        "domessagebox",
        "doquerybox",
        "else",
        "elsif",
        "emptyvalue",
        "enddo",
        "endfunction",
        "endif",
        "endofcalculatedperiodbt",
        "endofmonth",
        "endofperiodbt",
        "endofquart",
        "endofweek",
        "endofyear",
        "endprocedure",
        "endtry",
        "enum",
        "except",
        "exclusivemode",
        "exitsystem",
        "export",
        "find",
        "findmarkedfordelete",
        "findreferences",
        "fixtemplate",
        "for",
        "format",
        "forward",
        "fs",
        "function",
        "generallanguage",
        "getap",
        "getapposition",
        "getdateofap",
        "getday",
        "getdayofweek",
        "getdayofyear",
        "getdocofap",
        "getemptyvalue",
        "geterrordescription",
        "getmonth",
        "getselectionvalues",
        "gettimeofap",
        "getweekofyear",
        "getyear",
        "goto",
        "ibdir",
        "idleprocessing",
        "if",
        "inputchartofaccounts",
        "inputdate",
        "inputenum",
        "inputnumeric",
        "inputperiod",
        "inputstring",
        "inputsubcontokind",
        "inputvalue",
        "int",
        "isblankstring",
        "iscurrentibcenter",
        "iscurrentibrecepientonly",
        "left",
        "linebreak",
        "ln",
        "loadaddin",
        "log10",
        "logmessagewrite",
        "lower",
        "mainchartofaccounts",
        "makedocposition",
        "max",
        "maxsubcontocount",
        "message",
        "metadata",
        "mid",
        "min",
        "not",
        "number",
        "openform",
        "openformmodal",
        "or",
        "pagebreak",
        "periodstr",
        "procedure",
        "raise",
        "recalculationrule",
        "register",
        "restorevalue",
        "return",
        "returnstatus",
        "right",
        "rightname",
        "rollbacktransaction",
        "round",
        "runapp",
        "savevalue",
        "sequence",
        "setaccount",
        "setaptobeg",
        "setaptoend",
        "setkind",
        "spelling",
        "splitdocposition",
        "status",
        "strcountoccur",
        "strgetline",
        "string",
        "strlen",
        "strlinecount",
        "strreplace",
        "subcontokinds",
        "system",
        "systemcaption",
        "tabsymbol",
        "tempfilesdir",
        "template",
        "then",
        "to",
        "trimall",
        "triml",
        "trimr",
        "try",
        "upper",
        "userdir",
        "userfullname",
        "userinterfacename",
        "username",
        "val",
        "valuefromfile",
        "valuefromstring",
        "valuefromstringinternal",
        "valuetofile",
        "valuetostring",
        "valuetostringinternal",
        "valuetype",
        "valuetypestr",
        "var",
        "while",
        "workingdate",
        "ввестивидсубконто",
        "ввестидату",
        "ввестизначение",
        "ввестиперечисление",
        "ввестипериод",
        "ввестиплансчетов",
        "ввестистроку",
        "ввестичисло",
        "видрасчета",
        "видрасчета",
        "видысубконто",
        "возврат",
        "вопрос",
        "восстановитьзначение",
        "врег",
        "выбранныйплансчетов",
        "вызватьисключение",
        "группарасчетов",
        "далее",
        "дата",
        "датагод",
        "датамесяц",
        "датачисло",
        "для",
        "добавитьмесяц",
        "если",
        "завершитьработусистемы",
        "заголовоксистемы",
        "загрузитьвнешнююкомпоненту",
        "записьжурналарегистрации",
        "запуститьприложение",
        "зафиксироватьтранзакцию",
        "знач",
        "значениевстроку",
        "значениевстрокувнутр",
        "значениевфайл",
        "значениеизстроки",
        "значениеизстрокивнутр",
        "значениеизфайла",
        "и",
        "ибсозданияобъекта",
        "или",
        "имякомпьютера",
        "имяпользователя",
        "иначе",
        "иначеесли",
        "исключение",
        "календари",
        "каталогбазыданных",
        "каталогвременныхфайлов",
        "каталогиб",
        "каталогпользователя",
        "каталогпрограммы",
        "кодсимв",
        "командасистемы",
        "конгода",
        "конецесли",
        "конецпериодаби",
        "конецпопытки",
        "конецпроцедуры",
        "конецрассчитанногопериодаби",
        "конецфункции",
        "конеццикла",
        "конквартала",
        "конмесяца",
        "коннедели",
        "константа",
        "контекст",
        "лев",
        "лог",
        "лог10",
        "макс",
        "максимальноеколичествосубконто",
        "метаданные",
        "мин",
        "монопольныйрежим",
        "названиеинтерфейса",
        "названиенабораправ",
        "назначитьвид",
        "назначитьсчет",
        "найти",
        "найтипомеченныенаудаление",
        "найтиссылки",
        "началопериодаби",
        "начатьтранзакцию",
        "начгода",
        "начквартала",
        "начмесяца",
        "начнедели",
        "не",
        "номерднягода",
        "номерднянедели",
        "номернеделигода",
        "нрег",
        "обработкаожидания",
        "окр",
        "описаниеошибки",
        "основнойжурналрасчетов",
        "основнойплансчетов",
        "основнойплансчетов",
        "основнойязык",
        "открытьформу",
        "открытьформумодально",
        "отменитьтранзакцию",
        "очиститьокносообщений",
        "перейти",
        "перем",
        "перечисление",
        "периодстр",
        "планысчетов",
        "по",
        "подключитьвнешнююкомпоненту",
        "пока",
        "полноеимяпользователя",
        "получитьвремята",
        "получитьдатута",
        "получитьдокументта",
        "получитьзначенияотбора",
        "получитьпозициюта",
        "получитьпустоезначение",
        "получитьта",
        "попытка",
        "последовательность",
        "прав",
        "правилоперерасчета",
        "праводоступа",
        "предупреждение",
        "прервать",
        "префиксавтонумерации",
        "продолжить",
        "пропись",
        "процедура",
        "пустаястрока",
        "пустоезначение",
        "рабочаядата",
        "разделительстраниц",
        "разделительстрок",
        "разм",
        "разобратьпозициюдокумента",
        "рассчитатьрегистрына",
        "рассчитатьрегистрыпо",
        "регистр",
        "сигнал",
        "симв",
        "символтабуляции",
        "создатьобъект",
        "сокрл",
        "сокрлп",
        "сокрп",
        "сообщить",
        "состояние",
        "сохранитьзначение",
        "сред",
        "статусвозврата",
        "стрдлина",
        "стрзаменить",
        "стрколичествострок",
        "строка",
        "стрполучитьстроку",
        "стрчисловхождений",
        "сформироватьпозициюдокумента",
        "счетпокоду",
        "текущаядата",
        "текущаяибкод",
        "текущаяибнаименование",
        "текущаяибстатус",
        "текущаяибтолькополучатель",
        "текущаяибцентральная",
        "текущеевремя",
        "типзначения",
        "типзначениястр",
        "тогда",
        "удалитьобъекты",
        "установитьтана",
        "установитьтапо",
        "фиксшаблон",
        "формат",
        "фс",
        "функция",
        "цел",
        "центральнаяибкод",
        "цикл",
        "число",
        "шаблон",
        "экспорт"
      };
      for (int index = 0; index < strArray.GetLength(0); ++index)
      {
        if (strArray[index].ToUpper() == token.ToUpper())
          return true;
      }
      return false;
    }

    private void SetColor(string token, int index, Color color)
    {
      int length = token.Length;
      this.Select(index - length, length);
      if (!(this.SelectionColor != color))
        return;
      this.SelectionColor = color;
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 15)
      {
        if (RichTextBox1C.m_bPaint)
          base.WndProc(ref m);
        else
          m.Result = IntPtr.Zero;
      }
      else
        base.WndProc(ref m);
    }

    public void SetText(string text)
    {
      this.AppendText(text);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.TextChanged += new EventHandler(this.RichTextBox1C_TextChanged);
      this.ResumeLayout(false);
    }

    public delegate void ParseDelegate(int i, int length);
  }
}
