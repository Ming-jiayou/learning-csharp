namespace Strategy_Pattern
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// 执行策略
        /// </summary>
        /// <param name="context">执行上下文</param>
        void Execute(Context context);
    }

    /// <summary>
    /// 具体策略：加法
    /// </summary>
    public class AdditionStrategy : IStrategy
    {
        public void Execute(Context context)
        {
            int result = context.Numbers[0] + context.Numbers[1];
            Console.WriteLine($"加法结果：{result}");
        }
    }

    /// <summary>
    /// 具体策略：减法
    /// </summary>
    public class SubtractionStrategy : IStrategy
    {
        public void Execute(Context context)
        {
            int result = context.Numbers[0] - context.Numbers[1];
            Console.WriteLine($"减法结果：{result}");
        }
    }

    /// <summary>
    /// 执行上下文
    /// </summary>
    public class Context
    {
        public int[] Numbers { get; set; }
        public IStrategy Strategy { get; set; }

        public Context(int[] numbers, IStrategy strategy)
        {
            Numbers = numbers;
            Strategy = strategy;
        }

        public void ExecuteStrategy()
        {
            Strategy.Execute(this);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // 执行加法
            int[] numbers = { 10, 5 };
            IStrategy additionStrategy = new AdditionStrategy();
            Context additionContext = new Context(numbers, additionStrategy);
            additionContext.ExecuteStrategy();

            // 执行减法
            IStrategy subtractionStrategy = new SubtractionStrategy();
            Context subtractionContext = new Context(numbers, subtractionStrategy);
            subtractionContext.ExecuteStrategy();

            // 或者直接通过构造函数切换策略
            Context contextWithSwitchedStrategy = new Context(numbers, subtractionStrategy);
            contextWithSwitchedStrategy.ExecuteStrategy();
        }
    }
}
