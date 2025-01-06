# C# AIModelRouter：使用不同的AI模型完成不同的任务

## AIModelRouter

AI模型路由，模型的能力有大小之分，有些简单任务，能力小一点的模型也能很好地完成，而有些比较难的或者希望模型做得更好的，则可以选择能力强的模型。为什么要这样做呢？可以降低AI模型的使用成本，毕竟能力强的模型会更贵一点，省着用挺好的。

Semantic Kernel中可以很简便地使用一个AIModelRouter。

## 实践

**先来一个简单的例子**

来自https://github.com/microsoft/semantic-kernel/tree/main/dotnet/samples/Demos/AIModelRouter

新建一个CustomRouter类，如下所示：

```csharp
internal sealed class CustomRouter()
{
    internal string GetService(string lookupPrompt, List<string> serviceIds)
    {
        // The order matters, if the keyword is not found, the first one is used.
        foreach (var serviceId in serviceIds)
        {
            if (Contains(lookupPrompt, serviceId))
            {
                return serviceId;
            }
        }

        return serviceIds[0];
    }

    // Ensure compatibility with both netstandard2.0 and net8.0 by using IndexOf instead of Contains
    private static bool Contains(string prompt, string pattern)
        => prompt.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0;
}
```

新建一个SelectedServiceFilter类用于打印一些信息：

```csharp
 internal sealed class SelectedServiceFilter : IPromptRenderFilter
 {
     /// <inheritdoc/>
     public Task OnPromptRenderAsync(PromptRenderContext context, Func<PromptRenderContext, Task> next)
     {
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine($"Selected service id: '{context.Arguments.ExecutionSettings?.FirstOrDefault().Key}'");

         Console.ForegroundColor = ConsoleColor.White;
         Console.Write("Assistant > ");
         return next(context);
     }
 }
```

使用多个模型：

![image-20250106101815911](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106101815911.png)

为捕获路由器选择的服务 ID 添加自定义过滤器：

![image-20250106101942229](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106101942229.png)

开启一个聊天循环：

```csharp
        Console.ForegroundColor = ConsoleColor.White;

        ChatHistory history = [];
        string history1 = string.Empty;
        bool isComplete = false;

        do
        {
            Console.WriteLine();
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }
            if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                isComplete = true;
                break;
            }
            if (input.Trim().Equals("Clear", StringComparison.OrdinalIgnoreCase))
            {
                history.Clear();
                history1 = " ";
                Console.WriteLine("已清除聊天记录");
                continue;
            }

            history.Add(new ChatMessageContent(AuthorRole.User, input));
            history1 += $"User:{input}\n";

            Console.WriteLine();

            // Find the best service to use based on the user's input
            KernelArguments arguments = new(new PromptExecutionSettings()
            {
                ServiceId = router.GetService(input, serviceIds).Result,
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            });

            // Invoke the prompt and print the response
            //await foreach (var chatChunk in kernel.InvokePromptStreamingAsync(userMessage, arguments).ConfigureAwait(false))
            //{
            //    Console.Write(chatChunk);
            //}
           
            var result = await kernel.InvokePromptAsync(history1, arguments).ConfigureAwait(false);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(result);
            Console.WriteLine();

            // Add the message from the agent to the chat history
            history.AddMessage(AuthorRole.Assistant, result.ToString());
            history1 += $"Assistant:{result}\n";
        } while (!isComplete);
    }
}
```

来看看现在这个简单的路由规则：

![image-20250106102824888](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106102824888.png)

当你的提问中包含一个ServiceId的时候，就会选择那个服务ID对应的模型进行回复，如果不包含就选择第一个服务ID对应的模型进行回复。

实际上这样使用，很容易让AI迷惑，因为我们总是要带上一个ServiceId，如果让AI根据用户的提问，自己决定用哪个模型是更好的。

**进阶使用，用AI自己来决定**

![image-20250106103343454](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106103343454.png)

使用一个靠谱的AI模型来做这个事情比较好。

我们输入你好，那么Prompt就会变成这样：

![image-20250106103624167](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106103624167.png)

AI返回的结果如下：

![image-20250106103713305](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106103713305.png)

![image-20250106103742224](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106103742224.png)

再试试其他几个怎么触发：

![image-20250106103848889](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106103848889.png)

而工具调用与其他比较容易混淆，因为就算是我们自己，也很难分辨有什么区别：

![image-20250106104310185](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106104310185.png)

这时候或许修改Prompt可以奏效。

修改后的Prompt如下：

```csharp
 string skPrompt = """
          根据用户的输入，返回最佳服务ID。
          如果用户需要获取当前时间与写邮件，则选择工具调用相关的服务ID。
          用户输入：
          {{$input}}
          服务ID列表：
          {{$serviceIds}}
          无需返回任何其他内容，只需返回服务ID。              
     """;
```

效果如下所示：

![image-20250106113558077](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250106113558077.png)

以上就是本次分享的全部内容，希望对你有所帮助。