namespace Template_Method_Pattern
{
    public abstract class Beverage
    {
        // 模板方法 - 定义算法骨架
        public void Make()
        {
            BoilWater();
            Brew(); // 延迟到子类实现
            PourInCup();
            AddCondiments(); // 延迟到子类实现
        }

        // 共同步骤 - 烧水
        protected void BoilWater()
        {
            Console.WriteLine("烧水...");
        }

        // 共同步骤 - 倒入杯中
        protected void PourInCup()
        {
            Console.WriteLine("倒入杯中...");
        }

        // 子类实现 - 泛型方法（抽象方法）
        protected abstract void Brew();
        protected abstract void AddCondiments();
    }

    public class Coffee : Beverage
    {
        // 实现咖啡特有的步骤 - 煮咖啡
        protected override void Brew()
        {
            Console.WriteLine("用沸水冲泡咖啡...");
        }

        // 实现咖啡特有的步骤 - 加糖和奶精
        protected override void AddCondiments()
        {
            Console.WriteLine("加入糖和奶精...");
        }
    }

    public class Tea : Beverage
    {
        // 实现茶特有的步骤 - 泡茶
        protected override void Brew()
        {
            Console.WriteLine("用沸水泡茶...");
        }

        // 实现茶特有的步骤 - 加柠檬
        protected override void AddCondiments()
        {
            Console.WriteLine("加入柠檬片...");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 制作咖啡
            Console.WriteLine("制作咖啡:");
            Beverage coffee = new Coffee();
            coffee.Make();

            Console.WriteLine(); // 空行分隔

            // 制作茶
            Console.WriteLine("制作茶:");
            Beverage tea = new Tea();
            tea.Make();
        }
    }
}
