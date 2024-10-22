namespace Flyweight_Pattern
{
    public class CharacterFlyweight
    {
        public string Font { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }

        public CharacterFlyweight(string font, int size, string color)
        {
            Font = font;
            Size = size;
            Color = color;
        }

        // 共享的行为，例如绘制逻辑
        public void Draw(string character)
        {
            Console.WriteLine($"绘制字符 '{character}'，字体：{Font}，大小：{Size}，颜色：{Color}");
        }
    }

    public class Character
    {
        public char Value { get; set; }
        public CharacterFlyweight Flyweight { get; set; }

        public Character(char value, CharacterFlyweight flyweight)
        {
            Value = value;
            Flyweight = flyweight;
        }

        public void Draw()
        {
            Flyweight.Draw(Value.ToString());
        }
    }

    public class CharacterFlyweightFactory
    {
        private readonly Dictionary<string, CharacterFlyweight> _flyweights = new Dictionary<string, CharacterFlyweight>();

        public CharacterFlyweight GetFlyweight(string font, int size, string color)
        {
            string key = $"{font}-{size}-{color}";
            if (!_flyweights.TryGetValue(key, out CharacterFlyweight flyweight))
            {
                flyweight = new CharacterFlyweight(font, size, color);
                _flyweights.Add(key, flyweight);
            }
            return flyweight;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new CharacterFlyweightFactory();

            // 共享同一套字体设置的字符
            var sharedFlyweight = factory.GetFlyweight("Arial", 12, "Black");
            var charA = new Character('A', sharedFlyweight);
            var charB = new Character('B', sharedFlyweight); // 与charA共享Flyweight

            // 不同的字体设置
            var anotherFlyweight = factory.GetFlyweight("Times New Roman", 14, "Blue");
            var charC = new Character('C', anotherFlyweight);

            // 绘制字符
            charA.Draw();
            charB.Draw();
            charC.Draw();
        }
    }
}
