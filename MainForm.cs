using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JNote
{
    public partial class MainForm : Form
    {
        private UndoRedoStack urStack;
        private int cursorPos;

        public MainForm()
        {
            InitializeComponent();
            
            // note title
            this.Text = "JNote - New";

            //restore
            // cursorPos = GetRichTextBox().SelectionStart;

            // assign custom renderer
            menuStrip.Renderer = new MenuRenderer();

            // redo/undo
            urStack = new UndoRedoStack();
            // urStack.FirstPush(ref GetRichTextBox());
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This will close the current process
            Application.Exit(); // this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the main text box
            // GetRichTextBox().Text = "";
            // GetRichTextBox().Clear();

            // GetRichTextBox().Clear();
            TabPage tp = new TabPage("New Document");
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;

            tp.Controls.Add(rtb);
            tabControl.Controls.Add(tp);
            tabControl.SelectedTab = tp;
        }

        private void aboutJnoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Rights Reserved By Jay Jeong\nJNote\nContact: jaykop.jy@gmail.com",
            "Help",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }

        private void mainTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GetRichTextBox().Text == "")
            {
                AddLineNumbers();
            }

            // urStack.Push(ref mainTextBox, cursorPos);

            // button toggles
            undoToolStripMenuItem.Enabled = urStack.UndoCount > 1;
            redoToolStripMenuItem.Enabled = urStack.RedoCount > 0;

            if (GetRichTextBox().Text.Length > 0)
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
            }
            else
            {
                selectAllToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // simple Undo
            //GetRichTextBox().Undo();
            //undoToolStripMenuItem.Enabled = false;
            //redoToolStripMenuItem.Enabled = true;

            GetRichTextBox().Text = urStack.Undo(GetRichTextBox().Text, ref cursorPos);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // simple Redo
            //GetRichTextBox().Redo();
            //undoToolStripMenuItem.Enabled = true;
            //redoToolStripMenuItem.Enabled = false;

            GetRichTextBox().Text = urStack.Redo(GetRichTextBox().Text, ref cursorPos);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Text += DateTime.Now;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // todo
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (.txt)|*.txt";
            ofd.Title = "Open a file...";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                GetRichTextBox().Text = sr.ReadToEnd();
                this.Text = "JNote - " + ofd.FileName;
                sr.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text == "JNote - New")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files (.txt)|*.txt";
                sfd.Title = "Save the file...";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                    sw.Write(GetRichTextBox().Text);
                    this.Text = "JNote - " + sfd.FileName;
                    sw.Close();
                }
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(MainMenuStrip.Name);
                sw.Write(GetRichTextBox().Text);
                sw.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files (.*)|*.*";
            sfd.Title = "Save the file as...";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                sw.Write(GetRichTextBox().Text);
                this.Text = "JNote - " + sfd.FileName;
                sw.Close();
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // todo
            // GetRichTextBox().Find();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //lineNumberTextBox.Font = GetRichTextBox().Font;
            //GetRichTextBox().Select();
            //AddLineNumbers();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // new file button
        private void newFileButton_Click(object sender, EventArgs e)
        {
            // GetRichTextBox().Clear();
            TabPage tp = new TabPage("New Document");
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;

            tp.Controls.Add(rtb);
            tabControl.Controls.Add(tp);
            tabControl.SelectedTab = tp;
        }

        // open file button
        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (.txt)|*.txt";
            ofd.Title = "Open a file";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                GetRichTextBox().Text = sr.ReadToEnd();
                this.Text = "JNote - " + ofd.FileName;
                sr.Close();
            }
        }

        // save button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.Text == "JNote - New")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files (.txt)|*.txt";
                sfd.Title = "Save the file";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                    sw.Write(GetRichTextBox().Text);
                    this.Text = "JNote - " + sfd.FileName;
                    sw.Close();
                }
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(MainMenuStrip.Name);
                sw.Write(GetRichTextBox().Text);
                sw.Close();
            }
        }

        // save as button
        private void saveAsButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files (.*)|*.*";
            sfd.Title = "Save the file as";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                sw.Write(GetRichTextBox().Text);
                this.Text = "JNote - " + sfd.FileName;
                sw.Close();
            }
        }

        // cut button
        private void cutButton_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Cut();
        }

        // copy button
        private void copyButton_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Copy();
        }

        // paste button
        private void pasteButton_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Paste();
        }

        // find button
        private void findButton_Click(object sender, EventArgs e)
        {

        }

        // replcae button
        private void replaceButton_Click(object sender, EventArgs e)
        {

        }

        // undo button
        private void undoButton_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Text = urStack.Undo(GetRichTextBox().Text, ref cursorPos);
        }

        // redo button
        private void redoButton_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Text = urStack.Redo(GetRichTextBox().Text, ref cursorPos);
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from MainTextBox    
            int First_Index = GetRichTextBox().GetCharIndexFromPosition(pt);
            int First_Line = GetRichTextBox().GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from MainTextBox    
            int Last_Index = GetRichTextBox().GetCharIndexFromPosition(pt);
            int Last_Line = GetRichTextBox().GetLineFromCharIndex(Last_Index);
            // set Center alignment to lineNumberTextBox    
            lineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set lineNumberTextBox text to null & width to getWidth() function value    
            lineNumberTextBox.Text = "";
            lineNumberTextBox.Width = getWidth();
            // now add each line number to lineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                lineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of MainTextBox    
            int line = GetRichTextBox().Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)GetRichTextBox().Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)GetRichTextBox().Font.Size;
            }
            else
            {
                w = 50 + (int)GetRichTextBox().Font.Size;
            }

            return w;
        }

        private void mainTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = GetRichTextBox().GetPositionFromCharIndex(GetRichTextBox().SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void mainTextBox_VScroll(object sender, EventArgs e)
        {
            lineNumberTextBox.Text = "";
            AddLineNumbers();
            lineNumberTextBox.Invalidate();
        }

        private void mainTextBox_FontChanged(object sender, EventArgs e)
        {
            //lineNumberTextBox.Font = GetRichTextBox().Font;
            GetRichTextBox().Select();
            AddLineNumbers();
        }

        private void lineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            GetRichTextBox().Select();
            lineNumberTextBox.DeselectAll();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private RichTextBox GetRichTextBox()
        {
            RichTextBox rtb = null;
            TabPage tp = tabControl.SelectedTab;
            
            if (tp != null)
            {
                rtb = tp.Controls[0] as RichTextBox;
            }

            return rtb;
        }
    }
}
