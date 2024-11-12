# WPF Prism 01-BootstrapperShell

## Prism介绍

Prism 是一个用于在 WPF、.NET MAUI、Uno 平台和 Xamarin Forms 中构建松耦合、可维护和可测试的 XAML 应用程序的框架。每个平台都有单独的发布版本，并且这些版本将在独立的开发时间线上进行开发。Prism 提供了一组设计模式的实现，这些模式有助于编写结构良好且可维护的 XAML 应用程序，包括 MVVM、依赖注入、命令、EventAggregator 等。Prism 的核心功能是通过交叉编译的 .NET Standard 和 .NET 4.5/4.8 库中的共享代码库实现的。需要平台特定的功能则在目标平台的相应库中实现。Prism 还提供了这些模式与目标平台的出色集成。例如，Xamarin Forms 的 Prism 允许你使用可单元测试的导航抽象，但该抽象建立在平台导航概念和 API 之上，因此你可以充分利用平台本身提供的功能，但以 MVVM 的方式实现。

Github地址：https://github.com/PrismLibrary/Prism

文档地址：https://docs.prismlibrary.com/docs

## Prism入门

Prism的入门可以看Prism的入门示例+官方文档。

Prism示例GitHub：https://github.com/PrismLibrary/Prism-Samples-Wpf

## 01-BootstrapperShell

今天学习的是第一个入门示例01-BootstrapperShell，项目结构如下图所示：

![image-20241112083923204](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112083923204.png)

与普通的WPF程序的差别主要是在这两个地方：

![image-20241112084011218](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112084011218.png)

![image-20241112084042268](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112084042268.png)

### BootstrapperShell介绍

在WPF应用中，`Bootstrapper`是一个负责初始化应用程序框架和执行应用程序启动逻辑的组件。Bootstrapper的概念借鉴了依赖注入（Dependency Injection, DI）和容器（Container）的模式，用于管理和解析应用程序中的依赖关系。

Bootstrapper的主要职责包括：

1. **初始化依赖注入容器**：Bootstrapper负责创建和配置依赖注入容器，用于管理和解析应用程序中的依赖关系。
2. **注册应用程序组件和服务**：Bootstrapper负责将应用程序中的组件和服务注册到依赖注入容器中，以便在需要时进行解析和注入。
3. **执行应用程序启动逻辑**：Bootstrapper负责执行应用程序的启动逻辑，包括创建主窗口、初始化应用程序状态、启动后台任务等。
4. **支持设计时和运行时行为的分离**：Bootstrapper可以配置为在设计和运行时执行不同的逻辑，以支持设计时工具和运行时行为的灵活分离。

### 方法介绍

目前我们遇到了两个方法：

```csharp
 class Bootstrapper : PrismBootstrapper
 {
     protected override DependencyObject CreateShell()
     {
         return Container.Resolve<MainWindow>();
     }

     protected override void RegisterTypes(IContainerRegistry containerRegistry)
     {
         
     }
 }
```

目前只需要知道这两个方法干嘛用的即可。

`CreateShell`方法用于创建应用程序的主窗口（Shell）。在Prism框架中，Shell通常是应用程序的主UI容器，它可能包含菜单、工具栏、状态栏等。

当`Bootstrapper`的`InitializeShell`方法被调用时，它会查找并实例化Shell。`CreateShell`方法负责返回这个Shell的实例。

```csharp
protected override DependencyObject CreateShell()
{
    return Container.Resolve<MainWindow>();
}
```

在上面的代码示例中，`CreateShell`方法通过依赖注入容器（`Container`）解析并返回`MainWindow`的实例。

`RegisterTypes`方法用于在依赖注入容器中注册应用程序中使用的各种类型。这是实现依赖注入的关键步骤，它允许框架在需要时自动创建和管理对象的生命周期。

在`RegisterTypes`方法中，你可以使用`IContainerRegistry`接口提供的方法来注册类型。例如，你可以注册视图、视图模型、服务等的类型。

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // 在这里注册应用程序中使用的类型
}
```

在上面的代码示例中，`RegisterTypes`方法被重写，但具体的注册代码被省略了。在实际的应用程序中，你需要在这里添加代码来注册你的应用程序中使用的各种类型。

注意到这里的`Container.Resolve<MainWindow>();`中的`Resolve`的含义。

Resolve在编程和依赖注入中的含义主要有如下：

1、**解析依赖关系**：

- “Resolve”指的是确定并获取一个对象所依赖的其他对象的过程。
- 在依赖注入容器中，解析依赖关系通常意味着从容器中检索已注册的依赖项实例。

2、**解析对象实例**：

- “Resolve”也可以指直接从依赖注入容器中创建并返回一个特定类型的对象实例。
- 这个过程通常会自动处理该对象所需的所有依赖项。

### 程序启动流程

在WPF应用程序启动时会调用`OnStartup`方法：

![image-20241112091046551](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112091046551.png)

默认的启动流程，一般会查找名为 MainWindow.xaml 的窗口文件，并创建它的实例，然后显示该窗口，如下所示：

![image-20241112092000507](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112092000507.png)

这里变成了创建一个Bootstrapper类对象，然后执行该对象的Run方法。

Run方法定义在PrismBootstrapperBase类中，如下所示：

![image-20241112092313785](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241112092313785.png)

简单理解就是在WPF应用中创建一个BootstrapperShell可以在应用创建时执行一些自定义操作，就是在显示主窗体之外，多做了一些事情。





