# C++/C#设计模式之工厂方法模式

### 工厂方法模式简介

工厂方法模式（Factory Method Pattern）是一种创建型设计模式，它定义了一个用于创建对象的接口，但让子类决定实例化哪一个类。工厂方法使一个类的实例化延迟到其子类。

这种模式的核心思想是将对象的创建过程与其使用过程分离，从而提高系统的灵活性和可扩展性。通过工厂方法模式，可以在不改变客户端代码的情况下，引入新的产品类型。

### 工厂方法模式的结构

工厂方法模式通常包含以下几个角色：

1. **Product（产品）**：定义工厂方法所创建的对象的接口。
2. **ConcreteProduct（具体产品）**：实现Product接口的具体类。
3. **Creator（创建者）**：声明工厂方法，该方法返回一个Product类型的对象。Creator也可以定义一个工厂方法的默认实现，返回一个默认的ConcreteProduct对象。
4. **ConcreteCreator（具体创建者）**：重写工厂方法以返回一个ConcreteProduct的实例。

### C#实现工厂方法模式

下面是一个使用C#实现工厂方法模式的示例。

#### 1. 定义产品接口

```
public interface IProduct
{
    string GetName();
}
```

#### 2. 定义具体产品

```
public class ConcreteProductA : IProduct
{
    public string GetName()
    {
        return "Product A";
    }
}

public class ConcreteProductB : IProduct
{
    public string GetName()
    {
        return "Product B";
    }
}
```

#### 3. 定义创建者

```
public abstract class Creator
{
    // 工厂方法
    public abstract IProduct FactoryMethod();

    // 业务逻辑
    public void SomeOperation()
    {
        IProduct product = FactoryMethod();
        Console.WriteLine("Created: " + product.GetName());
    }
}
```

#### 4. 定义具体创建者

```
public class ConcreteCreatorA : Creator
{
    public override IProduct FactoryMethod()
    {
        return new ConcreteProductA();
    }
}

public class ConcreteCreatorB : Creator
{
    public override IProduct FactoryMethod()
    {
        return new ConcreteProductB();
    }
}
```

#### 5. 客户端代码

```
public class Program
{
    public static void Main(string[] args)
    {
        Creator creatorA = new ConcreteCreatorA();
        creatorA.SomeOperation(); // 输出: Created: Product A

        Creator creatorB = new ConcreteCreatorB();
        creatorB.SomeOperation(); // 输出: Created: Product B
    }
}
```

### 运行结果

运行上述代码后，控制台将输出：

```
Created: Product A
Created: Product B
```

如下所示：

![image-20241011082814956](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241011082814956.png)

### C++实现工厂方法模式

```c++
#include <iostream>
#include <memory>

// 产品抽象类
class Product {
public:
    virtual ~Product() = default;
    virtual void use() const = 0;
};

// 具体产品类A
class ConcreteProductA : public Product {
public:
    void use() const override {
        std::cout << "Using ConcreteProductA" << std::endl;
    }
};

// 具体产品类B
class ConcreteProductB : public Product {
public:
    void use() const override {
        std::cout << "Using ConcreteProductB" << std::endl;
    }
};

// 工厂抽象类
class Factory {
public:
    virtual ~Factory() = default;
    virtual std::unique_ptr<Product> createProduct() const = 0;
};

// 具体工厂类A
class ConcreteFactoryA : public Factory {
public:
    std::unique_ptr<Product> createProduct() const override {
        return std::make_unique<ConcreteProductA>();
    }
};

// 具体工厂类B
class ConcreteFactoryB : public Factory {
public:
    std::unique_ptr<Product> createProduct() const override {
        return std::make_unique<ConcreteProductB>();
    }
};

int main() {
    // 使用具体工厂A创建产品A
    std::unique_ptr<Factory> factoryA = std::make_unique<ConcreteFactoryA>();
    std::unique_ptr<Product> productA = factoryA->createProduct();
    productA->use();

    // 使用具体工厂B创建产品B
    std::unique_ptr<Factory> factoryB = std::make_unique<ConcreteFactoryB>();
    std::unique_ptr<Product> productB = factoryB->createProduct();
    productB->use();

    return 0;
}
```

结果如下所示：

![image-20241011093322480](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241011093322480.png)

### 总结

工厂方法模式通过将对象的创建过程封装在具体的创建者类中，使得系统更加灵活，易于扩展。当需要添加新的产品类型时，只需创建新的具体产品和具体创建者，而不需要修改现有的代码。这符合开闭原则（Open/Closed Principle），即对扩展开放，对修改关闭。