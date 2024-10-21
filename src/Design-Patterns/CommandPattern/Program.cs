namespace Command_Pattern
{
    internal class Program
    {
        public interface ICommand
        {
            void Execute();
        }

        public class LightOnCommand : ICommand
        {
            private readonly Light _light;

            public LightOnCommand(Light light)
            {
                _light = light;
            }

            public void Execute()
            {
                _light.TurnOn();
            }
        }

        public class LightOffCommand : ICommand
        {
            private readonly Light _light;

            public LightOffCommand(Light light)
            {
                _light = light;
            }

            public void Execute()
            {
                _light.TurnOff();
            }
        }

        public class Light
        {
            public void TurnOn()
            {
                Console.WriteLine("Light is on");
            }

            public void TurnOff()
            {
                Console.WriteLine("Light is off");
            }
        }

        public class RemoteControl
        {
            private ICommand _command;

            public void SetCommand(ICommand command)
            {
                _command = command;
            }

            public void PressButton()
            {
                _command.Execute();
            }
        }


        static void Main(string[] args)
        {
            // 创建接收者
            Light light = new Light();

            // 创建命令
            ICommand onCommand = new LightOnCommand(light);
            ICommand offCommand = new LightOffCommand(light);

            // 创建请求者
            RemoteControl remote = new RemoteControl();

            // 设置并执行命令
            remote.SetCommand(onCommand);
            remote.PressButton();  // 输出: Light is on

            remote.SetCommand(offCommand);
            remote.PressButton();  // 输出: Light is off
        }
    }
}
