namespace Adapter_Pattern
{
    internal class Program
    {
        static void Main(string[] args)
        {       
            ITarget target = new Adapter();
            target.Request();
        }
    }

    // 目标接口
    public interface ITarget
    {
        void Request();
    }

    // 适配器类
    public class Adapter : ITarget
    {
        private Adaptee _adaptee = new Adaptee();

        public void Request()
        {
            _adaptee.SpecificRequest();
        }
    }

    // 被适配的类
    public class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}
