# 开发者福音！！智谱GLM-4V-Flash免费调用！！几行代码接入你的C#应用

## 前言

智谱GLM-4V-Flash现在可以免费调用了，快来试试吧！！

![image-20241215104805573](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241215104805573.png)

VLM的使用一般都比较耗费Token，智谱直接免费调用了，真是开发者福音，最关键的是免费调用的模型的能力也很强！！

## 接入自己的C#应用

想要接入的时候，一般要看一下智谱的开发文档，但是我觉得应该会兼容OpenAI的格式，尝试了一下确实可以。因此C#开发者可以直接使用SemanticKernel接入。

安装SemanticKernel与OpenAI连接器：

![image-20241215105336410](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241215105336410.png)

```csharp
 switch (platform)
 {
     case "SiliconCloud":
         IKernelBuilder kernelBuilder1 = Kernel.CreateBuilder();
         kernelBuilder1.AddOpenAIChatCompletion(
             modelId: SelectedVLM,
             apiKey: APIKey,
             endpoint: new Uri("https://api.siliconflow.cn/v1")
         );
         kernel = kernelBuilder1.Build();
         break;
     case "ZhiPu":
         IKernelBuilder kernelBuilder2 = Kernel.CreateBuilder();
         kernelBuilder2.AddOpenAIChatCompletion(
             modelId: SelectedVLM,
             apiKey: APIKey,
             endpoint: new Uri("https://open.bigmodel.cn/api/paas/v4")
         );
         kernel = kernelBuilder2.Build();
         break;
     default:
         throw new InvalidOperationException("Unsupported platform");
 }
        
 var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
```

注意只要这里的endpoint修改为"https://open.bigmodel.cn/api/paas/v4"，modelId填入glm-4v-flash，apiKey填入智谱的API Key即可。

简单使用这样写即可：

```csharp
 if (SelectedFilePath == null)
 {
     return;
 }

 byte[] bytes = File.ReadAllBytes(SelectedFilePath);

 // Create a chat history with a system message instructing
 // the LLM on its required role.
 var chatHistory = new ChatHistory("你是一个描述图片的助手，全程使用中文回答");

 // Add a user message with both the image and a question
 // about the image.
 chatHistory.AddUserMessage(
   [
     new TextContent(AskAIText),
     new ImageContent(bytes, "image/jpeg"),
   ]);

 // Invoke the chat completion model.        
 var response = chatCompletionService.GetStreamingChatMessageContentsAsync(
                             chatHistory: chatHistory,
                             kernel: kernel
                         );
 await foreach (var chunk in response)
 {
     AskAIResponse += chunk;
 }
```

查看一下效果：

![image-20241215110017919](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241215110017919.png)

![image-20241215105953162](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241215105953162.png)

如果不想自己从头写这些，也想体验体验的话，可以直接使用VLM-OCR-Demo，GitHub地址：https://github.com/Ming-jiayou/VLM-OCR-Demo

新建一个.env文件，这样写即可体验：

![image-20241215110552721](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241215110552721.png)

