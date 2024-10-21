namespace Builder_Pattern
{

    public class Computer
    {
        public string CPU { get; set; }
        public int RAM { get; set; } // GB
        public int Storage { get; set; } // GB
        public string GPU { get; set; }
    
        // 覆盖ToString方法，方便输出电脑配置
        public override string ToString()
        {
            return $"CPU: {CPU}, RAM: {RAM}GB, Storage: {Storage}GB, GPU: {GPU}";
        }
    }

    public abstract class ComputerBuilder
    {
        protected Computer Computer;

        public ComputerBuilder()
        {
            Computer = new Computer();
        }

        public abstract ComputerBuilder SetCPU(string cpu);
        public abstract ComputerBuilder SetRAM(int ram);
        public abstract ComputerBuilder SetStorage(int storage);
        public abstract ComputerBuilder SetGPU(string gpu);

        // 获取构建好的电脑
        public Computer Build()
        {
            return Computer;
        }
    }

    // 游戏电脑建造者
    public class GamingComputerBuilder : ComputerBuilder
    {
        public GamingComputerBuilder()
        {
            // 默认配置
            SetCPU("AMD Ryzen 9 5900X");
            SetRAM(64);
            SetStorage(2048);
            SetGPU("NVIDIA GeForce RTX 3080");
        }

        public override ComputerBuilder SetCPU(string cpu)
        {
            Computer.CPU = cpu;
            return this;
        }

        public override ComputerBuilder SetRAM(int ram)
        {
            Computer.RAM = ram;
            return this;
        }

        public override ComputerBuilder SetStorage(int storage)
        {
            Computer.Storage = storage;
            return this;
        }

        public override ComputerBuilder SetGPU(string gpu)
        {
            Computer.GPU = gpu;
            return this;
        }
    }

    // 办公电脑建造者
    public class OfficeComputerBuilder : ComputerBuilder
    {
        public OfficeComputerBuilder()
        {
            // 默认配置
            SetCPU("Intel Core i5-11400");
            SetRAM(16);
            SetStorage(512);
            SetGPU("Integrated Graphics");
        }

        public override ComputerBuilder SetCPU(string cpu)
        {
            Computer.CPU = cpu;
            return this;
        }

        public override ComputerBuilder SetRAM(int ram)
        {
            Computer.RAM = ram;
            return this;
        }

        public override ComputerBuilder SetStorage(int storage)
        {
            Computer.Storage = storage;
            return this;
        }

        public override ComputerBuilder SetGPU(string gpu)
        {
            Computer.GPU = gpu;
            return this;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
           // 构建游戏电脑，但自定义RAM和GPU
            var gamingComputer = new GamingComputerBuilder()
                .SetRAM(128)
                .SetGPU("NVIDIA GeForce RTX 3090")
                .Build();

            Console.WriteLine("游戏电脑配置:");
            Console.WriteLine(gamingComputer.ToString());

            // 构建办公电脑，使用默认配置
            var officeComputer = new OfficeComputerBuilder()
                .Build();

            Console.WriteLine("\n办公电脑配置:");
            Console.WriteLine(officeComputer.ToString());
        }
    }

   
}    

