namespace State_Pattern
{
    // 抽象状态类
    public abstract class VendingMachineState
    {
        protected VendingMachine _vendingMachine;

        public VendingMachineState(VendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }

        public abstract void SelectProduct();
        public abstract void MakePayment();
        public abstract void Dispense();
    }

    // 环境类
    public class VendingMachine
    {
        private VendingMachineState _currentState;

        public VendingMachine()
        {
            _currentState = new NoSelectionState(this);
        }

        public void SetState(VendingMachineState state)
        {
            _currentState = state;
        }

        public void SelectProduct()
        {
            _currentState.SelectProduct();
        }

        public void MakePayment()
        {
            _currentState.MakePayment();
        }

        public void Dispense()
        {
            _currentState.Dispense();
        }
    }

    // 具体状态类 - 无选择状态
    public class NoSelectionState : VendingMachineState
    {
        public NoSelectionState(VendingMachine vendingMachine) : base(vendingMachine)
        {
        }

        public override void SelectProduct()
        {
            Console.WriteLine("您已选择产品。");
            _vendingMachine.SetState(new HasSelectionState(_vendingMachine));
        }

        public override void MakePayment()
        {
            Console.WriteLine("请先选择产品。");
        }

        public override void Dispense()
        {
            Console.WriteLine("请先选择产品并支付。");
        }
    }

    // 具体状态类 - 已选择状态
    public class HasSelectionState : VendingMachineState
    {
        public HasSelectionState(VendingMachine vendingMachine) : base(vendingMachine)
        {
        }

        public override void SelectProduct()
        {
            Console.WriteLine("您已经选择了产品，请支付。");
        }

        public override void MakePayment()
        {
            Console.WriteLine("支付成功，准备出货。");
            _vendingMachine.SetState(new SoldState(_vendingMachine));
        }

        public override void Dispense()
        {
            Console.WriteLine("请先支付。");
        }
    }

    // 具体状态类 - 售出状态
    public class SoldState : VendingMachineState
    {
        public SoldState(VendingMachine vendingMachine) : base(vendingMachine)
        {
        }

        public override void SelectProduct()
        {
            Console.WriteLine("请等待当前产品出货后再选择。");
        }

        public override void MakePayment()
        {
            Console.WriteLine("您已经支付过了。");
        }

        public override void Dispense()
        {
            Console.WriteLine("出货成功！");
            _vendingMachine.SetState(new NoSelectionState(_vendingMachine));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();

            vendingMachine.SelectProduct();  // 选择产品
            vendingMachine.MakePayment();    // 支付
            vendingMachine.Dispense();       // 出货

            Console.ReadKey();
        }
    }
}
