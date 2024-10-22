namespace Singleton_Pattern
{
    public class Singleton
    {
        private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => _instance.Value;

        // 私有构造函数，防止外部实例化
        private Singleton() { }

        // 测试方法
        public void Test()
        {
            Console.WriteLine("这是单例模式的测试方法");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // 获取单例实例
            var singleton1 = Singleton.Instance;
            var singleton2 = Singleton.Instance;

            // 是否是同一个实例
            Console.WriteLine(singleton1 == singleton2); // True

            // 调用测试方法
            singleton1.Test();
        }
    }
}
