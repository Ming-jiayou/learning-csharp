using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCP_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建一个TCP监听器
            TcpListener server = new TcpListener(IPAddress.Any, 13000);
            server.Start();
            Console.WriteLine("Server started. Listening for connections on port 13000...");

            while (true)
            {
                // 等待客户端连接
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                // 创建一个线程来处理客户端连接
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        static void HandleClient(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream stream = tcpClient.GetStream();

            try
            {
                while (true)
                {
                    // 读取客户端发送的数据
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break; // 客户端断开连接
                    }

                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received from client: " + data);

                    // 向客户端发送响应
                    string response = "Server received: " + data;
                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // 关闭连接
                tcpClient.Close();
                Console.WriteLine("Client disconnected.");
            }
        }
    }
}