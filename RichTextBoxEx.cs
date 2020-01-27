using System;
using System.Drawing;
using System.Windows.Forms;

namespace JNote
{
    public class RichTextBoxEx : RichTextBox
    {
        public RichTextBox ln_txtbbox;
        public RichTextBoxEx()
        {
            new_ln_textBox();

            // set configurations
            this.TextChanged += mainTextBox_TextChanged;
            this.FontChanged += mainTextBox_FontChanged;
            this.VScroll += mainTextBox_VScroll;
            this.SelectionChanged += mainTextBox_SelectionChanged;

            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(255, 16, 16, 16);
            this.ForeColor = Color.White;
            this.Font = new Font("Calibri", 12);
            
            this.Select();
            this.AddLineNumbers();
        }
        private void new_ln_textBox()
        {
            // set linumber text box
            ln_txtbbox = new RichTextBox();
            ln_txtbbox.Dock = DockStyle.Left;
            ln_txtbbox.BackColor = Color.Black;
            ln_txtbbox.ForeColor = Color.White;
            ln_txtbbox.Font = new Font("Calibri", 12);
            ln_txtbbox.ScrollBars = 0;
            ln_txtbbox.MouseDown += ln_txtbbox_MouseDown;
        }
        private void ln_txtbbox_MouseDown(object sender, MouseEventArgs e)
        {
            Select();
            ln_txtbbox.DeselectAll();
        }

        private void mainTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = GetPositionFromCharIndex(SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void mainTextBox_VScroll(object sender, EventArgs e)
        {
            ln_txtbbox.Text = "";
            AddLineNumbers();
            ln_txtbbox.Invalidate();
        }

        private void mainTextBox_FontChanged(object sender, EventArgs e)
        {
            //ln_txtbbox.Font = Font;
            Select();
            AddLineNumbers();
        }

        private void mainTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Text == "")
            {
                AddLineNumbers();
            }

            // urStack.Push(ref mainTextBox, cursorPos);

            // button toggles
            // undoToolStripMenuItem.Enabled = urStack.UndoCount > 1;
            // redoToolStripMenuItem.Enabled = urStack.RedoCount > 0;

            //if (Text.Length > 0)
            //{
            //    cutToolStripMenuItem.Enabled = true;
            //    copyToolStripMenuItem.Enabled = true;
            //    selectAllToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
            //    selectAllToolStripMenuItem.Enabled = false;
            //    cutToolStripMenuItem.Enabled = false;
            //    copyToolStripMenuItem.Enabled = false;
            //}
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from MainTextBox    
            int First_Index = this.GetCharIndexFromPosition(pt);
            int First_Line = this.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from MainTextBox    
            int Last_Index = this.GetCharIndexFromPosition(pt);
            int Last_Line = this.GetLineFromCharIndex(Last_Index);
            // set Center alignment to ln_txtbbox    
            ln_txtbbox.SelectionAlignment = HorizontalAlignment.Center;
            // set ln_txtbbox text to null & width to getWidth() function value    
            ln_txtbbox.Text = "";
            ln_txtbbox.Width = getWidth();
            // now add each line number to ln_txtbbox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                ln_txtbbox.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of MainTextBox    
            int line = this.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)this.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)this.Font.Size;
            }
            else
            {
                w = 50 + (int)this.Font.Size;
            }

            return w;
        }
    }
}
