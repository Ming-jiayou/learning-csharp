using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDP_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建一个UDP客户端
            UdpClient server = new UdpClient(13000);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Server started. Listening for messages on port 13000...");

            while (true)
            {
                // 接收客户端发送的数据
                byte[] buffer = server.Receive(ref remoteEndPoint);
                string data = Encoding.ASCII.GetString(buffer);
                Console.WriteLine("Received from client: " + data);

                // 向客户端发送响应
                string response = "Server received: " + data;
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                server.Send(responseBytes, responseBytes.Length, remoteEndPoint);
            }
        }
    }
}
