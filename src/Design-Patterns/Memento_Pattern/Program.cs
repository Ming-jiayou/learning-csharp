namespace Memento_Pattern
{
    /// <summary>
    /// 备忘录类，保存编辑器的状态
    /// </summary>
    public class Memento
    {
        public string Text { get; private set; }
        public int CursorPosition { get; private set; }

        public Memento(string text, int cursorPosition)
        {
            Text = text;
            CursorPosition = cursorPosition;
        }
    }

    /// <summary>
    /// 编辑器类，具有撤销/恢复功能
    /// </summary>
    public class Editor
    {
        public string Text { get; private set; }
        public int CursorPosition { get; private set; }
        private List<Memento> _history = new List<Memento>();
        private int _currentIndex = -1;

        public void InsertText(string text)
        {
            Text += text;
            _history.Add(new Memento(Text, CursorPosition));
            _currentIndex++;
        }

        public void MoveCursor(int position)
        {
            CursorPosition = position;
            _history.Add(new Memento(Text, CursorPosition));
            _currentIndex++;
        }

        public void Undo()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                RestoreState(_history[_currentIndex]);
            }
        }

        public void Redo()
        {
            if (_currentIndex < _history.Count - 1)
            {
                _currentIndex++;
                RestoreState(_history[_currentIndex]);
            }
        }

        private void RestoreState(Memento memento)
        {
            Text = memento.Text;
            CursorPosition = memento.CursorPosition;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Editor editor = new Editor();

            editor.InsertText("Hello, ");
            editor.InsertText("World!");
            editor.MoveCursor(7);

            Console.WriteLine($"Before Undo: Text='{editor.Text}', CursorPosition={editor.CursorPosition}");

            editor.Undo();
            Console.WriteLine($"After Undo: Text='{editor.Text}', CursorPosition={editor.CursorPosition}");

            editor.Redo();
            Console.WriteLine($"After Redo: Text='{editor.Text}', CursorPosition={editor.CursorPosition}");
        }
    }
}
