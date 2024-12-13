# C#委托与事件

## C# 委托

在C#中，委托（Delegate）是一种引用类型，用于封装方法的引用。它允许你将方法作为参数传递，或者将方法赋值给变量，从而实现方法的传递和调用。委托在C#中扮演着非常重要的角色，尤其是在事件处理、异步编程和多线程编程中。

### 委托的定义

委托的定义类似于方法签名，它包含了返回类型和参数列表，但没有方法体。例如，定义一个无参数返回void类型的委托：

```
public delegate void MyDelegate();
```

或者，定义一个接受一个整型参数并返回void类型的委托：

```
public delegate void MyDelegate(int value);
```

### 委托的实例化

创建委托实例时，需要使用new关键字，并通过委托类型调用构造函数。然后，可以使用+=运算符将方法与委托关联：

```
MyDelegate myDelegate = new MyDelegate(MyMethod);
```

或者，可以使用简化的语法：

```
MyDelegate myDelegate = MyMethod;
```

### 委托的调用

调用委托时，就像调用普通方法一样，只是使用的是委托实例。例如：

```
myDelegate();
```

### 多播委托

委托可以被设计为多播的，这意味着一个委托可以调用多个方法。当调用多播委托时，所有关联的方法都会按顺序执行。这可以通过多次使用+=运算符来实现：

```
myDelegate += AnotherMethod;
```

### 委托的异步调用

C#中的委托还支持异步调用，这可以通过BeginInvoke和EndInvoke方法实现。异步调用允许在不阻塞当前线程的情况下执行委托。

### 委托与匿名方法、Lambda表达式

C#还允许使用匿名方法和Lambda表达式来定义委托，这使得代码更加简洁和易于理解。例如：

```
MyDelegate myDelegate = delegate(int x) { Console.WriteLine(x); };
```

或使用Lambda表达式：

```
MyDelegate myDelegate = (int x) => Console.WriteLine(x);
```

### 总结

委托在C#中提供了一种灵活的方法引用机制，它允许你将方法作为参数传递，实现方法的延迟调用，以及支持多播和异步调用，是C#中一个非常强大的特性。

## C#事件

在C#中，事件（Event）是一种特殊的委托类型，用于实现发布-订阅模式（Publish-Subscribe Pattern），也称为观察者模式。事件允许一个类（发布者）通知其他类（订阅者）某些事情已经发生，而无需发布者直接与订阅者交互。这提供了高度的解耦和灵活性。

### 事件的基本构成

事件由两部分组成：

1. **事件声明**：在类中声明事件，通常使用关键字`event`和一个委托类型。
2. **事件处理器**：处理事件的代码，通常是一个方法，其签名与事件委托的签名相匹配。

### 使用事件的步骤

#### 1. 定义委托类型

首先，定义一个委托类型，它将作为事件的类型。例如：

```
public delegate void MyEventHandler(object sender, EventArgs e);
```

#### 2. 声明事件

在类中声明事件，使用`event`关键字和前面定义的委托类型：

```
public event MyEventHandler MyEvent;
```

#### 3. 触发事件

在类中，当某个条件满足时，通过调用`+=`和`-=`操作符的隐式调用的`Invoke`方法来触发事件：

```
public void OnMyEvent()
{
    MyEvent?.Invoke(this, EventArgs.Empty);
}
```

#### 4. 订阅事件

在其他类中，可以订阅事件，即向事件添加事件处理器：

```
public void SubscribeToEvent()
{
    PublisherClass publisher = new PublisherClass();
    publisher.MyEvent += OnMyEventReceived;
}

private void OnMyEventReceived(object sender, EventArgs e)
{
    Console.WriteLine("Event received!");
}
```

#### 5. 取消订阅事件

同样在订阅者类中，可以取消订阅事件：

```
public void UnsubscribeFromEvent()
{
    PublisherClass publisher = new PublisherClass();
    publisher.MyEvent -= OnMyEventReceived;
}
```

### 注意事项

- 事件在设计上是线程安全的，但事件处理函数的调用可能不是线程安全的，因此在多线程环境中需要额外注意。
- 在事件处理器中，`sender`参数通常用于获取触发事件的对象的引用，而`e`参数通常用于获取关于事件的附加信息。
- 事件处理器通常应该能够处理可能的`null`引用异常，因为事件可能没有订阅者。

通过使用事件，C#程序可以实现更复杂的交互和解耦的组件设计。

## C# 委托与事件的区别

在C#中，委托（Delegate）是一种引用类型的数据类型，它允许你封装一个方法的引用，从而可以在稍后的某个时间调用这个方法。委托可以看作是一种“可传递的函数”，能够将函数作为参数传递给其他函数，或者将函数赋给变量。委托在C#中用于定义回调方法，是实现事件和多线程的基础。

事件（Event）是基于委托的一种封装，它提供了一种多对一的机制，即多个订阅者可以订阅同一个事件，当事件被触发时，所有订阅者都会被通知。事件是C#中封装和隐藏委托的一种方式，它通常用于组件之间或者类之间的通信。事件的声明通常在类的内部，而事件的订阅和触发则发生在类的外部。

事件和委托的关系可以这样理解：

1. **事件是委托的封装**：事件在内部使用委托来存储和调用方法，但对外部提供了更安全和封装的接口。
2. **事件是多播的**：一个事件可以绑定多个委托，这意味着当事件被触发时，所有绑定的方法都会被调用。
3. **事件的访问限制**：事件通常被声明为public或protected，而其对应的委托类型可以是private的，这样可以控制事件的触发点，但允许外部代码订阅事件。
4. **事件的触发**：事件的触发通常通过类的内部代码完成，而委托可以直接由外部代码调用。

简单来说，委托是C#中实现方法引用和传递的基础，而事件是基于委托的一种高级特性，用于实现对象之间的解耦和通信。