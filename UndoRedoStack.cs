using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNote
{
    public class UndoRedoStack
    {
        private Stack<string> _Undo;
        private Stack<string> _Redo;
        private bool undoing, redoing;

        public int UndoCount
        {
            get
            {
                return _Undo.Count;
            }
        }
        public int RedoCount
        {
            get
            {
                return _Redo.Count;
            }
        }

        public UndoRedoStack()
        {
            Reset();
            undoing = redoing = false;
        }
        public void Reset()
        {
            _Undo = new Stack<string>();
            _Redo = new Stack<string>();
        }

        public string Do(string input)
        {
            _Undo.Push(input); // add new input to undo on the stack
            _Redo.Clear(); // Once we issue a new command, the redo stack clears
            return input;
        }

        // return the final content to undo
        public string Undo(string input)
        {
            // from the undo stack
            if (_Undo.Count > 1)
            {
                undoing = true;

                // setup undo/redo stack
                string pop = _Undo.Pop();
                string output = _Undo.First();
                if (_Redo.Count == 0 
                    || _Redo.First() != pop)
                    _Redo.Push(pop);

                // return output
                return output;
            }

            else
            {
                _Undo.Clear();
                undoing = false;
                return input;
            }
        }

        // return the final content to redo
        public string Redo(string input)
        {
            // from the redo stack
            if (_Redo.Count > 0)
            {
                redoing = true;

                // setup undo/redo stack
                string output = _Redo.Pop();
                if (_Undo.Count == 0
                    || _Undo.First() != input)
                    _Undo.Push(input);

                // return output
                return output;
            }

            else
            {
                redoing = false;
                return input;
            }
        }

        public void FirstPush(string input)
        {
            _Undo.Push(input);
        }

        public void Push(string input)
        {
            if (!redoing && !undoing
                && _Undo.First() != input)
            {
                _Undo.Push(input);
                _Redo.Clear(); // Anytime we push a new command, the redo stack clears
            }

            redoing = undoing = false;
        }

        public string UnPush()
        {
            if (_Undo.Count > 0)
            {
                string pop = _Undo.Pop();
                _Redo.Push(pop);
                return pop;
            }
            else
            {
                return null;
            }
        }
        public string RePush()
        {
            if (_Redo.Count > 0)
            {
                string pop = _Redo.Pop();
                _Undo.Push(pop);
                return pop;
            }
            else
            {
                return null;
            }
        }
    }
}
