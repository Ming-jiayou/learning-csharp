# C#泛型

在C#中，泛型是一种强大的功能，它允许程序员在类、接口、方法或委托中定义类型参数，从而创建高度可重用和类型安全的代码。使用泛型，你可以编写一次代码，然后在运行时使用多种不同的数据类型。以下是泛型的一些关键概念和用法：

### 1. 泛型类型

泛型类型允许你定义一个类或接口，它在实例化时可以接受一个或多个类型参数。例如，`List<T>` 是一个泛型类，其中 `T` 是类型参数，表示列表中元素的类型。

```csharp
public class Box<T>
{
    private T _item;

    public void Set(T item)
    {
        _item = item;
    }

    public T Get()
    {
        return _item;
    }
}

// 使用泛型类
Box<int> intBox = new Box<int>();
intBox.Set(10);
int result = intBox.Get(); // result 是 int 类型
```

### 2. 泛型方法

泛型方法允许你在方法中使用类型参数，这样你就可以用不同类型的参数调用同一个方法。这可以提高代码的灵活性和复用性。

```csharp
public static T Max<T>(T x, T y)
{
    return x.CompareTo(y) > 0 ? x : y;
}

// 使用泛型方法
int maxInt = Max(5, 10);
string maxString = Max("apple", "banana");
```

### 3. 泛型约束

为了确保泛型类型或方法的正确使用，你可以为类型参数添加约束。这可以限制类型参数的范围，确保它们满足某些条件。

```csharp
public class Box<T> where T : class, new()
{
    public T Value { get; set; }

    public Box()
    {
        Value = new T(); // 由于约束，可以调用 new T()
    }
}
```

在这个例子中，`T` 必须是一个引用类型（`class`），并且必须有一个无参数的构造函数（`new()`）。

### 4. 泛型接口

你还可以定义泛型接口，这些接口可以被泛型类或非泛型类实现。

```csharp
public interface IComparer<T>
{
    int Compare(T x, T y);
}

public class IntComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return x.CompareTo(y);
    }
}
```

### 5. 泛型委托

泛型委托允许你定义接受类型参数的委托类型，这可以让你创建更灵活的事件处理程序和回调函数。

```csharp
public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

Func<int, int, int> add = (x, y) => x + y;
int sum = add(5, 3); // sum is 8
```

泛型在C#中提供了强大的类型安全和代码复用能力，是现代C#编程中不可或缺的一部分。