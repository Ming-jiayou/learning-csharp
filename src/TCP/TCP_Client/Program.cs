using System.Net.Sockets;
using System.Text;

namespace TCP_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建一个TCP客户端
            TcpClient client = new TcpClient();
            try
            {
                // 连接到服务器
                client.Connect("127.0.0.1", 13000);
                Console.WriteLine("Connected to server.");

                // 获取网络流
                NetworkStream stream = client.GetStream();

                // 发送数据到服务器
                string message = "Hello, Server!";
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: " + message);

                // 读取服务器的响应
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received from server: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // 关闭连接
                client.Close();
                Console.WriteLine("Disconnected from server.");
            }
        }
    }
}

