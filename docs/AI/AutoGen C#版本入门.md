# AutoGen C#版本入门

## AutoGen介绍

AutoGen 是一个开源编程框架，用于构建 AI 代理并促进多个代理之间的合作以解决问题。AutoGen 旨在提供一个易于使用和灵活的框架，以加速代理型 AI 的开发和研究，就像 PyTorch 之于深度学习。它提供了诸如代理之间可以对话、LLM 和工具使用支持、自主和人机协作工作流以及多代理对话模式等功能。

![image-20250108201942757](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250108201942757.png)

**主要特点**

AutoGen使得基于多智能体对话构建下一代LLM应用程序变得非常容易。它简化了复杂LLM工作流的编排、自动化和优化。它最大化了LLM模型的性能并克服了它们的弱点。

它支持复杂工作流的各种对话模式。通过使用可定制和可对话的代理，开发人员可以使用AutoGen构建各种涉及对话自主性、代理数量和代理对话拓扑的对话模式。

它提供了一系列不同复杂度的运行系统。这些系统涵盖了来自不同领域和复杂度的各种应用程序。这展示了AutoGen如何轻松支持各种对话模式。

上一篇文章介绍了AutoGen的入门例子，是使用python写的：

[AutoGen入门-让两个AI自行聊天完成任务](https://mp.weixin.qq.com/s/yva2JXzXRosYY-Xfs7cFAg)

实际上AutoGen也提供了.NET版本，只是文档不全，官方文档上的只有Python版本的介绍。

AutoGen项目地址：https://github.com/microsoft/autogen

![image-20250108202001569](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250108202001569.png)

## AutoGen C#版本入门

git clone项目到本地，打开dotnet文件夹中的AutoGen.sln。在AutoGen.BasicSample提供了一些基本的例子，把这些例子看完，基本上就实现入门了。

![image-20250108202240114](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250108202240114.png)

那么自己项目中如何使用C#版本的AutoGen呢？

接下来我将以一个具体的例子说明如何在自己的项目中开始使用AutoGen。

AutoGen目前还没有提供nuget包，使用它可以通过项目引用的方式。

新建一个C#控制台项目，添加现有项目，然后添加项目引用，如下所示：

![image-20250108202602967](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250108202602967.png)

还是与Python版本的入门例子一样，创建一个AI团队。

这里我想使用国内的模型，怎么办呢？

仿照例子的做法，创建一个LLMConfiguration类，如下所示：

```csharp
internal static class LLMConfiguration
{
    public static ChatClient GetOpenAIGPT4o_mini()
    {
        var openAIKey = "sk-xxx";
        var modelId = "gpt-4o-mini";

        return new OpenAIClient(openAIKey).GetChatClient(modelId);
    }

    public static ChatClient GetSiliconCloudModel(string modelId)
    {
        var openAIKey = "sk-xxx";
        ApiKeyCredential apiKeyCredential = new ApiKeyCredential(openAIKey);
        OpenAIClientOptions openAIClientOptions = new OpenAIClientOptions();
        openAIClientOptions.Endpoint = new Uri("https://api.siliconflow.cn");

        return new OpenAIClient(apiKeyCredential, openAIClientOptions).GetChatClient(modelId);
    }
}
```

因为不用上传到GitHub因此可以直接硬编码，如果你的代码别人会看到，可以使用环境变量或者.env文件的形式存储密钥。

创建两个不同的模型：

```csharp
 var model1 = LLMConfiguration.GetSiliconCloudModel("deepseek-ai/DeepSeek-V2.5");
 var model2 = LLMConfiguration.GetSiliconCloudModel("Qwen/Qwen2.5-72B-Instruct");
```

创建一个主要代理：

```csharp
 var primaryAgent = new OpenAIChatAgent(
    chatClient: model1,
    name: "primaryAgent",
    systemMessage: "You are a helpful AI assistant.")
    .RegisterMessageConnector()             
    .RegisterPrintMessage();
```

创建一个反馈代理：

```csharp
var criticAgent = new OpenAIChatAgent(
  chatClient: model1,
  name: "criticAgent",
  systemMessage: "提供修改意见。当你的意见被很好的响应的时候，回复APPROVE。")
  .RegisterMessageConnector()
  .RegisterMiddleware(async (msgs, option, agent, _) =>
   {
       var reply = await agent.GenerateReplyAsync(msgs, option);
       if (reply.GetContent()?.ToLower().Contains("approve") is true)
       {
           return new TextMessage(Role.Assistant, GroupChatExtension.TERMINATE, from: reply.From);
       }

       return reply;
   })
  .RegisterPrintMessage();
```

开始让这两个代理对话：

```csharp
 var conversation = await criticAgent.InitiateChatAsync(
    receiver: primaryAgent,
    message: "写一首关于春天的诗",
    maxRound: 10);
```

实现效果：

![image-20250108203530315](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250108203530315.png)

```csharp
from: primaryAgent
《春韵》

东风轻抚柳丝长，
燕语莺歌绕画梁。
雨润桃花红欲滴，
风吹杏蕊白如霜。
溪边嫩草添新绿，
陌上芳菲散晚香。
最是春光无限好，
人间四月胜天堂。

这首作品描绘了春天的生机盎然和美丽景色，通过东风、燕语、莺歌、柳丝、桃花、杏蕊等意象，展现了春天的多彩与活力。诗中，“雨 润桃花红欲滴”与“风吹杏蕊白如霜”形成色彩对比，增强了视觉美感。而“溪边嫩草添新绿”和“陌上芳菲散晚香”则进一步以春天的自然变 化来传达出人们对春天的喜爱之情。结尾处，“最是春光无限好，人间四月胜天堂”更是将春天的美好推向高潮，表达了对春天景色的无限赞美。

TextMessage from criticAgent
--------------------
这首诗确实很美，然我提供几个修改建议以提升其韵味。

1. 在第一句“东风轻抚柳丝长”中，增加一些动感元素可以让画面更加生动。例如，“东风拂动柳丝长”。

2. 第二句“燕语莺歌绕画梁”中的“绕”可以改为“掠”，“燕语莺歌掠画梁”，这样可以更好地描绘出燕子和黄莺轻盈飞过的场景。

3. 第四句“风吹杏蕊白如霜”中的“白如霜”略显平淡，可以考虑用“皎”字，“风吹杏蕊皎如雪”，以增强白色杏花的明亮和纯净感。

4. 最后一句“人间四月胜天堂”可以改为“人间四月美如仙”，这样不仅保持了赞美的意味，而且使诗句更加流畅易记。

这些改动旨在增强诗句的动感和色彩，进一步提升诗的整体意境。希望这些建议对你有所帮助。
--------------------

from: primaryAgent
感谢您的细致建议，这些修改确实能为诗篇增添动感与色彩，使春天的景象更加生动和鲜明。以下是根据您的建议修改后的版本：

《春韵》

东风拂动柳丝长，
燕语莺歌掠画梁。
雨润桃花红欲滴，
风吹杏蕊皎如雪。
溪边嫩草添新绿，
陌上芳菲散晚香。
最是春光无限好，
人间四月美如仙。

通过这些细微的调整，诗中的春风不再是轻轻的抚摸，而是带着一丝动感的拂动；燕语莺歌也不再仅仅是围绕，而是轻盈地掠过；杏花的白不再仅仅是如霜，而是洁白似雪的美；最后，人间四月的美丽也不再仅仅胜过天堂，而是如仙境般美轮美奂。这些改动不仅增强了诗句的表现力，也更加贴合春天的生动和多彩。希望这一版本能让春天的美丽更加动人心魄。

TextMessage from criticAgent
--------------------
[GROUPCHAT_TERMINATE]
--------------------
```

