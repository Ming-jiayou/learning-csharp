# C# 属性

在 C# 中，属性（Property）是一种特殊的成员，它允许对象以类似于公共数据成员的方式访问私有字段。属性提供了对类的私有字段的读取或写入访问，同时保持封装性。属性可以有 getter 和 setter 方法，分别用于读取和修改字段的值。

要创建一个只读属性，你只需包含 getter 方法，而不包含 setter 方法。这将允许其他代码读取属性的值，但不允许修改它。以下是一个创建只读属性的示例：

```csharp
public class Person
{
    // 私有字段
    private int _age;

    // 只读属性
    public int Age
    {
        get
        {
            return _age;
        }
    }

    // 构造函数
    public Person(int age)
    {
        _age = age;
    }
}

// 使用只读属性
class Program
{
    static void Main(string[] args)
    {
        Person person = new Person(30);
        Console.WriteLine("Person's age: " + person.Age);
        
        // 下面这行代码会引发编译错误，因为 Age 属性是只读的
        // person.Age = 31;
    }
}
```

在这个示例中，`Age` 属性是只读的，不允许外部代码修改其值。但是，你仍然可以在类的内部（如构造函数中）修改 `_age` 字段的值。

## C#中属性与字段有什么区别？

在C#中，属性和字段是用于存储数据的两种不同方式，它们之间存在一些关键的区别：

1. **字段（Field）**：
   - 字段是类或结构中的基本存储单元，直接存储数据。
   - 字段可以是公共的（public）、私有的（private）、受保护的（protected）等，但通常建议将字段设为私有，以确保封装和数据安全。
   - 字段的访问是直接的，没有额外的逻辑或方法调用。
2. **属性（Property）**：
   - 属性是C#中用于封装字段访问的一种方法，它提供了一种更安全和更灵活的方式来访问和修改类的私有字段。
   - 属性看起来像公共字段，但在后台，它们实际上是通过get和set访问器实现的，这些访问器可以是公共的、私有的、受保护的等。
   - 属性可以在get和set访问器中添加逻辑，例如验证、事件触发或计算属性值，这使得数据的访问和修改更加可控。
   - 属性可以只包含get访问器（只读属性）或只包含set访问器（只写属性）。

**示例**：

```csharp
// 字段示例
public class Person
{
    private int age;
    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    // 直接字段访问
    public string Name;
}

// 使用属性的示例
public class Person
{
    private int age;
    public int Age
    {
        get { return age; }
        set
        {
            if (value >= 0 && value <= 150)
            {
                age = value;
            }
            else
            {
                throw new ArgumentException("Age must be between 0 and 150.");
            }
        }
    }
}
```

在上面的示例中，`Name`是一个公共字段，可以直接访问和修改，而`Age`是一个属性，它在set访问器中包含了一些验证逻辑，确保年龄值在合理的范围内。