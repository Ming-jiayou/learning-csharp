using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Factory_Method_Pattern
{
    public class ConcreteProductA : IProduct
    {
        public string GetName()
        {
            return "Product A";
        }
    }
}