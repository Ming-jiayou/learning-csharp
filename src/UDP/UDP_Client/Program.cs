using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建一个UDP客户端
            UdpClient client = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13000);

            try
            {
                // 发送数据到服务器
                string message = "Hello, Server!";
                byte[] data = Encoding.ASCII.GetBytes(message);
                client.Send(data, data.Length, serverEndPoint);
                Console.WriteLine("Sent: " + message);

                // 读取服务器的响应
                byte[] buffer = client.Receive(ref serverEndPoint);
                string response = Encoding.ASCII.GetString(buffer);
                Console.WriteLine("Received from server: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // 关闭客户端
                client.Close();
                Console.WriteLine("Disconnected from server.");
            }
        }
    }
}
