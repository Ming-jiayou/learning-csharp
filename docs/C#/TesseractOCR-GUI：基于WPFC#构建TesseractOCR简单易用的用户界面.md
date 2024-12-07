# TesseractOCR-GUI：基于WPF/C#构建TesseractOCR简单易用的用户界面

## 前言

前篇文章[使用Tesseract进行图片文字识别](https://mp.weixin.qq.com/s/C2o0-RtubtQb4pzys2wx6w)介绍了如何安装TesseractOCR与TesseractOCR的命令行使用。但在日常使用过程中，命令行使用还是不太方便的，因此今天介绍一下如何使用WPF/C#构建TesseractOCR简单易用的用户界面。

## 普通用户使用

参照上一篇教程，在本地安装好TesseractOCR之后，在GitHub的Release页面进行下载。

GitHub地址：https://github.com/Ming-jiayou/TesseractOCR-GUI

![image-20241207134914277](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207134914277.png)

推荐选择依赖框架的压缩包，体积比较小：

![image-20241207135004215](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135004215.png)

解压如下所示：

![image-20241207135159013](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135159013.png)

双击打开即可使用，如果显示你没有安装框架，点击链接，下载安装一下框架，即可打开使用。

识别中文：

![image-20241207135447692](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135447692.png)

识别英文：

![image-20241207135519142](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207135519142.png)

使用非常简单方便。

## WPF/C#程序员使用

经过简单的调研，发现构建TesseractOCR-GUI主要可以通过两种方式。一种就是对命令行的使用进行封装，另一种就是对TesseractOCR的C++ API进行封装。

对命令行的使用进行封装比较简单，而且目前暂时也满足了我的使用需求，因此目前只实现了这种方式，pytesseract好像也是使用的这种方式。第二种调用Tesseract C++ API的方式，可能得等第一种对命令行的使用进行封装无法满足需求的时候，才会去探索了。

项目结构：

![image-20241207140458038](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241207140458038.png)

开发工具：Visual Studio 2022

.NET版本：.NET 8

使用的包：Prism + WPF UI

核心代码：

```csharp
  private void ExecuteOCRCommand()
  {
      string command;
      switch(SelectedLanguage)
      {
          case "中文":
              command = $"tesseract {SelectedFilePath} stdout -l chi_sim quiet";
              break;
          case "英文":
              command = $"tesseract {SelectedFilePath} stdout -l eng quiet";
              break;
          default:
              command = $"tesseract {SelectedFilePath} stdout -l chi_sim quiet";
              break;
      }  

      // 创建一个新的 ProcessStartInfo 对象
      ProcessStartInfo processStartInfo = new ProcessStartInfo
      {
          FileName = "cmd.exe", // 使用 cmd.exe 作为命令解释器
          Arguments = $"/c {command}", // 传递命令作为参数，/c 表示执行命令后退出
          RedirectStandardOutput = true, // 重定向标准输出
          RedirectStandardError = true, // 重定向标准错误
          UseShellExecute = false, // 不使用 Shell 执行
          CreateNoWindow = true, // 不创建新窗口
          StandardOutputEncoding = Encoding.GetEncoding("UTF-8"), // 设置标准输出的编码
          StandardErrorEncoding = Encoding.GetEncoding("UTF-8") // 设置标准错误的编码
      };

      // 创建一个新的 Process 对象
      Process process = new Process
      {
          StartInfo = processStartInfo
      };

      // 启动进程
      process.Start();

      // 读取输出
      OCRText = process.StandardOutput.ReadToEnd();

      // 读取错误（如果有）
      string error = process.StandardError.ReadToEnd();

      // 等待进程退出
      process.WaitForExit();
  }
```

## 最后

本项目可以帮助人们更简单方便地使用TesseractOCR，对WPF/C#新手程序员，也可以当作一个简单的练手小项目。

如果对你有所帮助，点颗star，就是最大的支持！！

还有任何问题，欢迎通过微信公众号与我联系：

![qrcode_for_gh_eb0908859e11_344](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/qrcode_for_gh_eb0908859e11_344.jpg)