namespace Proxy_Pattern
{
    // 目标类：提供网络请求服务
    public class NetworkService
    {
        public string GetData(string url)
        {
            // 模拟网络请求（实际上，这里应该是网络操作）
            System.Threading.Thread.Sleep(2000); // 模拟延迟
            return $"数据来自 {url}";
        }
    }

    // 代理接口：保持与目标类相同的接口
    public interface INetworkService
    {
        string GetData(string url);
    }

    // 代理类：实现缓存机制
    public class CachedNetworkProxy : INetworkService
    {
        private readonly NetworkService _networkService;
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        public CachedNetworkProxy()
        {
            _networkService = new NetworkService();
        }

        public string GetData(string url)
        {
            if (_cache.TryGetValue(url, out string cachedData))
            {
                Console.WriteLine("来自缓存...");
                return cachedData;
            }
            else
            {
                Console.WriteLine("来自网络...");
                string data = _networkService.GetData(url);
                _cache[url] = data; // 缓存数据
                return data;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            INetworkService service = new CachedNetworkProxy();

            // 首次请求，来自网络
            Console.WriteLine(service.GetData("https://example.com/data1"));

            // 再次请求相同URL，来自缓存
            Console.WriteLine(service.GetData("https://example.com/data1"));

            // 请求不同URL，来自网络
            Console.WriteLine(service.GetData("https://example.com/data2"));
        }
    }
}
