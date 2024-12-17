# C#调用Python代码的方式（二），以PaddleOCR-GUI为例

## 前言

前面介绍了在C#中使用Progress类调用Python脚本的方法，但是这种方法在需要频繁调用并且需要进行数据交互的场景效果并不好，因此今天分享的是C#调用Python代码的方式（二）：使用pythonnet调用Python代码。

## pythonnet介绍

Python.NET 是一个包，为 Python 程序员提供了与 .NET 公共语言运行时 (CLR) 几乎无缝的集成，并为 .NET 开发者提供了一个强大的应用程序脚本工具。它允许 Python 代码与 CLR 交互，也可以用于将 Python 嵌入到 .NET 应用程序中。

![image-20241217120710093](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217120710093.png)

## 使用pythonnet，以PaddleOCR-GUI为例

在使用pythonnet之前，需要搞清楚它的三个概念，分别是 `Runtime.PythonDLL`、`PythonEngine.PythonHome`、` PythonEngine.PythonPath `，搞清楚了这些，使用起来就很方便了。

![image-20241217120547972](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217120547972.png)

先来看Runtime.PythonDLL如何指定。

比如你用Python3.12创建了一个虚拟环境，但是在这个虚拟环境，找不到DLL文件，这时候你需要去原始的那个Python3.12文件夹中去找：

![image-20241217120934674](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217120934674.png)

我这里对应的路径是：C:\Users\25398\AppData\Local\Programs\Python\Python312\python312.dll。

再来看PythonEngine.PythonHome如何指定。

PythonEngine.PythonHome写你创建的虚拟环境中的python.exe，这里我对应的是：

![image-20241217121302447](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217121302447.png)

最后再来看看PythonEngine.PythonPath如何指定。

PythonEngine.PythonPath是指运行你的python代码所需的所有目录，包括你写的python代码所在的目录，虚拟环境的一些目录，原始环境的一些目录，这里我对应的如下所示：

```csharp
D:\Learning\PaddleOCR\;
D:\Learning\PaddleOCR\PaddleOCRVENV\Lib;
D:\Learning\PaddleOCR\PaddleOCRVENV\Lib\site-packages;
C:\Users\25398\AppData\Local\Programs\Python\Python312\Lib;
C:\Users\25398\AppData\Local\Programs\Python\Python312\Lib\site-packages;
C:\Users\25398\AppData\Local\Programs\Python\Python312\DLLs"
```

当你运行时提示没有叫XX的模块的时候，需要看看是否都包含了，比如刚开始我没有包含C:\Users\25398\AppData\Local\Programs\Python\Python312\DLLs的时候，会报一个没有XX模块的错误，加上之后就不会了。

现在ViewModel中对这些量进行赋值：

![image-20241217122302883](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217122302883.png)

这样使用即可：

```csharp
 using (Py.GIL())
 {
     dynamic example = Py.Import("test4");
     if (SelectedFilePath == null)
     {
         return;
     }
     string image_path = SelectedFilePath;
     string selected_language = selectedLanguage;
     string result = example.use_paddleocr(image_path, selected_language);
     OCRText = result;
 }
```

![image-20241217122342153](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217122342153.png)

对应的Python代码如下：

```python
import logging
from paddleocr import PaddleOCR

def use_paddleocr(image_path,selected_language):
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

    ocr = PaddleOCR(use_angle_cls=True, lang=selected_language)  # need to run only once to download and load model into memory
    result = ocr.ocr(image_path, cls=True)
    result1 = ""
    for idx in range(len(result)):
        res = result[idx]   
        for line in res:            
            result1 += line[1][0]
    return result1
```

实现的效果如下所示：

![image-20241217123301884](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217123301884.png)

还遇到的一个坑，就是同步运行没问题，但是改成异步运行，前两次可以，后面就不行了，需要加上这个：

![image-20241217123639735](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241217123639735.png)

以上就是本次分享的全部内容，全部源代码已上传到GitHub，地址：https://github.com/Ming-jiayou/PaddleOCR-GUI。



















