namespace Iterator_Pattern
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"{Title} by {Author}";
        }
    }

    public interface IBookCollection
    {
        IIterator CreateIterator();
        void Add(Book book);
        int Count { get; }
    }

    public class BookCollection : IBookCollection
    {
        private List<Book> _books = new List<Book>();

        public IIterator CreateIterator()
        {
            return new BookIterator(this);
        }

        public void Add(Book book)
        {
            _books.Add(book);
        }

        public int Count => _books.Count;

        // 内部迭代器类
        private class BookIterator : IIterator
        {
            private readonly BookCollection _collection;
            private int _currentIndex = -1;

            public BookIterator(BookCollection collection)
            {
                _collection = collection;
            }

            public bool HasNext()
            {
                return _currentIndex < _collection.Count - 1;
            }

            public object Next()
            {
                if (HasNext())
                {
                    _currentIndex++;
                    return _collection._books[_currentIndex];
                }
                else
                {
                    throw new InvalidOperationException("No more elements.");
                }
            }
        }
    }

    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IBookCollection library = new BookCollection();

            library.Add(new Book("To Kill a Mockingbird", "Harper Lee"));
            library.Add(new Book("1984", "George Orwell"));
            library.Add(new Book("Pride and Prejudice", "Jane Austen"));

            IIterator iterator = library.CreateIterator();

            while (iterator.HasNext())
            {
                Book book = (Book)iterator.Next();
                Console.WriteLine(book.ToString());
            }
        }
    }
}
