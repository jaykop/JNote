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

        public MainForm()
        {
            InitializeComponent();
            this.Text = "JNote - New";
            
            urStack = new UndoRedoStack();
            urStack.FirstPush(MainTextBox.Text);
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
            urStack.Push(MainTextBox.Text);

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

            MainTextBox.Text = urStack.Undo(MainTextBox.Text);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // simple Redo
            //MainTextBox.Redo();
            //undoToolStripMenuItem.Enabled = true;
            //redoToolStripMenuItem.Enabled = false;

            MainTextBox.Text = urStack.Redo(MainTextBox.Text);
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
            ofd.Title = "Open a file...";

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

        // save as button
        private void toolStripButton3_Click(object sender, EventArgs e)
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
            MainTextBox.Text = urStack.Undo(MainTextBox.Text);
        }

        // redo button
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            MainTextBox.Text = urStack.Redo(MainTextBox.Text);
        }
    }
}
