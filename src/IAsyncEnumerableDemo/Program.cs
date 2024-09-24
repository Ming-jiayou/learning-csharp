namespace IAsyncEnumerableDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var s = Console.ReadLine();
            int n = int.Parse(s);
            // 遍历异步流
            await foreach (var number in GenerateNumbersAsync(n))
            {
                Console.WriteLine($"返回第{number + 1}个值"); 
            }
        }

        // 定义一个异步迭代器方法，生成一个异步的整数流
        public static async IAsyncEnumerable<int> GenerateNumbersAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(1000); // 模拟异步操作，例如从网络或文件系统读取数据
                yield return i; // 每次迭代返回一个值
            }
        }
    }
}
