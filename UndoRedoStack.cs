using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JNote
{
    public class UndoRedoStack
    {
        private Stack<string> undo, redo;
        private Stack<int> p_undo, p_redo;
        private bool undoing, redoing;

        public int UndoCount
        {
            get
            {
                return undo.Count;
            }
        }
        public int RedoCount
        {
            get
            {
                return redo.Count;
            }
        }

        public UndoRedoStack()
        {
            Reset();
            undoing = redoing = false;
        }
        public void Reset()
        {
            undo = new Stack<string>();
            redo = new Stack<string>();
            p_undo = new Stack<int>();
            p_redo = new Stack<int>();
        }

        //public string Do(string input)
        //{
        //    undo.Push(input); // add new input to undo on the stack
        //    redo.Clear(); // Once we issue a new command, the redo stack clears
        //    return input;
        //}

        // return the final content to undo
        public string Undo(string input, ref int sStart)
        {
            // from the undo stack
            if (undo.Count > 1)
            {
                undoing = true;

                // setup undo/redo stack
                string pop = undo.Pop();
                string output = undo.First();
                if (redo.Count == 0 
                    || redo.First() != pop)
                    redo.Push(pop);

                // setup text cursor
                int p_pop = p_undo.Pop();
                int p_output = p_undo.First();
                if (p_redo.Count == 0
                    || p_redo.First() != p_pop)
                    p_redo.Push(p_pop);

                sStart = p_output;

                // return output
                return output;
            }

            else
            {
                p_undo.Clear();
                undo.Clear();
                undoing = false;
                return input;
            }
        }

        // return the final content to redo
        public string Redo(string input, ref int sStart)
        {
            // from the redo stack
            if (redo.Count > 0)
            {
                redoing = true;

                // setup undo/redo stack
                string output = redo.Pop();
                if (undo.Count == 0
                    || undo.First() != input)
                    undo.Push(input);

                // setup text cursor
                int p_output = p_redo.Pop();
                if (p_undo.Count == 0
                    || p_undo.First() != sStart)
                    p_undo.Push(sStart);

                sStart = p_output;

                // return output
                return output;
            }

            else
            {
                redoing = false;
                return input;
            }
        }

        public void FirstPush(ref RichTextBox textBox)
        {
            undo.Push(textBox.Text);
            p_undo.Push(textBox.SelectionStart);
        }

        public void Push(ref RichTextBox textBox, int sStart)
        {
            if (!redoing && !undoing
                && undo.First() != textBox.Text)
            {
                undo.Push(textBox.Text);
                p_undo.Push(textBox.SelectionStart);
                redo.Clear(); // Anytime we push a new command, the redo stack clears
            }
            else
            {
                textBox.SelectionStart = sStart;
                textBox.SelectionLength = 0;
                textBox.Select();
            }

            redoing = undoing = false;
        }

        //public string UnPush()
        //{
        //    if (undo.Count > 0)
        //    {
        //        string pop = undo.Pop();
        //        redo.Push(pop);
        //        return pop;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //public string RePush()
        //{
        //    if (redo.Count > 0)
        //    {
        //        string pop = redo.Pop();
        //        undo.Push(pop);
        //        return pop;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}
