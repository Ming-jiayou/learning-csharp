using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Factory_Method_Pattern
{
    public abstract  class Creator
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
}