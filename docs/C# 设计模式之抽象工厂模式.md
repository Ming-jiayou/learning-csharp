# 设计模式之抽象工厂模式

抽象工厂模式是一种创建型设计模式，它提供了一种方式来封装一组具有共同主题的单独工厂。抽象工厂模式通过提供一个超级工厂创建其他工厂的接口，使得客户端使用抽象的接口操作具体对象，从而实现客户端与具体工厂类解耦。

## 使用C++实现抽象工厂模式（Abstract Factory）

下面是一个使用C++实现抽象工厂模式的例子：

```c++
#include <iostream>
#include <string>

// 抽象产品A
class AbstractProductA {
public:
    virtual ~AbstractProductA() {}
    virtual std::string use() = 0;
};

// 抽象产品B
class AbstractProductB {
public:
    virtual ~AbstractProductB() {}
    virtual std::string eat() = 0;
};

// 具体产品A1
class ProductA1 : public AbstractProductA {
public:
    std::string use() override {
        return "Using Product A1";
    }
};

// 具体产品A2
class ProductA2 : public AbstractProductA {
public:
    std::string use() override {
        return "Using Product A2";
    }
};

// 具体产品B1
class ProductB1 : public AbstractProductB {
public:
    std::string eat() override {
        return "Eating Product B1";
    }
};

// 具体产品B2
class ProductB2 : public AbstractProductB {
public:
    std::string eat() override {
        return "Eating Product B2";
    }
};

// 抽象工厂
class AbstractFactory {
public:
    virtual ~AbstractFactory() {}
    virtual AbstractProductA* createProductA() = 0;
    virtual AbstractProductB* createProductB() = 0;
};

// 具体工厂1
class Factory1 : public AbstractFactory {
public:
    AbstractProductA* createProductA() override {
        return new ProductA1();
    }
    AbstractProductB* createProductB() override {
        return new ProductB1();
    }
};

// 具体工厂2
class Factory2 : public AbstractFactory {
public:
    AbstractProductA* createProductA() override {
        return new ProductA2();
    }
    AbstractProductB* createProductB() override {
        return new ProductB2();
    }
};

// 客户端代码
void clientCode(AbstractFactory* factory) {
    AbstractProductA* productA = factory->createProductA();
    AbstractProductB* productB = factory->createProductB();

    std::cout << productA->use() << std::endl;
    std::cout << productB->eat() << std::endl;

    delete productA;
    delete productB;
}

int main() {
    AbstractFactory* factory1 = new Factory1();
    AbstractFactory* factory2 = new Factory2();

    std::cout << "Using the products created by Factory 1:" << std::endl;
    clientCode(factory1);

    std::cout << "Using the products created by Factory 2:" << std::endl;
    clientCode(factory2);

    delete factory1;
    delete factory2;

    return 0;
}
```

运行结果如下所示：

![image-20241012095327960](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241012095327960.png)

在这个例子中，我们定义了两个抽象产品类`AbstractProductA`和`AbstractProductB`，以及两个具体产品类`ProductA1`和`ProductA2`、`ProductB1`和`ProductB2`。我们还定义了一个抽象工厂类`AbstractFactory`，以及两个具体工厂类`Factory1`和`Factory2`。

客户端代码使用抽象工厂接口来创建具体产品，并使用这些产品。通过使用抽象工厂模式，我们可以在不改变客户端代码的情况下，改变具体工厂类和具体产品类。

## 使用C#实现抽象工厂模式（Abstract Factory）

```csharp
namespace Abstract_Factory_Pattern
{
    internal class Program
    {
        public interface ILaptop
        {
            void ShowSpecs();
        }

        public interface IDesktop
        {
            void ShowSpecs();
        }

        // Apple 品牌的产品
        public class AppleLaptop : ILaptop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Apple Laptop Specifications");
            }
        }

        public class AppleDesktop : IDesktop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Apple Desktop Specifications");
            }
        }

        // Lenovo 品牌的产品
        public class LenovoLaptop : ILaptop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Lenovo Laptop Specifications");
            }
        }

        public class LenovoDesktop : IDesktop
        {
            public void ShowSpecs()
            {
                Console.WriteLine("Lenovo Desktop Specifications");
            }
        }

        public interface IComputerFactory
        {
            ILaptop CreateLaptop();
            IDesktop CreateDesktop();
        }

        public class AppleFactory : IComputerFactory
        {
            public ILaptop CreateLaptop()
            {
                return new AppleLaptop();
            }

            public IDesktop CreateDesktop()
            {
                return new AppleDesktop();
            }
        }

        public class LenovoFactory : IComputerFactory
        {
            public ILaptop CreateLaptop()
            {
                return new LenovoLaptop();
            }

            public IDesktop CreateDesktop()
            {
                return new LenovoDesktop();
            }
        }

        static void Main(string[] args)
        {

            // 创建工厂
            IComputerFactory appleFactory = new AppleFactory();
            IComputerFactory lenovoFactory = new LenovoFactory();

            // 创建 Apple 产品的笔记本和台式机
            ILaptop appleLaptop = appleFactory.CreateLaptop();
            IDesktop appleDesktop = appleFactory.CreateDesktop();
            appleLaptop.ShowSpecs();
            appleDesktop.ShowSpecs();

            // 创建 Lenovo 产品的笔记本和台式机
            ILaptop lenovoLaptop = lenovoFactory.CreateLaptop();
            IDesktop lenovoDesktop = lenovoFactory.CreateDesktop();
            lenovoLaptop.ShowSpecs();
            lenovoDesktop.ShowSpecs();
        }
    }
}
```

运行结果如下所示：

![image-20241012151046022](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241012151046022.png)

## 使用Java实现抽象工厂模式

```java
package src.Design_Patterns;

public class Abstract_Factory_Pattern 
{
    public interface Laptop 
    {
        void showSpecs();
    }
    
    public interface Desktop 
    {
        void showSpecs();
    }

    // Apple 品牌的产品
    public class AppleLaptop implements Laptop {
        @Override
        public void showSpecs() {
            System.out.println("Apple Laptop Specifications");
        }
    }

    public class AppleDesktop implements Desktop {
        @Override
        public void showSpecs() {
            System.out.println("Apple Desktop Specifications");
        }
    }

    // Lenovo 品牌的产品
    public class LenovoLaptop implements Laptop {
        @Override
        public void showSpecs() {
            System.out.println("Lenovo Laptop Specifications");
        }
    }

    public class LenovoDesktop implements Desktop {
        @Override
        public void showSpecs() {
            System.out.println("Lenovo Desktop Specifications");
        }
    }

    public interface ComputerFactory {
        Laptop createLaptop();
        Desktop createDesktop();
    }

    public class AppleFactory implements ComputerFactory {
        @Override
        public Laptop createLaptop() {
            return new AppleLaptop();
        }
    
        @Override
        public Desktop createDesktop() {
            return new AppleDesktop();
        }
    }
    
    public class LenovoFactory implements ComputerFactory {
        @Override
        public Laptop createLaptop() {
            return new LenovoLaptop();
        }
    
        @Override
        public Desktop createDesktop() {
            return new LenovoDesktop();
        }
    }

    public  void test()
    {
        // 创建工厂
        ComputerFactory appleFactory = new AppleFactory();
        ComputerFactory lenovoFactory = new LenovoFactory();

        // 创建 Apple 产品的笔记本和台式机
        Laptop appleLaptop = appleFactory.createLaptop();
        Desktop appleDesktop = appleFactory.createDesktop();
        appleLaptop.showSpecs();
        appleDesktop.showSpecs();

        // 创建 Lenovo 产品的笔记本和台式机
        Laptop lenovoLaptop = lenovoFactory.createLaptop();
        Desktop lenovoDesktop = lenovoFactory.createDesktop();
        lenovoLaptop.showSpecs();
        lenovoDesktop.showSpecs();
    }

    public static void main(String[] args) {
        Abstract_Factory_Pattern afp = new Abstract_Factory_Pattern();
        afp.test();
    }

}
```

运行结果如下所示：

![image-20241012152602182](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20241012152602182.png)