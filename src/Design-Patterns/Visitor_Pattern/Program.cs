namespace Visitor_Pattern
{
    public interface IProduct
    {
        void Accept(IVisitor visitor);
        decimal Price { get; set; }
    }

    public class Book : IProduct
    {
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Book(string title, decimal price)
        {
            Title = title;
            Price = price;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Electronic : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Electronic(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public interface IVisitor
    {
        void Visit(Book book);
        void Visit(Electronic electronic);
    }

    public class DiscountCoupon : IVisitor
    {
        private decimal _discountRate = 0.1m; // 10% off

        public void Visit(Book book)
        {
            book.Price *= (1 - _discountRate);
            Console.WriteLine($"Applied discount to Book: {book.Title}, New Price: {book.Price} CNY");
        }

        public void Visit(Electronic electronic)
        {
            electronic.Price *= (1 - _discountRate);
            Console.WriteLine($"Applied discount to Electronic: {electronic.Name}, New Price: {electronic.Price} CNY");
        }
    }

    public class FullReductionCoupon : IVisitor
    {
        private decimal _fullAmount = 100m;
        private decimal _reduction = 20m;

        public void Visit(Book book)
        {
            if (book.Price >= _fullAmount)
            {
                book.Price -= _reduction;
                Console.WriteLine($"Applied full reduction to Book: {book.Title}, New Price: {book.Price} CNY");
            }
        }

        public void Visit(Electronic electronic)
        {
            if (electronic.Price >= _fullAmount)
            {
                electronic.Price -= _reduction;
                Console.WriteLine($"Applied full reduction to Electronic: {electronic.Name}, New Price: {electronic.Price} CNY");
            }
        }
    }

    public class CreditCard : IVisitor
    {
        public void Visit(Book book)
        {
            Console.WriteLine($"Paid Book: {book.Title} with Credit Card, Price: {book.Price} CNY");
        }

        public void Visit(Electronic electronic)
        {
            Console.WriteLine($"Paid Electronic: {electronic.Name} with Credit Card, Price: {electronic.Price} CNY");
        }
    }

    public class Alipay : IVisitor
    {
        public void Visit(Book book)
        {
            Console.WriteLine($"Paid Book: {book.Title} with Alipay, Price: {book.Price} CNY");
        }

        public void Visit(Electronic electronic)
        {
            Console.WriteLine($"Paid Electronic: {electronic.Name} with Alipay, Price: {electronic.Price} CNY");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("The Great Gatsby", 60m);
            var electronic = new Electronic("iPhone 13", 8000m);

            // 应用优惠券
            var discountCoupon = new DiscountCoupon();
            book.Accept(discountCoupon);
            electronic.Accept(discountCoupon);

            // 应用满减券
            var fullReductionCoupon = new FullReductionCoupon();
            book.Accept(fullReductionCoupon);
            electronic.Accept(fullReductionCoupon);

            // 使用支付方式
            var creditCard = new CreditCard();
            book.Accept(creditCard);
            electronic.Accept(creditCard);

            var alipay = new Alipay();
            book.Accept(alipay);
            electronic.Accept(alipay);
        }
    }
}
