# C# 中的静态构造函数和实例构造函数的区别

在C#中，静态构造函数和实例构造函数在类的初始化过程中扮演着不同的角色。下面我将详细介绍这两种构造函数的区别：

1. **实例构造函数**（Instance Constructor）：

   - 实例构造函数用于初始化类的实例（对象）。当创建类的一个新实例时，实例构造函数会被调用。
   - 每个类至少有一个实例构造函数。如果没有显式定义，C#编译器会自动提供一个默认的无参构造函数。
   - 实例构造函数可以是公共的、私有的、受保护的、内部的或受保护内部的，这取决于你希望如何控制对象的创建。
   - 它可以有参数，用于初始化类的实例变量。

   **示例**：

   ```csharp
   public class MyClass
   {
       public string Name { get; set; }
   
       public MyClass(string name)
       {
           Name = name;
       }
   }
   ```

2. **静态构造函数**（Static Constructor）：

   - 静态构造函数用于初始化类的静态成员。它在类的任何实例被创建之前执行，且在整个应用程序域中仅执行一次。
   - 静态构造函数没有访问修饰符，没有参数，不能被标记为 public、protected、private 等，也不能被重载。
   - 它主要用于初始化静态字段，如数据库连接字符串、日志配置等。
   - 如果类包含静态成员，且这些成员需要初始化逻辑，那么静态构造函数是理想的选择。

   **示例**：

   ```csharp
   public class MyClass
   {
       public static string ConnectionString { get; private set; }
   
       static MyClass()
       {
           ConnectionString = "Data Source=server;Initial Catalog=database;User ID=user;Password=password";
       }
   }
   ```

总结：

- 实例构造函数用于初始化类的实例，而静态构造函数用于初始化类的静态成员。
- 静态构造函数在类的任何实例创建之前执行，而实例构造函数在创建类的每个实例时执行。
- 静态构造函数没有访问修饰符，不能有参数，而实例构造函数可以有访问修饰符和参数。