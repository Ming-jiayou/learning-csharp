namespace Abstract_Factory_Pattern
{
    internal class Program
    {
        public interface ILaptop
        {
            void ShowSpecs();
        }

        public interface IDesktop
        {
            void ShowSpecs();
        }

        // Apple 品牌的产品
        public class AppleLaptop : ILaptop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Apple Laptop Specifications");
            }
        }

        public class AppleDesktop : IDesktop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Apple Desktop Specifications");
            }
        }

        // Lenovo 品牌的产品
        public class LenovoLaptop : ILaptop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Lenovo Laptop Specifications");
            }
        }

        public class LenovoDesktop : IDesktop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Lenovo Desktop Specifications");
            }
        }

        public interface IComputerFactory
        {
            ILaptop CreateLaptop();
            IDesktop CreateDesktop();
        }

        public class AppleFactory : IComputerFactory
        {
            public ILaptop CreateLaptop()
            {
                return new AppleLaptop();
            }

            public IDesktop CreateDesktop()
            {
                return new AppleDesktop();
            }
        }

        public class LenovoFactory : IComputerFactory
        {
            public ILaptop CreateLaptop()
            {
                return new LenovoLaptop();
            }

            public IDesktop CreateDesktop()
            {
                return new LenovoDesktop();
            }
        }

        static void Main(string[] args)
        {

            // 创建工厂
            IComputerFactory appleFactory = new AppleFactory();
            IComputerFactory lenovoFactory = new LenovoFactory();

            // 创建 Apple 产品的笔记本和台式机
            ILaptop appleLaptop = appleFactory.CreateLaptop();
            IDesktop appleDesktop = appleFactory.CreateDesktop();
            appleLaptop.ShowSpecs();
            appleDesktop.ShowSpecs();

            // 创建 Lenovo 产品的笔记本和台式机
            ILaptop lenovoLaptop = lenovoFactory.CreateLaptop();
            IDesktop lenovoDesktop = lenovoFactory.CreateDesktop();
            lenovoLaptop.ShowSpecs();
            lenovoDesktop.ShowSpecs();
        }
    }
}
