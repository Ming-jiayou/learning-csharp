# WPF Prism ViewDiscovery

## ViewDiscovery介绍

在 WPF Prism 应用程序中，View Discovery 是一个用于发现和注册视图（View）的机制。它允许您将视图与特定的区域（Region）相关联，并在运行时自动将视图添加到该区域中。

以下是 View Discovery 的一些关键概念：

1. **区域（Region）**：区域是 UI 中的一个逻辑部分，用于容纳视图。例如，主窗口的左侧面板可以是一个区域，右侧面板可以是另一个区域。
2. **视图（View）**：视图是用户界面的一个部分，通常实现为一个 UserControl 或一个自定义控件。
3. **视图发现（View Discovery）**：视图发现是指在运行时自动发现和注册视图的过程。

Prism 提供了以下几种视图发现机制：

1. **显式注册（Explicit Registration）**：您可以在代码中显式注册视图，将其与特定的区域相关联。
2. **自动发现（Auto-Discovery）**：Prism 可以自动发现视图，并将其注册到相应的区域中。
3. **基于约定的发现（Convention-based Discovery）**：您可以定义一个约定，用于指定视图的名称和区域的名称，Prism 会根据约定自动注册视图。

使用 View Discovery 的好处包括：

1. **解耦**：视图和区域之间的耦合度降低。
2. **灵活性**：您可以轻松地添加或移除视图，而无需修改代码。
3. **可重用性**：视图可以在多个区域中重用。

## ViewDiscovery示例

ViewDiscovery示例在https://github.com/PrismLibrary/Prism-Samples-Wpf/tree/master/04-ViewDiscovery

项目结构如下图所示：

![image-20241112095955525](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112095955525.png)

在MainWindow.xaml中定义了一个区域：

![image-20241112100056125](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112100056125.png)

在MainWindow.xaml.cs中进行了视图发现：

![image-20241112100239428](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112100239428.png)

因此该区域就会显示ViewA，如下所示：

![image-20241112100412944](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112100412944.png)

我们可以创建一个ViewB再把区域与ViewB联系起来：

```xaml
<Grid>
    <TextBlock Text="View B" FontSize="38" />     
</Grid>
```

```csharp
 //view discovery
 regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewB));
```

![image-20241112100831033](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112100831033.png)

## IRegionManager是什么时候注入容器的？

实现视图发现用到了`IRegionManager` ，它是一个用于管理区域（Region）的接口。

![image-20241113080611053](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113080611053.png)

看到这里，可能大家和我一样，都有个疑问，就是IRegionManager是什么时候注入容器的，为什么这里可以直接使用了呢？会不会是初始化的时候，Prism框架就已经把它注入了呢？我们的猜测是正确的。

查看Prism的源代码，在PrismApplicationBase中看到这个：

![image-20241113081246992](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113081246992.png)

这就是在注册Prism框架所需的一些自带服务。查看它的实现：

![image-20241113081350047](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113081350047.png)

继续查看：

![image-20241113081507956](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113081507956.png)

我们会发现IRegionManager与它的实现类RegionManager是在这里注入容器的，所有我们在主窗体的构造函数中可以直接使用了。

## RegisterViewWithRegion方法介绍

RegisterViewWithRegion方法通过注册一个类型来将视图与区域关联起来，当该区域被显示时，这个类型会被ServiceLocator解析成一个具体的实例。这个实例会被添加进区域的视图集合中。

![image-20241113082310113](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113082310113.png)

现在只要知道这个方法的含义与用法就行，具体实现先不管，慢慢来从会用开始。
