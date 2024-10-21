namespace Decorator_Pattern
{
    /// <summary>
    /// 组件接口
    /// </summary>
    public interface ICoffee
    {
        string GetDescription();
        decimal CalculateCost();
    }

    /// <summary>
    /// 简单的咖啡
    /// </summary>
    public class SimpleCoffee : ICoffee
    {
        public string GetDescription() => "简单的咖啡";
        public decimal CalculateCost() => 1.00m;
    }

    /// <summary>
    /// 装饰者抽象类
    /// </summary>
    public abstract class CoffeeDecorator : ICoffee
    {
        protected readonly ICoffee _coffee;

        protected CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public abstract string GetDescription();
        public abstract decimal CalculateCost();
    }

    /// <summary>
    /// 牛奶装饰者
    /// </summary>
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription() => $"{_coffee.GetDescription()}, 牛奶";
        public override decimal CalculateCost() => _coffee.CalculateCost() + 0.50m;
    }

    /// <summary>
    /// 糖装饰者
    /// </summary>
    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription() => $"{_coffee.GetDescription()}, 糖";
        public override decimal CalculateCost() => _coffee.CalculateCost() + 0.20m;
    }

    /// <summary>
    /// 香草 syrup 装饰者
    /// </summary>
    public class VanillaSyrupDecorator : CoffeeDecorator
    {
        public VanillaSyrupDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription() => $"{_coffee.GetDescription()}, 香草 syrup";
        public override decimal CalculateCost() => _coffee.CalculateCost() + 0.80m;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建简单的咖啡
            ICoffee coffee = new SimpleCoffee();
            Console.WriteLine($"描述：{coffee.GetDescription()}, 价格：{coffee.CalculateCost()}");

            // 添加牛奶和糖
            coffee = new MilkDecorator(new SugarDecorator(coffee));
            Console.WriteLine($"描述：{coffee.GetDescription()}, 价格：{coffee.CalculateCost()}");

            // 再添加香草 syrup
            coffee = new VanillaSyrupDecorator(coffee);
            Console.WriteLine($"描述：{coffee.GetDescription()}, 价格：{coffee.CalculateCost()}");
        }
    }
}
