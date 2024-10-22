namespace Prototype_Pattern
{
    public interface IShapePrototype
    {
        IShapePrototype Clone();
        void Render();
    }

    public class Circle : IShapePrototype
    {
        public string Color { get; set; }
        public int Radius { get; set; }
        public (int, int) Position { get; set; }

        public Circle(string color, int radius, (int, int) position)
        {
            Color = color;
            Radius = radius;
            Position = position;
        }

        public IShapePrototype Clone()
        {
            // 创建深度副本
            return new Circle(Color, Radius, Position);
        }

        public void Render()
        {
            Console.WriteLine($"渲染圆形，位置：{Position}, 半径：{Radius}, 颜色：{Color}");
        }
    }

    public class Rectangle : IShapePrototype
    {
        public string Color { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public (int, int) Position { get; set; }

        public Rectangle(string color, int width, int height, (int, int) position)
        {
            Color = color;
            Width = width;
            Height = height;
            Position = position;
        }

        public IShapePrototype Clone()
        {
            // 创建深度副本
            return new Rectangle(Color, Width, Height, Position);
        }

        public void Render()
        {
            Console.WriteLine($"渲染矩形，位置：{Position}, 尺寸：{Width}x{Height}, 颜色：{Color}");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // 原始圆形
            var originalCircle = new Circle("红色", 10, (0, 0));
            Console.WriteLine("原始:");
            originalCircle.Render();

            // 克隆圆形
            var clonedCircle = (Circle)originalCircle.Clone();
            clonedCircle.Color = "蓝色"; // 修改克隆体
            clonedCircle.Position = (5, 5);
            Console.WriteLine("\n克隆并修改后:");
            clonedCircle.Render();

            // 原始矩形
            var originalRectangle = new Rectangle("绿色", 20, 30, (10, 10));
            Console.WriteLine("\n\n原始矩形:");
            originalRectangle.Render();

            // 克隆矩形
            var clonedRectangle = (Rectangle)originalRectangle.Clone();
            clonedRectangle.Width = 40; // 修改克隆体
            clonedRectangle.Position = (20, 20);
            Console.WriteLine("\n克隆并修改后的矩形:");
            clonedRectangle.Render();
        }
    }
}
