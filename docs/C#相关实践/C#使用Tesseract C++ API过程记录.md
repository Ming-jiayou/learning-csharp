# C#使用Tesseract C++ API过程记录

## Tesseract

Tesseract 是一个开源的光学字符识别（OCR）引擎，最初由 Hewlett-Packard（惠普）实验室开发，后来由 Google 收购并继续维护和开源贡献。Tesseract 可以识别多种语言的文字，广泛应用于将图片或扫描文档中的文本内容转换成可编辑的文本格式。随着深度学习技术的发展，Tesseract 也整合了基于深度神经网络的 OCR 模型，提升其识别准确率，特别是对于复杂排版和手写体的识别效果有所改善。

Tesseract 适合开发人员和研究人员使用，可以嵌入到各种应用中，比如文档数字化、图像处理软件、内容管理系统等。它支持命令行操作，也提供了丰富的 API 接口，支持 C++、Python、Java、Node.js 等多种编程语言，便于集成和调用。Tesseract 的核心功能包括文本检测、字符识别和后处理纠错，能够处理多种图像输入格式，输出包括纯文本、HOCR（HTML + OCR）格式、PDF 等多种格式。Tesseract 的高灵活性和强大的识别能力使其成为 OCR 领域中非常受欢迎的工具之一。

GitHub地址：https://github.com/tesseract-ocr/tesseract

![image-20241227152937200](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227152937200.png)

Tesseract提供了丰富的 API 接口，支持 C++、Python、Java、Node.js 等多种编程语言，没有C#的，实际上已经有大佬做了C#的封装了，并提供了一个示例项目，需要只是简单使用一下，用这个大佬的就很方便了。

感兴趣的可以瞧瞧：

项目GitHub地址：https://github.com/charlesw/tesseract

![image-20241227153302569](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227153302569.png)

示例GitHub地址：https://github.com/charlesw/tesseract-samples

![image-20241227153437445](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227153437445.png)

但这不是我们今天的主题，现在还处于学习阶段，能直接使用大佬的库确实很方便，但是如果自己能够知道大佬是怎么实现的，那不是也很酷吗？

实现的方式与大佬项目的方式是类似的，如下所示：

![image-20241227153822517](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227153822517.png)

需要依赖leptonica-1.82.0.dll与tesseract50.dll，然后通过DllImport导入其中的C++函数。

已经有现成的库了为什么不直接使用呢？

第一，项目中可能只需要用到Tesseract的几个C++ API而已，直接引用一大堆东西没有必要。第二，学习阶段，以自己学习掌握技能为主，自己先掌握了这项技能，然后偷懒了直接使用大佬的库也不迟。

## Windows编译Tesseract

首先我们需要先在Windows上编译Tesseract，官方文档有一些介绍，文档地址：https://tesseract-ocr.github.io/tessdoc/Compiling.html。

查看文档之后，我使用这种方式：

![image-20241227154722530](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227154722530.png)

先来简单介绍一下vcpkg。

**vcpkg**

vcpkg是一个用来管理C++库的跨平台包管理工具，由微软开发并维护，旨在帮助开发者简化第三方库的集成和使用过程。vcpkg通过提供预编译的二进制包和源代码，使开发者能够在Windows、Linux和macOS等操作系统上轻松安装和管理C++库。它支持多种编译器，包括Visual Studio、GCC和Clang。vcpkg的使用非常简单，只需要下载并安装，然后通过命令行工具指定要安装的库名，vcpkg会自动下载、编译并安装所需的库及其依赖项。此外，vcpkg还具有版本控制功能，能够方便地切换库的不同版本。它对于提升开发效率、保持项目的一致性以及解决跨平台开发中的库兼容性问题非常有帮助。许多开源项目和商业软件都选择使用vcpkg来管理和分发依赖库。

**使用vcpkg安装Tesseract**

```cmd
git clone https://github.com/microsoft/vcpkg.git
```

```cmd
cd vcpkg; .\bootstrap-vcpkg.bat
```

```cmd
vcpkg install tesseract:x64-windows
```

安装完成：

![image-20241226105928367](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241226105928367.png)

```cmd
vcpkg integrate install
```

![image-20241226110526085](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241226110526085.png)

为这个 vcpkg 根目录应用了全局用户集成。 CMake 项目应使用："-DCMAKE_TOOLCHAIN_FILE=D:/Learning/vcpkg/scripts/buildsystems/vcpkg.cmake"

现在所有 MSBuild C++ 项目都可以 #include 任何已安装的库。链接将会自动处理。安装新库后，它们将立即可用。

```cmd
vcpkg list
```

![image-20241226110859781](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241226110859781.png)

**新建一个C++项目使用Tesseract C++ API**

我写了两个简单的函数用于测试。

头文件：

```c++
#pragma once
extern "C" __declspec(dllexport) char* getChineseText(const char* imagePath);
extern "C" __declspec(dllexport) char* getEnglishText(const char* imagePath);
extern "C" __declspec(dllexport) void freeMemory(char* ptr);
```

源文件：

```c++
#include <tesseract/baseapi.h>
#include <leptonica/allheaders.h>
#include "test.h"
#include <iostream>

void windows_cmd_support_utf8(void)
{
   system("chcp 65001 & cls"); //cls 用来清除 chcp 的输出
}

char* getEnglishText(const char* imgPath) {
    tesseract::TessBaseAPI* api = new tesseract::TessBaseAPI();
    if (api->Init(NULL, "eng")) {
        fprintf(stderr, "Could not initialize tesseract.\n");
        delete api;
        return nullptr;
    }

    Pix* image = pixRead(imgPath);
    if (!image) {
        fprintf(stderr, "Could not read image file.\n");
        api->End();
        delete api;
        return nullptr;
    }

    api->SetImage(image);
    char* outText = api->GetUTF8Text();
    if (!outText) {
        fprintf(stderr, "OCR failed.\n");
        api->End();
        pixDestroy(&image);
        delete api;
        return nullptr;
    }
  

    api->Clear();
    api->End();
    delete api;
    pixDestroy(&image);

    return outText;
}

char* getChineseText(const char* imgPath) {
    tesseract::TessBaseAPI* api = new tesseract::TessBaseAPI();
    if (api->Init(NULL, "chi_sim")) {
        fprintf(stderr, "Could not initialize tesseract.\n");
        delete api;
        return nullptr;
    }

    Pix* image = pixRead(imgPath);
    if (!image) {
        fprintf(stderr, "Could not read image file.\n");
        api->End();
        delete api;
        return nullptr;
    }

    api->SetImage(image);
    char* outText = api->GetUTF8Text();
    if (!outText) {
        fprintf(stderr, "OCR failed.\n");
        api->End();
        pixDestroy(&image);
        delete api;
        return nullptr;
    }


    api->Clear();
    api->End();
    delete api;
    pixDestroy(&image);

    return outText;
}

void freeMemory(char* ptr) {
    delete[] ptr;
}

int main()
{ 
    const char* imgPath = "D:\\SemanticKernel-Test2.png";  // 替换为你的图像文件路径

    const char* imgPath2 = "D:\\test666.png";  // 替换为你的图像文件路径

    // 第一次调用
    char* result1 = getChineseText(imgPath);

    windows_cmd_support_utf8();

    std::cout << "OCR Result 1: " << result1 << std::endl;

    // 第二次调用
    char* result2 = getChineseText(imgPath2);
    std::cout << "OCR Result 2: " << result2 << std::endl;

    // 释放内存
    //freeMemory(result1);
    //freeMemory(result2);
    return 0;  // 程序正常结束

}
```

**注意📍📍📍**

先将项目配置成X64:

![image-20241227160709604](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227160709604.png)

现在运行项目，会出现一个错误，因为并没有包含tessdata。

`tessdata` 是 Tesseract OCR 引擎使用的一种数据文件格式，用于存储语言模型和字符识别数据。Tesseract 通过加载这些数据文件来实现对不同语言文字的识别。每个语言都有一套对应的 `tessdata` 文件，通常命名为 `lang.traineddata`，其中 `lang` 是语言的缩写（例如，`eng` 表示英语，`chi_sim` 表示简体中文）。

tessdata的GitHub地址：https://github.com/tesseract-ocr/tessdata

![image-20241227185027622](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227185027622.png)

也可以看我后面分享的GitHub，一般只要中英文就可以了，如下所示：

![image-20241227185504978](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227185504978.png)

将tessdata文件夹放在x64的Debug目录下即可。

先测试中文识别效果：

测试图片1

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/SemanticKernel-Test2.png)

测试图片2

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/test666.png)

查看效果：

![image-20241227155928432](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227155928432.png)

**注意📍📍📍**

如果不加上windows_cmd_support_utf8();

就会出现乱码，如下所示：

![image-20241227160300511](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227160300511.png)

并不是Tesseract识别中文效果不好，只是控制台默认没支持utf-8罢了。

再来测试一下英文：

测试图片1

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/SemanticKernel-Test1.png)

测试图片2

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_20241226223741.png)

效果如下所示：

![image-20241227190017513](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227190017513.png)

**生成DLL**

测试没有问题之后，现在需要生成DLL文件。

右键项目，点击属性，设置配置类型为DLL：

![image-20241227190254191](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227190254191.png)

生成解决方案之后，如下所示：

![image-20241227190504150](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227190504150.png)

这里需要搞清楚的是为什么头文件不用std::string而是char*呢？

```c++
#pragma once
extern "C" __declspec(dllexport) char* getChineseText(const char* imagePath);
extern "C" __declspec(dllexport) char* getEnglishText(const char* imagePath);
extern "C" __declspec(dllexport) void freeMemory(char* ptr);
```

- **`extern "C"`**：这告诉编译器这些函数应该按照C语言的方式进行链接，而不是C++的方式。这样可以确保这些函数在C语言中也可以被正确调用。
- 简单的说std::string不是C语言风格的，所以不行。

## 在C#项目中调用C++ DLL

新建一个C#控制台项目用于测试。

测试代码如下：

```c#
using System.Runtime.InteropServices;

class Program
{
    [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern IntPtr getEnglishText(string imagePath);

    [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern IntPtr getChineseText(string imagePath);


    static void Main()
    {
        

        string imagePath = @"D:\SemanticKernel-Test2.png";

        // 调用 DLL 中的函数
        IntPtr resultPtr = getChineseText(imagePath);
        if (resultPtr == IntPtr.Zero)
        {
            return;
        }

        string? result = Marshal.PtrToStringUTF8(resultPtr);
        return; ;

    }
}
```

将项目设置成X64平台，将C++项目的所有输出文件，复制到X64的输出目录这里，如下所示：

![image-20241227191524217](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227191524217.png)

为了避免太混乱，本来想新建一个Libs文件夹，在放这些文件，在设置为嵌入的资源与如果较新就复制，但是就调用不了了，暂时没有解决，只能这样一堆放在这里了。

效果如下：

![image-20241227191730397](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227191730397.png)

这里需要注意一下，为什么是

```c#
  [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
  public static extern IntPtr getEnglishText(string imagePath);
```

而不是

```c#
  [DllImport("TesseractApiTest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
  public static extern string getEnglishText(string imagePath);
```

在C#中直接使用 `string` 作为返回类型并不适用于从C++导出的函数，尤其是当该函数返回的是一个 `char*` 类型的指针时。原因在于 ` char*` 是一个指向C风格字符串的指针，而C#中的 `string` 类型与C风格字符串并不直接兼容。C#的 `string` 类型是一个托管的字符串对象，而 ` char*` 是一个非托管的指针，直接进行转换会导致运行时错误或无法预期的行为。

使用 `IntPtr` 作为返回类型可以解决这个问题，因为 `IntPtr` 是一个可以表示非托管指针的类型。你可以通过 `Marshal` 类将 `IntPtr` 转换为C#中的 `string`。这样可以确保你在C#中能够正确处理C++函数返回的字符串指针。

## TesseractOCR-GUI中集成

之前跟大家分享的TesseractOCR-GUI需要在电脑上安装Tesseract才能用，因为只是简单的对Tesseract的命令行使用做了封装，现在通过这种方法，不需要安装Tesseract也能使用了。

GitHub地址：https://github.com/Ming-jiayou/TesseractOCR-GUI。

![image-20241227192409247](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227192409247.png)

git clone到本地，然后将平台设置成X64，先生成解决方案，然后将Libs文件夹下的内容，全部复制到x64的输出目录，如下所示：

![image-20241227192607954](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227192607954.png)

![image-20241227192639528](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227192639528.png)

现在直接点击应该就可以使用了：

![image-20241227192803222](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227192803222.png)

效果如下所示：

![image-20241227193007146](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241227193007146.png)

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/test.gif)

速度还是很快的，目前为止我们尝试了TesseractOCR、PaddleOCR、VLM，其中TesseractOCR我感觉是最快的。

以上就是本期的分享内容，希望对你有所帮助。

推荐阅读：

[C#调用C++代码，以OpenCV为例](https://mp.weixin.qq.com/s/6U_dWQxbOmWNzKrwt0RnFA)

[C#调用Python脚本的方式（一），以PaddleOCR-GUI为例](https://mp.weixin.qq.com/s/A7-SbXyDOOd56JC9N7uOJg)

[C#调用Python代码的方式（二），以PaddleOCR-GUI为例](https://mp.weixin.qq.com/s/AnzbLyIH5cSMzaLdqWvjiA)

[VLM-OCR-Demo：一个使用VLM用于OCR任务的示例](https://mp.weixin.qq.com/s/Pfi8tL_N6hLuxZAKpRjQSA)

[TesseractOCR-GUI：基于WPF/C#构建TesseractOCR简单易用的用户界面](https://mp.weixin.qq.com/s/t_C360h6AP9P4GfssZnkbA)

[使用Tesseract进行图片文字识别](https://mp.weixin.qq.com/s/C2o0-RtubtQb4pzys2wx6w)





