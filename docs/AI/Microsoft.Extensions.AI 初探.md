# Microsoft.Extensions.AI 初探

## .NET Conf上的介绍

在今年的.NET Conf上Steve Sanderson带来了题为“AI Building Blocks - A new, unified AI layer”的演讲。该演讲的主要内容如下：

“大多数.NET应用程序可以通过AI功能变得更加强大和高效，例如语义搜索、自动分类、摘要生成、翻译、数据提取，甚至是基于聊天的助手。但直到现在，.NET本身还没有统一的AI概念表示标准，因此开发者需要组合使用许多不相关的API。Microsoft.Extensions.AI解决了这个问题，提供了一组新的AI服务标准API，包括在本地工作站上运行或作为托管服务的大型语言模型（LLMs），并集成了文本嵌入、向量存储等功能。在本次演讲中，我们将展示这些新的标准抽象如何让你组合多个服务，并且这些服务可以随着时间的推移轻松替换和更改，以及如何在更高级的场景中接入内部机制。通过本次演讲，你将能够开始在自己的应用程序中实验新的AI功能。”

youtube地址：https://www.youtube.com/watch?v=qcp6ufe_XYo&list=PLdo4fOcmZ0oXeSG8BgCVru3zQtw_K4ANY&index=3

Steve Sanderson介绍了以下几种应用场景：

![image-20241120094958717](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241120094958717.png)

## Microsoft.Extensions.AI介绍

2024年10月8日，Luis Quintanilla在.NET Blog上发布了题为“Introducing Microsoft.Extensions.AI Preview – Unified AI Building Blocks for .NET”的文章介绍了Microsoft.Extensions.AI Preview。

文章地址：https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/

“Microsoft.Extensions.AI 是一组由 .NET 生态系统中的开发者（包括 Semantic Kernel 团队）共同开发的核心 .NET 库。这些库提供了一层统一的 C# 抽象层，用于与 AI 服务进行交互，例如小型和大型语言模型（SLM 和 LLM）、嵌入内容以及中间件。”

![img](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/meai-architecture-diagram.png)

“目前，我们的重点是创建抽象概念，这些抽象概念可以由各种服务实现，并且都遵循相同的核心理念。我们不打算发布针对任何特定服务提供商的API。我们的目标是在.NET生态系统中充当一个统一的层，使开发者能够选择他们喜欢的框架和库，同时确保在整个生态系统中的无缝集成和协作。”

## Microsoft.Extensions.AI的优势

Microsoft.Extensions.AI 提供了一个统一的 API 抽象，用于 AI 服务，类似于我们在日志记录和依赖注入（DI）抽象方面的成功。我们的目标是提供标准的实现，用于缓存、遥测、工具调用和其他常见任务，这些实现可以与任何提供商兼容。

核心优势有以下几点：

统一API：为将AI服务集成到.NET应用程序提供了一致的API和约定。
灵活性：允许.NET库作者使用AI服务而无需绑定特定提供商，使其适应任何提供商。
易用性：使.NET开发人员能够使用相同的底层抽象尝试不同的包，在整个应用程序中保持单一API。
组件化：简化了添加新功能的过程，并促进了应用程序的组件化和测试。

## Microsoft.Extensions.AI简单实践

使用Microsoft.Extensions.AI可以看Nuget包的介绍。

地址：https://www.nuget.org/packages/Microsoft.Extensions.AI.Abstractions/9.0.0-preview.9.24556.5

先简单的以OpenAI为例，然后考虑到在国内使用OpenAI不便，再介绍一下如何接入兼容OpenAI格式的大语言模型提供商。

简单的对话：

```csharp
string OPENAI_API_KEY = "sk-sssss...";

IChatClient client =
 new OpenAIClient(OPENAI_API_KEY)
.AsChatClient(modelId: "gpt-4o-mini");

var response = await client.CompleteAsync("你是谁？");

Console.WriteLine(response.Message);
```

效果：

![image-20241120101114704](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241120101114704.png)

我比较关心的是Function Calling的功能，来简单尝试一下：

```csharp
string OPENAI_API_KEY = "sk-sssss...";

[Description("Get the current time")]
 string GetCurrentTime() => DateTime.Now.ToString();

 IChatClient client = new ChatClientBuilder()
     .UseFunctionInvocation()
     .Use(new OpenAIClient(OPENAI_API_KEY).AsChatClient(modelId: "gpt-4o-mini"));

 var response = client.CompleteStreamingAsync(
     "现在几点了？",
     new() { Tools = [AIFunctionFactory.Create(GetCurrentTime)] });

 await foreach (var update in response)
 {
     Console.Write(update);
 }
```

效果：

![image-20241120101404123](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241120101404123.png)

成功获取到了当前的时间。

由于在国内使用OpenAI不方便，而且国内也有很多大模型提供商都是兼容OpenAI格式的，因此现在以国内的模型提供商为例，进行说明。

我以硅基流动为例，上面还有一些额度。

简单对话：

```csharp
 OpenAIClientOptions openAIClientOptions = new OpenAIClientOptions();
 openAIClientOptions.Endpoint = new Uri("https://api.siliconflow.cn/v1");

 // SiliconCloud API Key
 string mySiliconCloudAPIKey = "sk-lll...";


 OpenAIClient client = new OpenAIClient(new ApiKeyCredential(mySiliconCloudAPIKey),  openAIClientOptions);
 IChatClient chatClient = client.AsChatClient("Qwen/Qwen2.5-72B-Instruct-128K");
 var response = await chatClient.CompleteAsync("你是谁？");
 Console.WriteLine(response.Message);
```

效果：

![image-20241120101803488](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241120101803488.png)

函数调用：

```csharp
 OpenAIClientOptions openAIClientOptions = new OpenAIClientOptions();
 openAIClientOptions.Endpoint = new Uri("https://api.siliconflow.cn/v1");

 // SiliconCloud API Key
 string mySiliconCloudAPIKey = "sk-lll...";

  [Description("Get the current time")]
  string GetCurrentTime() => DateTime.Now.ToString();

  IChatClient client = new ChatClientBuilder()
      .UseFunctionInvocation()
      .Use(new OpenAIClient(new ApiKeyCredential(mySiliconCloudAPIKey), openAIClientOptions).AsChatClient("Qwen/Qwen2.5-72B-Instruct-128K"));

  var response = await client.CompleteAsync(
      "现在几点了？",
      new() { Tools = [AIFunctionFactory.Create(GetCurrentTime)] });

  Console.Write(response);
```

![image-20241120102258535](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241120102258535.png)

也成功进行函数调用，获取到了当前的时间。

会发现其实和SemanticKernel很像，Steve Sanderson也坦言这些是从SemanticKernel“毕业”的东西，更多用例可由读者自行探索。