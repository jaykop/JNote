using System;
using System.IO;
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
            
            // app title
            this.Text = "JNote";

            // assign custom renderer
            menuStrip.Renderer = new MenuRenderer();
            
            // redo/undo
            // urStack = new UndoRedoStack();
            // urStack.FirstPush(ref GetRichTextBox());
            // undoToolStripMenuItem.Enabled = false;
            // redoToolStripMenuItem.Enabled = false;
        }
        private RichTextBoxEx GetRichTextBox()
        {
            TabPage tp = tabControl.SelectedTab;
            RichTextBoxEx rtb = null;

            if (tp != null)
            {
                rtb = tp.Controls[0] as RichTextBoxEx;
            }

            return rtb;
        }
        private void newTab()
        {
            // genate a new tab page
            TabPage tp = new TabPage("New Document");
            tp.BackColor = Color.Black;
            tp.ForeColor = Color.White;

            // new text boxes for content and line numbers
            RichTextBoxEx rtb = new RichTextBoxEx();

            // set configurations
            rtb.Dock = DockStyle.Fill;
            rtb.BackColor = Color.FromArgb(255, 16, 16, 16);
            rtb.Font = new Font("Calibri", 12);
            rtb.ForeColor = Color.White;

            tp.Controls.Add(rtb);
            tp.Controls.Add(rtb.ln_txtbbox);
            tabControl.Controls.Add(tp);
            tabControl.SelectedTab = tp;

            cursorPos = GetRichTextBox().SelectionStart;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) { newTab(); }
        private void newFileButton_Click(object sender, EventArgs e) { newTab(); }
        private void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (.txt)|*.txt";
            ofd.Title = "Open a file...";
            string filename = null;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // new text boxes for content and line numbers
                RichTextBoxEx rtb = new RichTextBoxEx();

                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                filename = Path.GetFileName(ofd.FileName);
                rtb.Text = sr.ReadToEnd();
                this.Text = "JNote - " + ofd.FileName;
                sr.Close();

                // genate a new tab page
                TabPage tp = new TabPage(filename);
                tp.BackColor = Color.Black;
                tp.ForeColor = Color.White;

                tp.Controls.Add(rtb);
                tp.Controls.Add(rtb.ln_txtbbox);
                tabControl.Controls.Add(tp);
                tabControl.SelectedTab = tp;

                cursorPos = GetRichTextBox().SelectionStart;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) { openFile(); }
        private void openFileButton_Click(object sender, EventArgs e) { openFile(); }
        private void saveFile()
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
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { saveFile(); }
        private void saveButton_Click(object sender, EventArgs e) { saveFile(); }
        private void saveAsFile()
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
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) { saveAsFile(); }
        private void saveAsButton_Click(object sender, EventArgs e) { saveAsFile(); }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This will close the current process
            Application.Exit(); // this.Close();
        }
        private void aboutJnoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Rights Reserved By Jay Jeong\nJNote\nContact: jaykop.jy@gmail.com",
            "Help",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) { GetRichTextBox().Cut(); }
        private void cutButton_Click(object sender, EventArgs e) { GetRichTextBox().Cut(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) { GetRichTextBox().Copy(); }
        private void copyButton_Click(object sender, EventArgs e) { GetRichTextBox().Copy(); }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) { GetRichTextBox().Paste(); }
        private void pasteButton_Click(object sender, EventArgs e) { GetRichTextBox().Paste(); }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) { GetRichTextBox().SelectedText = ""; }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) { GetRichTextBox().SelectAll(); }
        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e) { GetRichTextBox().Text += DateTime.Now; }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // todo
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // todo
            // GetRichTextBox().Find();
        }
        private void findButton_Click(object sender, EventArgs e)        {        }
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)       {        }
        private void replaceButton_Click(object sender, EventArgs e)        {        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (GetRichTextBox() != null)
            {
                GetRichTextBox().Select();
                GetRichTextBox().AddLineNumbers();
            }
            else 
                newTab();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (GetRichTextBox() != null)
                GetRichTextBox().AddLineNumbers();
        }

        //private void mainTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    // urStack.Push(ref mainTextBox, cursorPos);

        //    // button toggles
        //    // undoToolStripMenuItem.Enabled = urStack.UndoCount > 1;
        //    // redoToolStripMenuItem.Enabled = urStack.RedoCount > 0;

        //    if (GetRichTextBox().Text.Length > 0)
        //    {
        //        cutToolStripMenuItem.Enabled = true;
        //        copyToolStripMenuItem.Enabled = true;
        //        selectAllToolStripMenuItem.Enabled = true;
        //    }
        //    else
        //    {
        //        selectAllToolStripMenuItem.Enabled = false;
        //        cutToolStripMenuItem.Enabled = false;
        //        copyToolStripMenuItem.Enabled = false;
        //    }
        //}

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // simple Undo
            //GetRichTextBox().Undo();
            //undoToolStripMenuItem.Enabled = false;
            //redoToolStripMenuItem.Enabled = true;

            // GetRichTextBox().Text = urStack.Undo(GetRichTextBox().Text, ref cursorPos);
        }
        private void undoButton_Click(object sender, EventArgs e)
        {
            // GetRichTextBox().Text = urStack.Undo(GetRichTextBox().Text, ref cursorPos);
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // simple Redo
            //GetRichTextBox().Redo();
            //undoToolStripMenuItem.Enabled = true;
            //redoToolStripMenuItem.Enabled = false;

            // GetRichTextBox().Text = urStack.Redo(GetRichTextBox().Text, ref cursorPos);
        }
        private void redoButton_Click(object sender, EventArgs e)
        {
            // GetRichTextBox().Text = urStack.Redo(GetRichTextBox().Text, ref cursorPos);
        }
    }
}
