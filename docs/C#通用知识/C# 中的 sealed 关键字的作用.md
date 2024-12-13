# C# 中的 sealed 关键字

在C#中，`sealed`关键字用于阻止一个类被继承，或者阻止一个成员（如方法、属性、索引器或事件）被重写。当一个类被声明为sealed时，它不能有子类，也就是说，不能有其他类从它继承。

当一个成员（如方法、属性、索引器或事件）被声明为sealed时，它不能在派生类中被重写。这对于阻止派生类修改特定成员的行为非常有用。

使用`sealed`关键字的基本语法如下：

```csharp
sealed class MyClass
{
    // ...
}

sealed override void MyMethod()
{
    // ...
}
```

需要注意的是，`sealed`关键字只能与`class`一起使用，不能与`interface`或`abstract`类一起使用。同时，`sealed`关键字只能与`override`关键字一起使用，不能单独用于方法、属性、索引器或事件。

例如，下面的代码定义了一个sealed类和一个sealed方法：

```csharp
public class MyBaseClass
{
    public virtual void MyMethod()
    {
        Console.WriteLine("Base class method.");
    }
}

public sealed class MySealedClass : MyBaseClass
{
    public sealed override void MyMethod()
    {
        Console.WriteLine("Sealed class method.");
    }
}
```

在这个例子中，`MySealedClass`不能被继承，而`MyMethod`不能在任何派生类中被重写。