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
            cursorPos = MainTextBox.SelectionStart;

            // assign custom renderer
            menuStrip1.Renderer = new MenuRenderer();

            // redo/undo
            urStack = new UndoRedoStack();
            urStack.FirstPush(ref MainTextBox);
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This will close the current process
            Application.Exit(); // this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the main text box
            // MainTextBox.Text = "";
            MainTextBox.Clear();
        }

        private void aboutJnoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Rights Reserved By Jay Jeong\nJNote\nContact: jaykop.jy@gmail.com",
            "Help",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }

        private void MainTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MainTextBox.Text == "")
            {
                AddLineNumbers();
            }

            urStack.Push(ref MainTextBox, cursorPos);

            // button toggles
            undoToolStripMenuItem.Enabled = urStack.UndoCount > 1;
            redoToolStripMenuItem.Enabled = urStack.RedoCount > 0;

            if (MainTextBox.Text.Length > 0)
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
            //MainTextBox.Undo();
            //undoToolStripMenuItem.Enabled = false;
            //redoToolStripMenuItem.Enabled = true;

            MainTextBox.Text = urStack.Undo(MainTextBox.Text, ref cursorPos);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // simple Redo
            //MainTextBox.Redo();
            //undoToolStripMenuItem.Enabled = true;
            //redoToolStripMenuItem.Enabled = false;

            MainTextBox.Text = urStack.Redo(MainTextBox.Text, ref cursorPos);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTextBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTextBox.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTextBox.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTextBox.Text += DateTime.Now;
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
                MainTextBox.Text = sr.ReadToEnd();
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
                    sw.Write(MainTextBox.Text);
                    this.Text = "JNote - " + sfd.FileName;
                    sw.Close();
                }
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(MainMenuStrip.Name);
                sw.Write(MainTextBox.Text);
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
                sw.Write(MainTextBox.Text);
                this.Text = "JNote - " + sfd.FileName;
                sw.Close();
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // todo
            // MainTextBox.Find();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = MainTextBox.Font;
            MainTextBox.Select();
            AddLineNumbers();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // new file button
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MainTextBox.Clear();
        }

        // open file button
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (.txt)|*.txt";
            ofd.Title = "Open a file";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                MainTextBox.Text = sr.ReadToEnd();
                this.Text = "JNote - " + ofd.FileName;
                sr.Close();
            }
        }

        // save button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.Text == "JNote - New")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files (.txt)|*.txt";
                sfd.Title = "Save the file";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                    sw.Write(MainTextBox.Text);
                    this.Text = "JNote - " + sfd.FileName;
                    sw.Close();
                }
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(MainMenuStrip.Name);
                sw.Write(MainTextBox.Text);
                sw.Close();
            }
        }

        // save as button
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files (.*)|*.*";
            sfd.Title = "Save the file as";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName);
                sw.Write(MainTextBox.Text);
                this.Text = "JNote - " + sfd.FileName;
                sw.Close();
            }
        }

        // cut button
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            MainTextBox.Cut();
        }

        // copy button
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            MainTextBox.Copy();
        }

        // paste button
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            MainTextBox.Paste();
        }

        // find button
        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        // replcae button
        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        // undo button
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            MainTextBox.Text = urStack.Undo(MainTextBox.Text, ref cursorPos);
        }

        // redo button
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            MainTextBox.Text = urStack.Redo(MainTextBox.Text, ref cursorPos);
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from MainTextBox    
            int First_Index = MainTextBox.GetCharIndexFromPosition(pt);
            int First_Line = MainTextBox.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from MainTextBox    
            int Last_Index = MainTextBox.GetCharIndexFromPosition(pt);
            int Last_Line = MainTextBox.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of MainTextBox    
            int line = MainTextBox.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)MainTextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)MainTextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)MainTextBox.Font.Size;
            }

            return w;
        }

        private void MainTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = MainTextBox.GetPositionFromCharIndex(MainTextBox.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void MainTextBox_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void MainTextBox_FontChanged(object sender, EventArgs e)
        {
            //LineNumberTextBox.Font = MainTextBox.Font;
            MainTextBox.Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            MainTextBox.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void LineNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
