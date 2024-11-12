# C# IAsyncEnumerable<T>

`IAsyncEnumerable<T>` 是 C# 8.0 引入的一个接口，用于表示可以异步遍历的集合。它允许你在不阻塞主线程的情况下，逐步获取集合中的元素。这在处理大量数据或长时间运行的操作时特别有用，因为它可以提高应用程序的响应性和资源利用率。

### 基本概念

1. **异步迭代**：
   - `IAsyncEnumerable<T>` 允许你定义一个异步迭代器方法，该方法可以异步生成一系列值。
   - 使用 `await foreach` 语法可以异步遍历这些值。
2. **非阻塞性**：
   - 与传统的 `IEnumerable<T>` 不同，`IAsyncEnumerable<T>` 不会一次性加载所有数据到内存中，而是按需异步获取数据。
   - 这使得处理大量数据或长时间运行的操作更加高效。

## 使用示例

```csharp
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
```

效果：

![image-20240924104125692](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240924104125692.png)

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/IAsyncEnumerableDemo%E6%95%88%E6%9E%9C.gif)

## 在项目中实际使用

第一次碰到IAsyncEnumerable<T>是在写SimpleRAG的时候，在`InvokePromptStreamingAsync`方法碰到的。

![image-20240924104557006](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240924104557006.png)

之前没有使用流式，是获取整个结果之后，为了实现假流式的效果，每次只显示几个字符：

![image-20240924104816564](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240924104816564.png)

现在改为使用流式：

![image-20240924104841534](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240924104841534.png)

现在对于回复内容比较多的效果就会好很多，如下所示：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/SimpleRAG%E6%94%B9%E4%B8%BA%E6%B5%81%E5%BC%8F.gif)

## 使用场景

`IAsyncEnumerable<T>` 在处理异步数据流时非常有用，特别是在以下场景中：

- **网络请求**：从多个网络端点异步获取数据。
- **文件读取**：从文件系统异步读取大量数据。
- **数据处理**：处理需要长时间计算的数据流。
- **数据库查询**：处理大量数据库查询结果。
- **实时数据流**：处理实时数据流，如传感器数据或日志文件。

通过使用 `IAsyncEnumerable<T>`，可以提高应用程序的响应性和资源利用率，确保在处理大量数据时不会阻塞主线程。