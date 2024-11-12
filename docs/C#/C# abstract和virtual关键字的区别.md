# C# abstract和virtual关键字的区别

在C#中，`abstract`和`virtual`关键字都用于实现多态性，但它们之间有一些关键的区别：

1. **抽象方法（abstract）**:
   - 抽象方法在基类中声明，但不提供实现。这意味着在基类中，你只能定义方法的签名，而不能定义其主体。
   - 抽象方法必须在派生类中重写。如果不重写，则派生类也必须声明为抽象的。
   - 抽象方法不能在抽象类之外声明。也就是说，抽象方法只能存在于抽象类中。
   - 抽象方法不能使用访问修饰符`private`、`static`、`sealed`或者`override`。
2. **虚方法（virtual）**:
   - 虚方法在基类中声明并提供一个默认实现。这意味着即使派生类没有重写这个方法，它仍然可以被调用。
   - 虚方法可以在派生类中被重写，但不强制要求。如果派生类想要提供不同的实现，可以使用`override`关键字。
   - 虚方法可以被标记为`sealed`，这意味着它不能再被派生类重写。
   - 虚方法可以使用访问修饰符`private`、`protected`、`internal`、`protected internal`或`public`。

总结：

- `abstract`方法没有实现，必须在派生类中重写，只能在抽象类中声明。
- `virtual`方法有默认实现，可以被重写但不是必须的，可以在任何类中声明。

示例：

```csharp
// 抽象类和抽象方法
public abstract class Animal
{
    public abstract void MakeSound();
}

// 派生类实现抽象方法
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }
}

// 虚方法
public class Vehicle
{
    public virtual void Drive()
    {
        Console.WriteLine("Driving...");
    }
}

// 重写虚方法
public class Car : Vehicle
{
    public override void Drive()
    {
        Console.WriteLine("Car is driving...");
    }
}
```