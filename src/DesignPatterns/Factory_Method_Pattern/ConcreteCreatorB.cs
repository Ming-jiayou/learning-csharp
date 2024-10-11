using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Factory_Method_Pattern
{
    public class ConcreteCreatorB : Creator
    {
         public override IProduct FactoryMethod()
         {
            return new ConcreteProductB();
         }
    }
}