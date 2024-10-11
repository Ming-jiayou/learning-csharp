using System;
using DesignPatterns.Factory_Method_Pattern;

namespace DesignPatterns
{
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
}