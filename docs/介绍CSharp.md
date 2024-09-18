# C#语言介绍

## C#概述

C# 语言是适用于 [.NET](https://learn.microsoft.com/zh-cn/dotnet/csharp/) 平台（免费的跨平台开源开发环境）的最流行语言。 C# 程序可以在许多不同的设备上运行，从物联网 (IoT) 设备到云以及介于两者之间的任何设备。 可为手机、台式机、笔记本电脑和服务器编写应用。

C# 是一种跨平台的通用语言，可以让开发人员在编写高性能代码时提高工作效率。 C# 是数百万开发人员中最受欢迎的 .NET 语言。 C# 在生态系统和所有 .NET [工作负载](https://learn.microsoft.com/zh-cn/dotnet/standard/glossary#workload)中具有广泛的支持。 基于面向对象的原则，它融合了其他范例中的许多功能，尤其是函数编程。 低级功能支持高效方案，无需编写不安全的代码。 大多数 .NET 运行时和库都是用 C# 编写的，C# 的进步通常会使所有 .NET 开发人员受益。

## 熟悉的 C# 功能

C# 对于初学者而言很容易上手，但同时也为经验丰富的专业应用程序开发人员提供了高级功能。 你很快就能提高工作效率。 你可以根据应用程序的需要学习更专业的技术。

C# 应用受益于 .NET 运行时的[自动内存管理](https://learn.microsoft.com/zh-cn/dotnet/standard/automatic-memory-management)。 C# 应用还可以使用 .NET SDK 提供的丰富[运行时库](https://learn.microsoft.com/zh-cn/dotnet/standard/runtime-libraries-overview)。 有些组件独立于平台，例如文件系统库、数据集合与数学库。 还有一些组件特定于单个工作负载，例如 ASP.NET Core Web 库或 .NET MAUI UI 库。 [NuGet](https://nuget.org/) 的丰富开源生态系统增强了作为运行时一部分的库。 这些库提供更多可用的组件。

C# 属于 C 语言家族。 如果你使用过 C、C++、JavaScript 或 Java，那么也会熟悉 [C# 语法](https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/)。 与 C 语言家族中的所有语言一样，分号 (`;`) 定义语句的结束。 C# 标识符区分大小写。 C# 同样使用大括号（`{` 和 `}`）、控制语句（例如 `if`、`else` 和 `switch`）以及循环结构（例如 `for` 和 `while`）。 C# 还具有适用于任何集合类型的 `foreach` 语句。

C# 是一种强类型语言。 声明的每个变量都有一个在编译时已知的类型。 编译器或编辑工具会告诉你是否错误地使用了该类型。 可以在运行程序之前修复这些错误。 以下[基础数据类型](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/types/)内置于语言和运行时中：值类型（例如 `int`、`double`、`char`）、引用类型（例如 `string`）、数组和其他集合。 编写程序时，你会创建自己的类型。 这些类型可以是值的 `struct` 类型，也可以是定义面向对象的行为的 `class` 类型。 可以将 `record` 修饰符添加到 `struct` 或 `class` 类型，以便编译器合成用于执行相等性比较的代码。 还可以创建 `interface` 定义，用于定义实现该接口的类型必须提供的协定或一组成员。 还可以定义泛型类型和方法。 [泛型](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/types/generics)使用类型参数为使用的实际类型提供占位符。

编写代码时，可以将函数（也称为[方法](https://learn.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/methods)）定义为 `struct` 和 `class` 类型的成员。 这些方法定义类型的行为。 可以使用不同数量或类型的参数来重载方法。 方法可以选择性地返回一个值。 除了方法之外，C# 类型还可以带有[属性](https://learn.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/properties)，即由称作访问器的函数支持的数据元素。 C# 类型可以定义[事件](https://learn.microsoft.com/zh-cn/dotnet/csharp/events-overview)，从而允许类型向订阅者通知重要操作。 C# 支持面向对象的技术，例如 `class` 类型的继承和多形性。

C# 应用使用[异常](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/exceptions/)来报告和处理错误。 如果你使用过 C++ 或 Java，则也会熟悉这种做法。 当无法执行预期的操作时，代码会引发异常。 其他代码（无论位于调用堆栈上面的多少个级别）可以选择性地使用 `try` - `catch` 块进行恢复。

## 独特的 C# 功能

你可能不太熟悉 C# 的某些元素。 [语言集成查询 (LINQ)](https://learn.microsoft.com/zh-cn/dotnet/csharp/linq/) 提供一种基于模式的通用语法来查询或转换任何数据集合。 LINQ 统一了查询内存中集合、结构化数据（例如 XML 或 JSON）、数据库存储，甚至基于云的数据 API 的语法。 你只需学习一套语法即可搜索和操作数据，无论其存储在何处。以下查询查找平均学分大于 3.5 的所有学生：

```csharp
var honorRoll = from student in Students
                where student.GPA > 3.5
                select student;
```

上面的查询适用于 `Students` 表示的许多存储类型。 它可以是对象的集合、数据库表、云存储 Blob 或 XML 结构。 相同的查询语法适用于所有存储类型。

使用[基于任务的异步编程模型](https://learn.microsoft.com/zh-cn/dotnet/csharp/asynchronous-programming/)，可以编写看起来像是同步运行的代码，即使它是异步运行的。 它利用 `async` 和 `await` 关键字来描述异步方法，以及表达式何时进行异步计算。以下示例等待异步 Web 请求。 异步操作完成后，该方法返回响应的长度：

```csharp
public static async Task<int> GetPageLengthAsync(string endpoint)
{
    var client = new HttpClient();
    var uri = new Uri(endpoint);
    byte[] content = await client.GetByteArrayAsync(uri);
    return content.Length;
}
```

C# 还支持使用 `await foreach` 语句来迭代由异步操作支持的集合，例如 GraphQL 分页 API。 以下示例以块的形式读取数据，并返回一个迭代器，该迭代器提供对每个可用元素的访问：

```csharp
public static async IAsyncEnumerable<int> ReadSequence()
{
    int index = 0;
    while (index < 100)
    {
        int[] nextChunk = await GetNextChunk(index);
        if (nextChunk.Length == 0)
        {
            yield break;
        }
        foreach (var item in nextChunk)
        {
            yield return item;
        }
        index++;
    }
}
```

调用方可以使用 `await foreach` 语句迭代该集合：

```csharp
await foreach (var number in ReadSequence())
{
    Console.WriteLine(number);
}
```

C# 提供[模式匹配](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/functional/pattern-matching)。 这些表达式使你能够检查数据并根据其特征做出决策。 模式匹配为基于数据的控制流提供了极好的语法。 以下代码演示如何使用模式匹配语法来表达布尔 and、or 和 xor 运算的方法：

```csharp
public static bool Or(bool left, bool right) =>
    (left, right) switch
    {
        (true, true) => true,
        (true, false) => true,
        (false, true) => true,
        (false, false) => false,
    };

public static bool And(bool left, bool right) =>
    (left, right) switch
    {
        (true, true) => true,
        (true, false) => false,
        (false, true) => false,
        (false, false) => false,
    };
public static bool Xor(bool left, bool right) =>
    (left, right) switch
    {
        (true, true) => false,
        (true, false) => true,
        (false, true) => true,
        (false, false) => false,
    };
```

可以通过对任何值统一使用 `_` 来简化模式匹配表达式。 以下示例演示如何简化 and 方法：

```csharp
public static bool ReducedAnd(bool left, bool right) =>
    (left, right) switch
    {
        (true, true) => true,
        (_, _) => false,
    };
```

最后，作为 .NET 生态系统的一部分，你可以将 [Visual Studio](https://visualstudio.microsoft.com/vs) 或 [Visual Studio Code](https://code.visualstudio.com/) 与 [C# DevKit](https://code.visualstudio.com/docs/csharp/get-started) 配合使用。 这些工具可以全方位地理解 C# 语言，包括你编写的代码。 它们还提供调试功能。

## 来源

[概述 - A tour of C# | Microsoft Learn](https://learn.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/overview)



