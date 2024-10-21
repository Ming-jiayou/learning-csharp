namespace Bridge_Pattern
{
    internal class Program
    {     
    // 抽象类
    public abstract class Abstraction
    {
        protected IImplementor implementor;

        public Abstraction(IImplementor implementor)
        {
            this.implementor = implementor;
        }

        public virtual void Operation()
        {
            implementor.OperationImpl();
        }
    }

    // 具体抽象类
    public class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(IImplementor implementor) : base(implementor)
        {
        }

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("RefinedAbstraction Operation");
        }
    }

    // 实现接口
    public interface IImplementor
    {
        void OperationImpl();
    }

    // 具体实现类A
    public class ConcreteImplementorA : IImplementor
    {
        public void OperationImpl()
        {
            Console.WriteLine("ConcreteImplementorA Operation");
        }
    }

    // 具体实现类B
    public class ConcreteImplementorB : IImplementor
    {
        public void OperationImpl()
        {
            Console.WriteLine("ConcreteImplementorB Operation");
        }
    }

    // 在Main方法中使用桥接模式
    static void Main(string[] args)
    {
        Abstraction abstractionA = new RefinedAbstraction(new ConcreteImplementorA());
        abstractionA.Operation();

        Abstraction abstractionB = new RefinedAbstraction(new ConcreteImplementorB());
        abstractionB.Operation();
    }
    }
}
