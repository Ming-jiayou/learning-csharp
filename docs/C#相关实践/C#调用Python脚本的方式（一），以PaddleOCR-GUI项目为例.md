# C#调用Python脚本的方式（一），以PaddleOCR-GUI为例

## 前言

每种语言都有每种语言的优势，Python由于其强大的生态，很多任务通过调用包就可以实现，那么学会从C#项目中调用Python脚本完成任务就很重要。C#调用Python代码有多种方式，如果Python那边内容比较多，可以考虑起一个Web Api进行调用，如果只是一个简单的脚本而且不需要频繁调用，那么可以考虑使用Process类创建一个进程来调用，如果有几个方法，并且需要进行数据交互，并可能会频繁调用，那么可以考虑使用pythonnet。

今天依托PaddleOCR-GUI项目，先给大家介绍的是C#调用Python脚本的方式一：使用Process类调用Python脚本。

## 背景介绍

PaddleOCR是基于PaddlePaddle框架开发的开源文字识别工具，由百度团队维护。它提供了从预处理、文字检测、文字识别到后处理的全流程文字识别解决方案。PaddleOCR不仅性能优异，而且配置灵活、使用便捷，能够满足多种场景下的文字识别需求，广泛应用于广告检测、图像搜索、自动驾驶、内容安全审核等多个领域。

![image-20241213190225955](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241213190225955.png)

GitHub地址：https://github.com/PaddlePaddle/PaddleOCR

之前也介绍过C#中可以直接使用PaddleSharp进行调用：

[C#使用PaddleOCR进行图片文字识别](https://mp.weixin.qq.com/s/ULf3ZY6x8KgaOFkd2oBYKA)

但是不能指望所有Python的东西都有大佬给你封装好，让你直接调就行。需要离开自己的舒适区，去了解更多其他语言其他生态的内容。

PaddleOCR-GUI只是给PaddleOCR提供了一个简单的使用界面，使用效果如下所示：

![image-20241213191129075](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241213191129075.png)

![image-20241213190922590](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241213190922590.png)

GitHub地址：https://github.com/Ming-jiayou/PaddleOCR-GUI

需要先在电脑上搭建PaddleOCR的环境：

Python版本 3.12.8

创建一个Python虚拟环境，在虚拟环境中安装好PaddleOCR，可以参考官网的快速开始：

[快速开始 - PaddleOCR 文档](https://paddlepaddle.github.io/PaddleOCR/main/quick_start.html)

## C#调用Python脚本

今天演示的是通过Process类调用Python脚本，与实际项目相结合，需要思考的是如何进行参数的传递呢？比如这里选择的图片路径以及选择的语言。

可以通过命令行参数的方式使用，Python脚本写好如下所示：

```python
import sys
import logging
from paddleocr import PaddleOCR

# Paddleocr目前支持的多语言语种可以通过修改lang参数进行切换
# 例如`ch`, `en`, `fr`, `german`, `korean`, `japan`

# 检查是否有参数传递
if len(sys.argv) > 1:
	imagePath = sys.argv[1]
	selectedLanguage = sys.argv[2]
else:
	print("请提供完整参数")

# 配置日志级别为 WARNING，这样 DEBUG 和 INFO 级别的日志信息将被隐藏
logging.basicConfig(level=logging.WARNING)

# 创建一个自定义的日志处理器，将日志输出到 NullHandler（不输出）
class NullHandler(logging.Handler):
	def emit(self, record):
		pass

# 获取 PaddleOCR 的日志记录器
ppocr_logger = logging.getLogger('ppocr')

# 移除所有默认的日志处理器
for handler in ppocr_logger.handlers[:]:
	ppocr_logger.removeHandler(handler)

# 添加自定义的 NullHandler
ppocr_logger.addHandler(NullHandler())

ocr = PaddleOCR(use_angle_cls=True, lang=selectedLanguage)  # need to run only once to download and load model into memory
img_path = imagePath
result = ocr.ocr(img_path, cls=True)
for idx in range(len(result)):
	res = result[idx]   
	for line in res:
		print(line[1][0])
```

需要传递的参数在此处通过命令行的方式传递：

```python
# 检查是否有参数传递
if len(sys.argv) > 1:
	imagePath = sys.argv[1]
	selectedLanguage = sys.argv[2]
else:
	print("请提供完整参数")
```

然后在C#中只需这样使用即可：

```csharp
  private Task ExecuteOCRCommand()
  {
      return Task.Run(() =>
      {
          string selectedLanguage;
          switch (SelectedLanguage)
          {
              case "中文":
                  selectedLanguage = "ch";
                  break;
              case "英文":
                  selectedLanguage = "en";
                  break;
              default:
                  selectedLanguage = "ch";
                  break;
          }
          if (PaddleOCRSettingsViewModel.PythonScriptPath == null || PaddleOCRSettingsViewModel.PythonExecutablePath == null)
          {
              return;
          }
          string pythonScriptPath = PaddleOCRSettingsViewModel.PythonScriptPath; // 替换为你的Python脚本路径
          string pythonExecutablePath = PaddleOCRSettingsViewModel.PythonExecutablePath; // 替换为你的Python解释器路径

          if (SelectedFilePath == null)
          {
              return;
          }

          string arguments = SelectedFilePath; // 替换为你要传递的参数                                                                                                                                                                         

          // 创建一个 ProcessStartInfo 实例
          ProcessStartInfo start = new ProcessStartInfo();
          start.FileName = pythonExecutablePath;
          start.Arguments = $"\"{pythonScriptPath}\" {arguments} {selectedLanguage}";
          start.UseShellExecute = false;
          start.RedirectStandardOutput = true;
          start.CreateNoWindow = true;

          // 创建并启动进程
          using (Process process = Process.Start(start))
          {
              using (System.IO.StreamReader reader = process.StandardOutput)
              {
                  string result = reader.ReadToEnd();
                  OCRText = result;
              }
          }
      });
  }
```

需要注意的地方在这几处：

![image-20241213192910898](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241213192910898.png)

Python解释器路径为虚拟环境中的Python解释器，我这里如下所示：

![image-20241213193105587](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241213193105587.png)

![image-20241213193142433](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241213193142433.png)

在此处传入Python脚本路径与设置的参数。

以上就是今天分享的C#调用Python脚本的第一种方式，下期介绍第二种方式，这两种方式在项目中都使用了，感兴趣的朋友，可以从GitHub获取源码进行实践学习。
