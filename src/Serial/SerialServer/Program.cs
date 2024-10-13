using System;
using System.IO.Ports;
namespace SerialServer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // 配置串口
            string portName = "COM1"; // 根据实际情况修改串口号
            int baudRate = 9600;

            using (SerialPort serialPort = new SerialPort(portName, baudRate))
            {
                // 设置串口数据接收事件处理程序
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                // 打开串口
                serialPort.Open();
                Console.WriteLine("Serial server started. Listening for data on " + portName + " at " + baudRate + " baud.");

                // 保持程序运行
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
            Console.WriteLine("Received data: " + data);
        }
    }
            
}
