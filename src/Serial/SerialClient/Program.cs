using System.IO.Ports;
namespace SerialClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 配置串口
            string portName = "COM2"; // 根据实际情况修改串口号
            int baudRate = 9600;

            using (SerialPort serialPort = new SerialPort(portName, baudRate))
            {
                // 打开串口
                serialPort.Open();
                Console.WriteLine("Serial client connected to " + portName + " at " + baudRate + " baud.");

                // 发送数据
                string message = "Hello, Server!";
                serialPort.WriteLine(message);
                Console.WriteLine("Sent: " + message);

                // 保持程序运行
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}

