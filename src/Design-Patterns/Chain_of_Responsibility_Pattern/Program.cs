namespace Chain_of_Responsibility_Pattern
{
    internal class Program
    {
        public class Request
        {
            public string RequestType { get; set; }
            public string RequestDescription { get; set; }

            public Request(string type, string description)
            {
                RequestType = type;
                RequestDescription = description;
            }
        }

        public abstract class Handler
        {
            protected Handler? _nextHandler;

            public void SetNextHandler(Handler nextHandler)
            {
                _nextHandler = nextHandler;
            }

            public abstract void HandleRequest(Request request);
        }

        public class TechnicalSupportHandler : Handler
        {
            public override void HandleRequest(Request request)
            {
                if (request.RequestType == "Technical")
                {
                    Console.WriteLine("Technical Support: Handling request - " + request.RequestDescription);
                }
                else if (_nextHandler != null)
                {
                    _nextHandler.HandleRequest(request);
                }
            }
        }

        public class AccountSupportHandler : Handler
        {
            public override void HandleRequest(Request request)
            {
                if (request.RequestType == "Account")
                {
                    Console.WriteLine("Account Support: Handling request - " + request.RequestDescription);
                }
                else if (_nextHandler != null)
                {
                    _nextHandler.HandleRequest(request);
                }
            }
        }

        public class ComplaintSupportHandler : Handler
        {
            public override void HandleRequest(Request request)
            {
                if (request.RequestType == "Complaint")
                {
                    Console.WriteLine("Complaint Support: Handling request - " + request.RequestDescription);
                }
                else if (_nextHandler != null)
                {
                    _nextHandler.HandleRequest(request);
                }
            }
        }

        static void Main(string[] args)
        {
            // 创建处理者对象
            Handler technicalSupport = new TechnicalSupportHandler();
            Handler accountSupport = new AccountSupportHandler();
            Handler complaintSupport = new ComplaintSupportHandler();

            // 构建责任链
            technicalSupport.SetNextHandler(accountSupport);
            accountSupport.SetNextHandler(complaintSupport);

            // 创建请求
            Request request1 = new Request("Technical", "My computer won't start.");
            Request request2 = new Request("Account", "I can't log in to my account.");
            Request request3 = new Request("Complaint", "The service is very slow.");

            // 处理请求
            technicalSupport.HandleRequest(request1);
            technicalSupport.HandleRequest(request2);
            technicalSupport.HandleRequest(request3);

            Console.ReadLine();       
        }
    }
}
