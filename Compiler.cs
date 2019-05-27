// Type: Compiler1C.Compiler
// Assembly: Compiler1C, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b60b150effcc610f
// MVID: 5117C784-90B9-4853-8623-3DCCE6577349
// Assembly location: C:\Current Version\Compiler1C.dll


using Compiler1C.Объекты1С;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Compiler1C.RichTextBox1C;

namespace Compiler1C
{
  public class Compiler : Form
  {
    private ListView.ListViewItemCollection words;
    private IContainer components;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem файлToolStripMenuItem;
    private ToolStrip toolStrip1;
    private StatusStrip statusStrip1;
    private SplitContainer splitContainer1;
    private SplitContainer splitContainer3;
    private RichTextBox1C.RichTextBox1C txtCode1C;
    private TextBox txtCode;
    private SplitContainer splitContainer2;
    private TextBox txtTokens;
    private TreeView treeCode;
    private ToolStripButton tsParse;
    private ToolStripButton tsCompile;

    public Compiler()
    {
      this.InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      this.ParseCode();
    }

    public void ParseCode()
    {
      Global.callsProc.Clear();
      Global.perems.Clear();
      Global.procedures.Clear();
      Global.ravno.Clear();
      this.txtTokens.Clear();
      this.treeCode.Nodes.Clear();
      this.txtCode1C.words.Clear();
      this.txtCode1C.ParseCode();
      this.words = this.txtCode1C.words.Items;
      for (int i = 0; i < this.words.Count; ++i)
      {
        ListViewItem itm = this.words[i];
        this.txtTokens.AppendText(itm.Name + ": " + itm.Text + Environment.NewLine);
        this.AddNode(itm, ref i);
      }
      this.SortCodeTree();
      ArrayList parametrs = new ArrayList();
      int num = this.txtCode1C.Text.IndexOf("// КонецКоманднойСтроки");
      if (this.txtCode1C.Text.StartsWith("// КоманднаяСтрока") && num > 0)
      {
        string str1 = this.txtCode1C.Text.Substring(19, num - 19);
        char[] chArray = new char[1]
        {
          '\n'
        };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.StartsWith("// "))
          {
            string[] strArray = str2.Substring(4).Split(new char[1]
            {
              ';'
            });
            if (strArray.Length < 3)
              return;
            string str3 = strArray[0].Trim();
            string str4 = strArray[1].Trim();
            string str5 = strArray[2].Trim();
            Переменная переменная;
            switch (str4.ToLower())
            {
              case "число":
                переменная = new Переменная()
                {
                  Имя = str3,
                  TypeObject1C = Compiler.TypeObject1C.Число
                };
                break;
              case "булево":
                переменная = new Переменная()
                {
                  Имя = str3,
                  TypeObject1C = Compiler.TypeObject1C.Булево
                };
                break;
              default:
                переменная = new Переменная()
                {
                  Имя = str3,
                  TypeObject1C = Compiler.TypeObject1C.Строка
                };
                break;
            }
            переменная.Text = str5;
            parametrs.Add((object) переменная);
            Global.perems.Add((object) переменная.Имя, (object) переменная);
          }
        }
      }
      this.txtCode.Text = GenerateCode.GenerateCSharpCode(this.treeCode, parametrs);
    }

    public void CompileCode(string nativeFile, bool showErrors)
    {
      GenerateCode.CompileToExe(this.txtCode.Text, nativeFile, showErrors);
    }

    public bool LoadCode1C(string file)
    {
      if (!File.Exists(file))
        return false;
      using (TextReader textReader = (TextReader) new StreamReader(file, Encoding.Default))
      {
        this.txtCode1C.Text = textReader.ReadToEnd();
        textReader.Close();
      }
      return true;
    }

    private void SortCodeTree()
    {
      for (int index = 0; index < this.treeCode.Nodes.Count; ++index)
      {
        CodeStruct codeStruct = (CodeStruct) this.treeCode.Nodes[index];
        if (codeStruct.TypeObject1C == Compiler.TypeObject1C.Переменная)
        {
          this.treeCode.Nodes.Remove((TreeNode) codeStruct);
          this.treeCode.Nodes.Insert(0, (TreeNode) codeStruct);
        }
      }
      int index1 = 0;
      while (index1 < this.treeCode.Nodes.Count && ((CodeStruct) this.treeCode.Nodes[index1]).TypeObject1C == Compiler.TypeObject1C.Переменная)
        ++index1;
      int index2 = index1;
      if (!this.treeCode.Nodes.ContainsKey("приоткрытии"))
        return;
      TreeNode node = this.treeCode.Nodes["приоткрытии"];
      this.treeCode.Nodes.Remove(node);
      this.treeCode.Nodes.Insert(index2, node);
    }

    private void AddNode(ListViewItem itm, ref int i)
    {
      switch (itm.Text.ToLower())
      {
        case "перем":
          CodeStruct node1 = new CodeStruct(itm.Text);
          i = this.AddPerem(node1, itm);
          break;
        case "процедура":
          CodeStruct node2 = new CodeStruct(itm.Text);
          i = this.AddProcedure(node2, itm);
          break;
        case "функция":
          CodeStruct node3 = new CodeStruct(itm.Text);
          i = this.AddFunction(node3, itm);
          break;
      }
    }

    private int AddPerem(CodeStruct node, ListViewItem itm)
    {
      int num = this.words.Count;
      Переменная переменная = new Переменная();
      node.Object1C = (object) переменная;
      node.TypeObject1C = Compiler.TypeObject1C.Переменная;
      ListView listView = new ListView();
      for (int index = itm.Index + 1; index < this.words.Count; ++index)
      {
        ListViewItem listViewItem = this.words[index];
        if (listViewItem.Text.ToLower() == ";")
        {
          num = index;
          break;
        }
        else
          listView.Items.Add((ListViewItem) listViewItem.Clone());
      }
      node.Name = listView.Items[0].Text;
      переменная.Имя = listView.Items[0].Text;
      Global.perems.Add((object) переменная.Имя, (object) переменная);
      CodeStruct codeStruct = node;
      string str = codeStruct.Text + " " + переменная.Имя + ";";
      codeStruct.Text = str;
      this.treeCode.Nodes.Add((TreeNode) node);
      return num;
    }

    private int AddProcedure(CodeStruct node, ListViewItem itm)
    {
      int num1 = this.words.Count;
      Процедура процедура = new Процедура();
      node.Object1C = (object) процедура;
      node.TypeObject1C = Compiler.TypeObject1C.Процедура;
      ListView body = new ListView();
      for (int index = itm.Index + 1; index < this.words.Count; ++index)
      {
        ListViewItem listViewItem = this.words[index];
        if (listViewItem.Text.ToLower() == "конецпроцедуры")
        {
          num1 = index;
          break;
        }
        else
        {
          body.Items.Add((ListViewItem) listViewItem.Clone());
          this.txtTokens.AppendText(listViewItem.Name + ": " + listViewItem.Text + Environment.NewLine);
        }
      }
      if (body.Items.Count == 0)
        throw new Exception("Ожидается конец продедуры!");
      процедура.Name = body.Items[0].Text;
      node.Name = body.Items[0].Text;
      Global.procedures.Add((object) процедура.Name, (object) процедура);
      ListViewItem itemWithText1 = body.FindItemWithText("(");
      if (itemWithText1 == null)
        throw new Exception("Ожидается символ '('");
      int index1 = itemWithText1.Index;
      ListViewItem itemWithText2 = body.FindItemWithText(")");
      if (itemWithText2 == null)
        throw new Exception("Ожидается символ ')'");
      int index2 = itemWithText2.Index;
      int num2 = index2 - index1;
      string str1 = "";
      if (num2 > 1)
      {
        for (int index3 = index1 + 1; index3 < index2; ++index3)
        {
          str1 = str1 + body.Items[index3].Text;
          if (!(body.Items[index3].Text == ","))
            процедура.Parametrs.Add((object) body.Items[index3].Text);
        }
      }
      CodeStruct codeStruct = node;
      string str2 = codeStruct.Text + " " + процедура.Name + "(" + str1 + ")";
      codeStruct.Text = str2;
      this.treeCode.Nodes.Add((TreeNode) node);
      this.ParseBody(процедура.Objects, (TreeNode) node, body, index2 + 1);
      return num1;
    }

    private int AddFunction(CodeStruct node, ListViewItem itm)
    {
      int num1 = this.words.Count;
      Функция функция = new Функция();
      node.Object1C = (object) функция;
      node.TypeObject1C = Compiler.TypeObject1C.Функция;
      ListView body = new ListView();
      for (int index = itm.Index + 1; index < this.words.Count; ++index)
      {
        ListViewItem listViewItem = this.words[index];
        if (listViewItem.Text.ToLower() == "конецфункции")
        {
          num1 = index;
          break;
        }
        else
        {
          body.Items.Add((ListViewItem) listViewItem.Clone());
          this.txtTokens.AppendText(listViewItem.Name + ": " + listViewItem.Text + Environment.NewLine);
        }
      }
      if (body.Items.Count == 0)
        throw new Exception("Ожидается конец функции!");
      функция.Name = body.Items[0].Text;
      node.Name = body.Items[0].Text;
      Global.procedures.Add((object) функция.Name, (object) функция);
      ListViewItem itemWithText1 = body.FindItemWithText("(");
      if (itemWithText1 == null)
        throw new Exception("Ожидается символ '('");
      int index1 = itemWithText1.Index;
      ListViewItem itemWithText2 = body.FindItemWithText(")");
      if (itemWithText2 == null)
        throw new Exception("Ожидается символ ')'");
      int index2 = itemWithText2.Index;
      int num2 = index2 - index1;
      string str1 = "";
      if (num2 > 1)
      {
        for (int index3 = index1 + 1; index3 < index2; ++index3)
        {
          str1 = str1 + body.Items[index3].Text;
          if (!(body.Items[index3].Text == ","))
            функция.Parametrs.Add((object) body.Items[index3].Text);
        }
      }
      CodeStruct codeStruct = node;
      string str2 = codeStruct.Text + " " + функция.Name + "(" + str1 + ")";
      codeStruct.Text = str2;
      this.treeCode.Nodes.Add((TreeNode) node);
      this.ParseBody(функция.Objects, (TreeNode) node, body, index2 + 1);
      return num1;
    }

    private void ParseBody(Hashtable objects, TreeNode parent, ListView body, int start)
    {
      for (int index = start; index < body.Items.Count; ++index)
      {
        ListViewItem listViewItem = body.Items[index];
        switch (listViewItem.Text.ToLower())
        {
          case "для":
            CodeStruct node1 = new CodeStruct(listViewItem.Text);
            parent.Nodes.Add((TreeNode) node1);
            index = this.AddCycle(objects, node1, body, listViewItem.Index + 1);
            break;
          case "сообщить":
            CodeStruct node2 = new CodeStruct(listViewItem.Text);
            parent.Nodes.Add((TreeNode) node2);
            index = Compiler.AddMessage(node2, body, listViewItem.Index + 1);
            break;
          case "если":
            CodeStruct codeStruct = new CodeStruct(listViewItem.Text)
            {
              OperatorType = CodeStruct.TypeOperator.Условие
            };
            parent.Nodes.Add((TreeNode) codeStruct);
            CodeStruct node3 = new CodeStruct(listViewItem.Text);
            codeStruct.Nodes.Add((TreeNode) node3);
            index = this.AddCondition(objects, node3, body, listViewItem.Index + 1, 0);
            break;
          case "иначеесли":
            CodeStruct node4 = new CodeStruct(listViewItem.Text);
            parent.Parent.Nodes.Add((TreeNode) node4);
            index = this.AddCondition(objects, node4, body, listViewItem.Index + 1, 1);
            break;
          case "иначе":
            CodeStruct node5 = new CodeStruct(listViewItem.Text);
            parent.Parent.Nodes.Add((TreeNode) node5);
            index = this.AddCondition(objects, node5, body, listViewItem.Index + 1, 2);
            break;
          case "возврат":
            CodeStruct node6 = new CodeStruct(listViewItem.Text);
            parent.Nodes.Add((TreeNode) node6);
            index = Compiler.AddOperator(node6, body, listViewItem.Index + 1, CodeStruct.TypeOperator.ВозвратФункции);
            break;
          default:
            if (listViewItem.SubItems[1].Text != "")
            {
              CodeStruct node7 = new CodeStruct(listViewItem.Text);
              parent.Nodes.Add((TreeNode) node7);
              index = Compiler.GetFullString(objects, node7, body, listViewItem.Index + 1);
              break;
            }
            else
              break;
        }
      }
    }

    private int AddCondition(Hashtable objects, CodeStruct node, ListView body, int start, int type)
    {
      int num1 = 0;
      int num2 = -1;
      for (int index = start; index < body.Items.Count; ++index)
      {
        if (body.Items[index].Text.ToLower() == "если")
          ++num1;
      }
      ListView body1 = new ListView();
      for (int index = start; index < body.Items.Count; ++index)
      {
        ListViewItem listViewItem1 = body.Items[index];
        if (listViewItem1.Text.ToLower() == "конецесли")
        {
          if (num1 > 0)
            body1.Items.Add((ListViewItem) listViewItem1.Clone());
        }
        else
          body1.Items.Add((ListViewItem) listViewItem1.Clone());
        if (!(listViewItem1.Text.ToLower() != "конецесли"))
        {
          --num1;
          if (num1 < 0)
          {
            ListViewItem listViewItem2 = body.Items[index + 1];
            body1.Items.Add((ListViewItem) listViewItem2.Clone());
            num2 = index;
            break;
          }
        }
      }
      if (num2 == -1 && type == 0)
        throw new Exception("Ожидается выражение 'КонецЕсли'");
      int num3 = num2 + 1;
      if (type == 2)
      {
        num3 = body.Items.Count;
        Условие условие = new Условие()
        {
          TypeCondition = type
        };
        node.Object1C = (object) условие;
        node.TypeObject1C = Compiler.TypeObject1C.Условие;
        node.Text = условие.ToString();
        this.ParseBody(objects, (TreeNode) node, body1, 0);
      }
      else
      {
        if (type == 1)
          num3 = body.Items.Count;
        ListViewItem itemWithText = body1.FindItemWithText("тогда");
        if (itemWithText == null)
          throw new Exception("Ожидается выражение 'тогда'");
        int index1 = itemWithText.Index;
        ArrayList arrayList1 = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        for (int index2 = 0; index2 < index1; ++index2)
        {
          ListViewItem listViewItem = body1.Items[index2];
          if (listViewItem.Text.ToLower() == "и" || listViewItem.Text.ToLower() == "или")
          {
            arrayList1.Add((object) arrayList2);
            arrayList1.Add((object) listViewItem.Text.ToUpper());
            arrayList2 = new ArrayList();
          }
          else
          {
            Объект1С объект1С = new Объект1С(listViewItem.Text);
            arrayList2.Add((object) объект1С);
          }
        }
        arrayList1.Add((object) arrayList2);
        Условие условие = new Условие()
        {
          Conditions = arrayList1,
          TypeCondition = type
        };
        node.Object1C = (object) условие;
        node.TypeObject1C = Compiler.TypeObject1C.Условие;
        node.Text = условие.ToString();
        this.ParseBody(objects, (TreeNode) node, body1, index1 + 1);
      }
      return num3;
    }

    private static int AddOperator(CodeStruct node, ListView body, int start, CodeStruct.TypeOperator typeOperator)
    {
      ListViewItem itemWithText = body.FindItemWithText(";", false, start);
      if (itemWithText == null)
        throw new Exception("Ожидается символ ';'");
      int index1 = itemWithText.Index;
      CodeStruct codeStruct1 = node;
      string str1 = codeStruct1.Text + " ";
      codeStruct1.Text = str1;
      node.OperatorType = typeOperator;
      for (int index2 = start; index2 < index1; ++index2)
      {
        node.Objects.Add((object) new Объект1С(body.Items[index2].Text));
        CodeStruct codeStruct2 = node;
        string str2 = codeStruct2.Text + body.Items[index2].Text;
        codeStruct2.Text = str2;
      }
      CodeStruct codeStruct3 = node;
      string str3 = codeStruct3.Text + ";";
      codeStruct3.Text = str3;
      return index1;
    }

    private static int AddMessage(CodeStruct node, ListView body, int start)
    {
      Сообщить сообщить = new Сообщить();
      ListViewItem itemWithText1 = body.FindItemWithText("(", false, start);
      if (itemWithText1 == null)
        throw new Exception("Ожидается символ '('");
      int index1 = itemWithText1.Index;
      ListViewItem itemWithText2 = body.FindItemWithText(";", false, start);
      if (itemWithText2 == null)
        throw new Exception("Ожидается символ ';'");
      int index2 = itemWithText2.Index;
      ListViewItem itemWithText3 = body.FindItemWithText(")", false, index2 - 1);
      if (itemWithText3 == null)
        throw new Exception("Ожидается символ ')'");
      int index3 = itemWithText3.Index;
      int num = index3 - index1;
      Объект1С объект1С = new Объект1С("Сообщить", Compiler.TypeObject1C.Сообщить);
      node.Objects.Add((object) объект1С);
      Процедура процедура = (Процедура) Compiler.GetParent(node);
      CodeStruct codeStruct1 = node;
      string str1 = codeStruct1.Text + "(";
      codeStruct1.Text = str1;
      if (num > 1)
      {
        for (int index4 = index1 + 1; index4 < index3; ++index4)
        {
          CodeStruct codeStruct2 = node;
          string str2 = codeStruct2.Text + body.Items[index4].Text;
          codeStruct2.Text = str2;
          if (процедура.Objects.ContainsKey((object) body.Items[index4].Text))
          {
            if (процедура.Objects[(object) body.Items[index4].Text] != null)
              node.Objects.Add((object) new Объект1С(body.Items[index4].Text, (Compiler.TypeObject1C) процедура.Objects[(object) body.Items[index4].Text]));
            else
              node.Objects.Add((object) new Объект1С(body.Items[index4].Text));
          }
          else
            node.Objects.Add((object) new Объект1С(body.Items[index4].Text));
        }
      }
      CodeStruct codeStruct3 = node;
      string str3 = codeStruct3.Text + ");";
      codeStruct3.Text = str3;
      node.Object1C = (object) сообщить;
      node.TypeObject1C = Compiler.TypeObject1C.Сообщить;
      node.OperatorType = CodeStruct.TypeOperator.ВызовПроцедуры;
      return index2;
    }

    internal static object GetParent(CodeStruct node)
    {
      if (node == null)
        return (object) null;
      if (node.Object1C is Процедура)
        return node.Object1C;
      bool flag = false;
      while (!flag)
      {
        if (node.Parent == null)
          return (object) null;
        node = (CodeStruct) node.Parent;
        if (node.Object1C is Процедура)
          return node.Object1C;
      }
      return (object) null;
    }

    private static int GetFullString(Hashtable objects, CodeStruct node, ListView body, int start)
    {
      if (body.Items.Count <= start)
        throw new Exception("Ожидается символ ';'");
      ListViewItem itemWithText1 = body.FindItemWithText(";", false, start);
      if (itemWithText1 == null)
        throw new Exception("Ожидается символ ';'");
      int index1 = itemWithText1.Index;
      ListViewItem itemWithText2 = body.FindItemWithText("=", false, start);
      if (itemWithText2 != null)
      {
        node.OperatorType = CodeStruct.TypeOperator.Присваивание;
        int index2 = itemWithText2.Index;
        string name = "";
        for (int index3 = start - 1; index3 < index2; ++index3)
        {
          ListViewItem listViewItem = body.Items[index3];
          name = name + listViewItem.Text;
        }
        if (!objects.ContainsKey((object) name))
          objects.Add((object) name, (object) null);
        Объект1С объект1С1 = new Объект1С(name);
        node.Objects.Add((object) объект1С1);
        node.Objects.Add((object) new Объект1С("=", Compiler.TypeObject1C.Равно));
        for (int index3 = index2 + 1; index3 < index1; ++index3)
        {
          ListViewItem listViewItem = body.Items[index3];
          if (listViewItem.Text.ToLower() == "новый")
          {
            node.Objects.Add((object) new Объект1С(listViewItem.Text, Compiler.TypeObject1C.Новый));
            ++index3;
            string text = body.Items[index3].Text;
            switch (text.ToLower())
            {
              case "списокзначений":
                node.Objects.Add((object) new Объект1С(text, Compiler.TypeObject1C.СписокЗначений));
                объект1С1.Тип = Compiler.TypeObject1C.СписокЗначений;
                if (objects.ContainsKey((object) name))
                {
                  objects[(object) name] = (object) Compiler.TypeObject1C.СписокЗначений;
                  break;
                }
                else
                  break;
              case "таблицазначений":
                node.Objects.Add((object) new Объект1С(text, Compiler.TypeObject1C.ТаблицаЗначений));
                объект1С1.Тип = Compiler.TypeObject1C.ТаблицаЗначений;
                if (objects.ContainsKey((object) name))
                {
                  objects[(object) name] = (object) Compiler.TypeObject1C.ТаблицаЗначений;
                  break;
                }
                else
                  break;
              default:
                node.Objects.Add((object) new Объект1С(text));
                break;
            }
          }
          else
          {
            Объект1С объект1С2 = new Объект1С(body.Items[index3].Text);
            node.Objects.Add((object) объект1С2);
          }
          node.Text = "";
          foreach (Объект1С объект1С2 in node.Objects)
          {
            CodeStruct codeStruct = node;
            string str = codeStruct.Text + объект1С2.Имя + " ";
            codeStruct.Text = str;
          }
          node.Text = node.Text.TrimEnd(new char[0]) + ";";
        }
        if (!Global.ravno.ContainsKey((object) объект1С1.Имя))
          Global.ravno.Add((object) объект1С1.Имя, (object) объект1С1);
        else
          Global.ravno[(object) объект1С1.Имя] = (object) объект1С1;
      }
      else if (node.Object1C == null)
      {
        for (int index2 = start - 1; index2 < index1; ++index2)
        {
          ListViewItem listViewItem = body.Items[index2];
          if (objects.ContainsKey((object) listViewItem.Text))
          {
            if (objects[(object) listViewItem.Text] != null)
              node.Objects.Add((object) new Объект1С(listViewItem.Text, (Compiler.TypeObject1C) objects[(object) listViewItem.Text]));
            else
              node.Objects.Add((object) new Объект1С(listViewItem.Text));
          }
          else
            node.Objects.Add((object) new Объект1С(listViewItem.Text));
        }
        node.OperatorType = CodeStruct.TypeOperator.ВызовПроцедуры;
        node.Text = "";
        foreach (Объект1С объект1С in node.Objects)
        {
          CodeStruct codeStruct = node;
          string str = codeStruct.Text + объект1С.Имя;
          codeStruct.Text = str;
        }
        node.Text = node.Text.TrimEnd(new char[0]) + ";";
      }
      else
      {
        string str1 = "";
        for (int index2 = start; index2 < index1; ++index2)
        {
          ListViewItem listViewItem = body.Items[index2];
          str1 = str1 + listViewItem.Text;
        }
        CodeStruct codeStruct = node;
        string str2 = codeStruct.Text + str1 + ";";
        codeStruct.Text = str2;
      }
      return index1;
    }

    private int AddCycle(Hashtable objects, CodeStruct node, ListView body, int start)
    {
      int num = body.Items.Count;
      ListView body1 = new ListView();
      for (int index = start; index < body.Items.Count; ++index)
      {
        ListViewItem listViewItem = body.Items[index];
        if (listViewItem.Text.ToLower() == "конеццикла")
        {
          num = index;
          break;
        }
        else
        {
          body1.Items.Add((ListViewItem) listViewItem.Clone());
          this.txtTokens.AppendText(listViewItem.Name + ": " + listViewItem.Text + Environment.NewLine);
        }
      }
      node.OperatorType = CodeStruct.TypeOperator.Цикл;
      if (body1.Items[0].Text.ToLower() == "каждого")
      {
        ListViewItem itemWithText1 = body1.FindItemWithText("из");
        if (itemWithText1 == null)
          throw new Exception("Ожидается выражение 'из'");
        int index1 = itemWithText1.Index;
        string str1 = "";
        string str2 = "";
        for (int index2 = 1; index2 < index1; ++index2)
          str1 = str1 + body1.Items[index2].Text;
        ListViewItem itemWithText2 = body1.FindItemWithText("цикл");
        if (itemWithText2 == null)
          throw new Exception("Ожидается выражение 'цикл'");
        for (int index2 = index1 + 1; index2 < itemWithText2.Index; ++index2)
          str2 = str2 + body1.Items[index2].Text;
        Цикл цикл = new Цикл()
        {
          ObjectCollection = str1,
          Collection = str2
        };
        node.Object1C = (object) цикл;
        node.TypeObject1C = Compiler.TypeObject1C.Цикл;
        node.Text = цикл.ToString();
        this.ParseBody(objects, (TreeNode) node, body1, itemWithText2.Index + 1);
      }
      else
      {
        ListViewItem itemWithText1 = body1.FindItemWithText("=");
        if (itemWithText1 == null)
          throw new Exception("Ожидается символ '='");
        int index1 = itemWithText1.Index;
        string str = "";
        string name1 = "";
        string name2 = "";
        for (int index2 = 0; index2 < index1; ++index2)
          str = str + body1.Items[index2].Text;
        ListViewItem itemWithText2 = body1.FindItemWithText("по");
        if (itemWithText2 == null)
          throw new Exception("Ожидается выражение 'по'");
        int index3 = itemWithText2.Index;
        for (int index2 = index1 + 1; index2 < itemWithText2.Index; ++index2)
          name1 = name1 + body1.Items[index2].Text;
        ListViewItem itemWithText3 = body1.FindItemWithText("цикл");
        if (itemWithText3 == null)
          throw new Exception("Ожидается выражение 'цикл'");
        for (int index2 = index3 + 1; index2 < itemWithText3.Index; ++index2)
          name2 = name2 + body1.Items[index2].Text;
        Цикл цикл = new Цикл()
        {
          ObjectCollection = str,
          ValueBegin = new Объект1С(name1),
          ValueMax = new Объект1С(name2)
        };
        node.Object1C = (object) цикл;
        node.TypeObject1C = Compiler.TypeObject1C.Цикл;
        node.Text = цикл.ToString();
        this.ParseBody(objects, (TreeNode) node, body1, itemWithText3.Index + 1);
      }
      return num;
    }

    private void tsParse_Click(object sender, EventArgs e)
    {
      this.ParseCode();
    }

    private void tsCompile_Click(object sender, EventArgs e)
    {
      this.CompileCode("code1C.exe", true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Compiler));
      this.menuStrip1 = new MenuStrip();
      this.файлToolStripMenuItem = new ToolStripMenuItem();
      this.toolStrip1 = new ToolStrip();
      this.tsParse = new ToolStripButton();
      this.tsCompile = new ToolStripButton();
      this.statusStrip1 = new StatusStrip();
      this.splitContainer1 = new SplitContainer();
      this.splitContainer3 = new SplitContainer();
      this.txtCode1C = new RichTextBox1C();
      this.txtCode = new TextBox();
      this.splitContainer2 = new SplitContainer();
      this.txtTokens = new TextBox();
      this.treeCode = new TreeView();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      this.menuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.файлToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(1151, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
      this.файлToolStripMenuItem.Size = new Size(45, 20);
      this.файлToolStripMenuItem.Text = "Файл";
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.tsParse,
        (ToolStripItem) this.tsCompile
      });
      this.toolStrip1.Location = new Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new Size(1151, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      this.tsParse.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.tsParse.Image = (Image) componentResourceManager.GetObject("tsParse.Image");
      this.tsParse.ImageTransparentColor = Color.Magenta;
      this.tsParse.Name = "tsParse";
      this.tsParse.Size = new Size(86, 22);
      this.tsParse.Text = "Разобрать код";
      this.tsParse.Click += new EventHandler(this.tsParse_Click);
      this.tsCompile.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.tsCompile.Image = (Image) componentResourceManager.GetObject("tsCompile.Image");
      this.tsCompile.ImageTransparentColor = Color.Magenta;
      this.tsCompile.Name = "tsCompile";
      this.tsCompile.Size = new Size(96, 22);
      this.tsCompile.Text = "Скомпилировать";
      this.tsCompile.Click += new EventHandler(this.tsCompile_Click);
      this.statusStrip1.Location = new Point(0, 639);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(1151, 22);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(0, 49);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.splitContainer3);
      this.splitContainer1.Panel2.Controls.Add((Control) this.splitContainer2);
      this.splitContainer1.Size = new Size(1151, 590);
      this.splitContainer1.SplitterDistance = 553;
      this.splitContainer1.TabIndex = 3;
      this.splitContainer3.Dock = DockStyle.Fill;
      this.splitContainer3.Location = new Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = Orientation.Horizontal;
      this.splitContainer3.Panel1.Controls.Add((Control) this.txtCode1C);
      this.splitContainer3.Panel2.Controls.Add((Control) this.txtCode);
      this.splitContainer3.Size = new Size(553, 590);
      this.splitContainer3.SplitterDistance = 272;
      this.splitContainer3.TabIndex = 0;
      this.txtCode1C.DetectTextChanged = true;
      this.txtCode1C.Dock = DockStyle.Fill;
      this.txtCode1C.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.txtCode1C.Location = new Point(0, 0);
      this.txtCode1C.Name = "txtCode1C";
      this.txtCode1C.PrevSelect = 0;
      this.txtCode1C.Size = new Size(553, 272);
      this.txtCode1C.TabIndex = 1;
      this.txtCode1C.Text = componentResourceManager.GetString("txtCode1C.Text");
      this.txtCode1C.Version1C = Version1C.v8;
      this.txtCode.Dock = DockStyle.Fill;
      this.txtCode.Location = new Point(0, 0);
      this.txtCode.Multiline = true;
      this.txtCode.Name = "txtCode";
      this.txtCode.ScrollBars = ScrollBars.Both;
      this.txtCode.Size = new Size(553, 314);
      this.txtCode.TabIndex = 3;
      this.splitContainer2.Dock = DockStyle.Fill;
      this.splitContainer2.Location = new Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = Orientation.Horizontal;
      this.splitContainer2.Panel1.Controls.Add((Control) this.txtTokens);
      this.splitContainer2.Panel2.Controls.Add((Control) this.treeCode);
      this.splitContainer2.Size = new Size(594, 590);
      this.splitContainer2.SplitterDistance = 322;
      this.splitContainer2.TabIndex = 0;
      this.txtTokens.Dock = DockStyle.Fill;
      this.txtTokens.Location = new Point(0, 0);
      this.txtTokens.Multiline = true;
      this.txtTokens.Name = "txtTokens";
      this.txtTokens.ScrollBars = ScrollBars.Both;
      this.txtTokens.Size = new Size(594, 322);
      this.txtTokens.TabIndex = 2;
      this.treeCode.Dock = DockStyle.Fill;
      this.treeCode.Location = new Point(0, 0);
      this.treeCode.Name = "treeCode";
      this.treeCode.Size = new Size(594, 264);
      this.treeCode.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1151, 661);
      this.Controls.Add((Control) this.splitContainer1);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.toolStrip1);
      this.Controls.Add((Control) this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Compiler";
      this.Text = "Compiler 1C";
      this.Load += new EventHandler(this.MainForm_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public enum TypeObject1C
    {
      Неопределено,
      Новый,
      Равно,
      Строка,
      Число,
      Переменная,
      Процедура,
      Функция,
      Цикл,
      Условие,
      СписокЗначений,
      ЭлементСпискаЗначений,
      ТаблицаЗначений,
      СтрокаТаблицыЗначений,
      КолонкаТаблицыЗначений,
      Булево,
      НЕ,
      Сообщить,
    }
  }
}
