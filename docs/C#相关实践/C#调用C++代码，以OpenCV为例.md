# C#调用C++代码，以OpenCV为例

## 前言

使用C#调用C++代码是一个很常见的需求，因此本文以知名的C++机器视觉库OpenCV为例，说明在C#中如何通过使用P/Invoke（平台调用）来调用C++代码。只是以OpenCV为例，实际上在C#中使用OpenCV可以使用OpenCVSharp这个项目，这是一个很优秀的项目，GitHub地址：https://github.com/shimat/opencvsharp。

![image-20241223143622622](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223143622622.png)

但是也有时候确实有需要调用C++代码的情况，因此大概知道应该怎么实现也是很有必要的，其他的C++库也是类似的方法。

## 下载安装OpenCV并生成DLL文件

OpenCV（Open Source Computer Vision Library）是一个开源的计算机视觉库，由Intel公司于1999年开始开发，并由 Willow Garage 继续维护。它支持多种编程语言，包括 Python、C++ 和 Java，可以在 Windows、Linux、MacOS、Android 和 iOS 等多个平台上运行。OpenCV 包含了各种图像处理和计算机视觉算法，可以用于人脸检测、物体识别、图像分割、特征提取、图像转换、相机标定等任务，应用领域广泛，例如安全监控、医疗图像分析、人机交互、AR/VR、工业检测等。OpenCV的高效性和灵活性使其成为计算机视觉研究和开发领域的首选工具之一。无论是学术研究还是工业应用，OpenCV都提供了强大的支持。目前，OpenCV 已经成为全球最流行的计算机视觉库之一，拥有庞大的开发者社区，并不断地推出新版本以支持最新的计算机视觉算法和技术。对于计算机视觉初学者来说，学习和使用 OpenCV 可以获得大量的资源和支持，对于有经验的研究者和开发人员来说，OpenCV 也是非常有价值的工具。

进入官网，官网地址：https://opencv.org/releases/。

下载最新的版本：

![image-20241223144221041](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223144221041.png)

安装完成之后，设置环境变量：

![image-20241223144400194](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223144400194.png)

使用VS2022创建一个新的C++空项目：

![image-20241223144540506](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223144540506.png)

添加一个头文件与源文件。

在头文件中写入：

```c++
#pragma once
extern "C" __declspec(dllexport) void DisplayGrayImage(const char* imagePath);
```

在源文件中写入：

```c++
#include <opencv2/opencv.hpp>
#include <iostream>
#include"test.h"

void DisplayGrayImage(const char* imagePath) {
    // 1. 读取图像
    cv::Mat image = cv::imread(imagePath);
    if (image.empty()) {
        std::cerr << "无法读取图像，请检查文件路径: " << imagePath << std::endl;
        return;
    }

    // 2. 灰度转换
    cv::Mat gray;
    cv::cvtColor(image, gray, cv::COLOR_BGR2GRAY);

    // 3. 显示原图和灰度图
    cv::namedWindow("原图", cv::WINDOW_NORMAL);
    cv::imshow("原图", image);

    cv::namedWindow("灰度图", cv::WINDOW_NORMAL);
    cv::imshow("灰度图", gray);

    // 等待用户按键
    cv::waitKey(0);

    // 释放资源
    cv::destroyAllWindows();
}
int main() {
    // 调用函数，传入图片路径
    DisplayGrayImage("D:\\狗狗.jpg");
   
    return 0;
}
```

现在会发现有很多错误，如下所示：

![image-20241223145123078](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223145123078.png)

右键项目，点击属性：

![image-20241223145224725](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223145224725.png)

在包含目录中包含

D:\Learning\OpenCV\opencv\build\include

D:\Learning\OpenCV\opencv\build\include\opencv2

在库目录中包含

D:\Learning\OpenCV\opencv\build\x64\vc16\lib

设置完成之后，如下所示：

![image-20241223150157809](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223150157809.png)

点击链接器——输入

D:\Learning\OpenCV\opencv\build\x64\vc16\lib\opencv_world4100d.lib

如下所示：

![image-20241223150418678](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223150418678.png)

debug对应的是opencv_world4100d.lib，release对应的是opencv_world4100.lib。

现在会发现之前的报错已经消失了。

如下所示：

![image-20241223150535592](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223150535592.png)

点击看是否能重新运行：

![image-20241223150623062](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223150623062.png)

现在需要导出C++的DLL文件。

右键项目，点击属性，将配置类型修改为动态库，如下所示：

![image-20241223150841733](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223150841733.png)

重新生成解决方案：

![image-20241223151030585](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223151030585.png)

生成了一个DLL文件：

![image-20241223151111138](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223151111138.png)

## 创建C#控制台项目，并调用C++的DLL文件

创建一个C#控制台项目，测试代码如下所示：

![image-20241223151355886](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223151355886.png)

```c#
using System.Runtime.InteropServices;

class Program
{  
    [DllImport("Project2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern void DisplayGrayImage(string imagePath);

    static void Main()
    {
        DisplayGrayImage("D:\\狗狗2.jpg");
    }
}
```

运行查看效果，如下所示：

![image-20241223151455653](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223151455653.png)

因为要记住，第一步肯定是要把DLL文件复制过来，如下所示：

![image-20241223151659508](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223151659508.png)

再次运行，效果如下所示：

![image-20241223151811512](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241223151811512.png)

以上就是C#通过P/Invoke（平台调用）来调用C++代码的一个简单示例，希望对你有所帮助。