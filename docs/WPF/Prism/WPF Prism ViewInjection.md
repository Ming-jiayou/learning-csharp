# WPF Prism ViewInjection

## ViewInjection介绍

ViewInjection是Prism框架提供的一种机制，用于将视图动态地注入到指定的容器（Region）中。这种注入方式允许你在运行时动态地添加、移除或替换视图，从而实现更灵活的用户界面设计。

## ViewInjection示例

GitHub地址：https://github.com/PrismLibrary/Prism-Samples-Wpf/tree/master/05-ViewInjection

项目结构如下图所示：

![image-20241113084440305](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113084440305.png)

运行效果如下所示：

![ViewInjection](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/%E8%A7%86%E5%9B%BE%E6%B3%A8%E5%85%A5.gif)

实现的代码：

![image-20241113084824778](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113084824778.png)

上一篇我们了解到`IRegionManager`是一个非常重要的接口，它负责管理应用程序中的区域（Regions）。这里我们新接触到了`IContainerExtension`，它是干嘛的呢？

![image-20241113090329776](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241113090329776.png)

`IContainerExtension`接口继承了`IContainerProvider`和`IContainerRegistry`接口，结合了这两个接口的功能。

- `IContainerProvider`：提供依赖注入容器的解析功能，允许你从容器中解析服务。
- `IContainerRegistry`：提供注册服务到依赖注入容器的功能，允许你在容器中注册服务。

通过实现IContainerExtension接口，你可以创建一个扩展的依赖注入容器，既可以注册服务，也可以解析服务。这在构建模块化应用程序时非常有用，因为它允许你在不同的模块之间共享和管理依赖关系。

视图注入代码：

```csharp
 var view = _container.Resolve<ViewA>();
 IRegion region = _regionManager.Regions["ContentRegion"];
 region.Add(view);
```

1.	`var view = _container.Resolve<ViewA>();`
这行代码使用依赖注入容器（IContainerExtension）来解析并创建一个ViewA视图的实例。ViewA是一个用户控件或视图，应该已经在容器中注册过。
2.	`IRegion region = _regionManager.Regions["ContentRegion"];`
   这行代码从区域管理器（IRegionManager）中获取名为"ContentRegion"的区域。区域是Prism中的一个概念，用于定义视图可以被注入的地方。"ContentRegion"应该在XAML文件中定义，例如在MainWindow.xaml中。
3.	`region.Add(view);`
这行代码将之前创建的ViewA实例添加到"ContentRegion"区域中。这样，ViewA视图就会显示在"ContentRegion"中。

这里可能大家会有一个问题就是为什么直接从容器中能直接解析出ViewA呢？那说明ViewA已经注入到容器中了，那么ViewA是什么时候在哪里注入的呢？

就像`Container.Resolve<MainWindow>();`一样，Prism将主窗体与视图自动注入到容器中了，至于具体实现，我们现在先不管，先学会用再说。

