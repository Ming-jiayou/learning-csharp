# C# 方法重载于方法重写的区别

在C#中，方法重载（Overloading）和方法重写（Overriding）是实现多态的两种不同方式，它们有着本质的区别：

1. **方法重载（Overloading）**：
   方法重载是在同一个类中定义多个具有相同名称但参数列表不同的方法。这里的参数列表不同可以是参数的数量不同，或者参数的类型不同，甚至参数的顺序不同。C#编译器会根据调用方法时传入的参数类型和数量自动选择合适的方法版本。重载不改变方法的访问级别，也不需要使用新的关键字，它主要是为了提供更清晰、更灵活的接口设计，使得一个方法名可以对应多种不同的功能实现。

   **示例**：

   ```csharp
   public class MyClass {
       public void MyMethod() {
           // 无参数的方法实现
       }
       public void MyMethod(int a) {
           // 带有int参数的方法实现
       }
       public void MyMethod(string s) {
           // 带有string参数的方法实现
       }
   }
   ```

2. **方法重写（Overriding）**：
   方法重写发生在继承关系中，子类可以重写（或覆盖）从基类继承的方法。重写的方法需要与基类中的方法具有完全相同的名称、参数列表和返回类型，但是可以改变方法的实现细节，甚至方法的访问级别（但不能更严格）。重写的关键字是`override`，它表明当前的方法是重写基类中的方法。重写的主要目的是为了让子类可以提供自己的实现，以适应更具体或不同的行为需求。

   **示例**：

   ```csharp
   public class MyBaseClass {
       public virtual void MyMethod() {
           Console.WriteLine("Base Class Method");
       }
   }
   public class MyDerivedClass : MyBaseClass {
       public override void MyMethod() {
           Console.WriteLine("Derived Class Method");
       }
   }
   ```

   在这个例子中，`MyDerivedClass`重写了从`MyBaseClass`继承的`MyMethod`方法，当`MyDerivedClass`的实例调用`MyMethod`时，将执行`MyDerivedClass`中重写的方法，而不是基类中的方法。

总结来说，方法重载是在同一个类中提供多个方法版本，而方法重写是在继承关系中子类对基类方法的重新实现。两者都是多态的重要体现，但应用场景和实现机制不同。