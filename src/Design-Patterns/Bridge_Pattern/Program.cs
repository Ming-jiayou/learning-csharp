namespace Bridge_Pattern
{
    internal class Program
    {     
      
         // 实现接口 - 图像格式
        public interface IImageFormat
        {
            void Display(string fileName);
        }

        // 具体实现 - PNG格式
        public class PNGFormat : IImageFormat
        {
            public void Display(string fileName)
            {
                Console.WriteLine($"Displaying {fileName} in PNG format.");
            }
        }

        // 具体实现 - JPG格式
        public class JPGFormat : IImageFormat
        {
            public void Display(string fileName)
            {
                Console.WriteLine($"Displaying {fileName} in JPG format.");
            }
        }

        // 抽象 - 操作系统
        public abstract class OperatingSystem
        {
            protected IImageFormat imageFormat; // 桥接接口

            public OperatingSystem(IImageFormat imageFormat)
            {
                this.imageFormat = imageFormat;
                Console.WriteLine("Initializing OperatingSystem...");
            }

            public abstract void OpenImage(string fileName);
        }

        // 扩展抽象 - Windows操作系统
        public class WindowsOS : OperatingSystem
        {
            public WindowsOS(IImageFormat imageFormat) : base(imageFormat)
            { 
                Console.WriteLine("Initializing WindowsOS...");
            }

            public override void OpenImage(string fileName)
            {
                Console.WriteLine("Windows: Opening image...");
                imageFormat.Display(fileName);
            }
        }

        // 扩展抽象 - macOS操作系统
        public class MacOS : OperatingSystem
        {
            public MacOS(IImageFormat imageFormat) : base(imageFormat) { }

            public override void OpenImage(string fileName)
            {
                Console.WriteLine("macOS: Opening image...");
                imageFormat.Display(fileName);
            }
        }
       
        // 在Main方法中使用桥接模式
        static void Main(string[] args)
        {
                // 创建格式实例
                IImageFormat pngFormat = new PNGFormat();
                IImageFormat jpgFormat = new JPGFormat();

                // 创建操作系统实例，并与格式实例桥接
                OperatingSystem windowsWithPNG = new WindowsOS(pngFormat);
                OperatingSystem macWithJPG = new MacOS(jpgFormat);
                OperatingSystem windowsWithJPG = new WindowsOS(jpgFormat); // 易扩展性示例

                // 演示
                windowsWithPNG.OpenImage("image1.png");
                macWithJPG.OpenImage("image2.jpg");
                windowsWithJPG.OpenImage("image3.jpg");
        }
    }
}
