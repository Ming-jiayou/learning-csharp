namespace Builder_Pattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Construct(builder);

            var product = builder.GetProduct();
            product.Show();
        }
    }

    public class Director
    {
        public void Construct(IBuilder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        Product GetProduct();
    }

    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new Product();

        public void BuildPartA()
        {
            _product.Add("PartA");
        }

        public void BuildPartB()
        {
            _product.Add("PartB");
        }

        public Product GetProduct()
        {
            return _product;
        }
    }

    public class Product
    {
        private List<string> _parts = new List<string>();

        public void Add(string part)
        {
            _parts.Add(part);
        }

        public void Show()
        {
            Console.WriteLine("Product Parts:");
            foreach (var part in _parts)
            {
                Console.WriteLine(part);
            }
        }
    }
}
