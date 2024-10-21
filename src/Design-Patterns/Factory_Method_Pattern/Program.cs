namespace Factory_Method_Pattern
{

    public interface IPaymentGateway
    {
        void Pay(Order order);
    }

    public class WeChatPaymentGateway : IPaymentGateway
    {
        public void Pay(Order order)
        {
            Console.WriteLine($"Processing WeChat payment for order {order.Id}...");
        }
    }

    public class AlipayPaymentGateway : IPaymentGateway
    {
        public void Pay(Order order)
        {
            Console.WriteLine($"Processing Alipay payment for order {order.Id}...");
        }
    }

    public class UnionPayPaymentGateway : IPaymentGateway
    {
        public void Pay(Order order)
        {
            Console.WriteLine($"Processing UnionPay payment for order {order.Id}...");
        }
    }

    public abstract class PaymentGatewayFactory
    {
        public abstract IPaymentGateway CreatePaymentGateway();
    }

    public class WeChatPaymentGatewayFactory : PaymentGatewayFactory
    {
        public override IPaymentGateway CreatePaymentGateway()
        {
            return new WeChatPaymentGateway();
        }
    }

    public class AlipayPaymentGatewayFactory : PaymentGatewayFactory
    {
        public override IPaymentGateway CreatePaymentGateway()
        {
            return new AlipayPaymentGateway();
        }
    }

    public class UnionPayPaymentGatewayFactory : PaymentGatewayFactory
    {
        public override IPaymentGateway CreatePaymentGateway()
        {
            return new UnionPayPaymentGateway();
        }
    }

    public class Order
    {
        public int Id { get; set; }
        // ...
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建订单
            Order order = new Order { Id = 123 };

            // 选择支付网关工厂
            PaymentGatewayFactory factory = GetPaymentGatewayFactory("WeChat"); // 或 "Alipay"、"UnionPay"

            // 创建支付网关实例
            IPaymentGateway paymentGateway = factory.CreatePaymentGateway();

            // 执行支付
            paymentGateway.Pay(order);
        }

        static PaymentGatewayFactory GetPaymentGatewayFactory(string gatewayType)
        {
            switch (gatewayType)
            {
                case "WeChat":
                    return new WeChatPaymentGatewayFactory();
                case "Alipay":
                    return new AlipayPaymentGatewayFactory();
                case "UnionPay":
                    return new UnionPayPaymentGatewayFactory();
                default:
                    throw new ArgumentException("Unsupported payment gateway type.");
            }
        }
    }
}
