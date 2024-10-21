namespace Command_Pattern
{
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    /// <summary>
    /// 具体命令：打开电视
    /// </summary>
    public class OpenTVCommand : ICommand
    {
        private readonly TV _tv;

        public OpenTVCommand(TV tv)
        {
            _tv = tv;
        }

        public void Execute()
        {
            _tv.Open();
        }

        public void Undo()
        {
            _tv.Close();
        }
    }

    /// <summary>
    /// 具体命令：关闭电视
    /// </summary>
    public class CloseTVCommand : ICommand
    {
        private readonly TV _tv;

        public CloseTVCommand(TV tv)
        {
            _tv = tv;
        }

        public void Execute()
        {
            _tv.Close();
        }

        public void Undo()
        {
            _tv.Open();
        }
    }

    /// <summary>
    /// 电视（接收者）
    /// </summary>
    public class TV
    {
        public void Open()
        {
            Console.WriteLine("电视打开了");
        }

        public void Close()
        {
            Console.WriteLine("电视关闭了");
        }
    }

    /// <summary>
    /// 远程控制器（调用者）
    /// </summary>
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

        public void PressUndoButton()
        {
            _command.Undo();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建接收者（电视）
            var tv = new TV();

            // 创建具体命令（打开电视）
            var openCommand = new OpenTVCommand(tv);

            // 创建具体命令（关闭电视）
            var closeCommand = new CloseTVCommand(tv);

            // 创建调用者（远程控制器）
            var remoteControl = new RemoteControl();

            // 设置命令并执行
            remoteControl.SetCommand(openCommand);
            remoteControl.PressButton(); // 电视打开了
            remoteControl.PressUndoButton(); // 电视关闭了

            // 切换命令并执行
            remoteControl.SetCommand(closeCommand);
            remoteControl.PressButton(); // 电视打开了（Undo的效果）
            remoteControl.PressUndoButton(); // 电视关闭了
        }
    }
}
