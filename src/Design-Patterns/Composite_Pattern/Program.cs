namespace Composite_Pattern
{
    // 组合模式中的组件（Component）
    public interface IFileSystemComponent
    {
        string GetName();
        void Operation(); // 示例操作：打印名称
    }

    // 叶子节点（Leaf）：文件
    public class File : IFileSystemComponent
    {
        public string Name { get; set; }

        public File(string name)
        {
            Name = name;
        }

        public string GetName() => Name;

        public void Operation()
        {
            Console.WriteLine($"File: {Name}");
        }
    }

    // 容器节点（Composite）：文件夹
    public class Directory : IFileSystemComponent
    {
        public string Name { get; set; }
        private List<IFileSystemComponent> _components = new List<IFileSystemComponent>();

        public Directory(string name)
        {
            Name = name;
        }

        public string GetName() => Name;

        public void Add(IFileSystemComponent component)
        {
            _components.Add(component);
        }

        public void Remove(IFileSystemComponent component)
        {
            _components.Remove(component);
        }

        public void Operation()
        {
            Console.WriteLine($"Directory: {Name}");
            foreach (var component in _components)
            {
                component.Operation(); // 递归操作子组件
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建文件系统结构
            var root = new Directory("Root");
            var docs = new Directory("Documents");
            var pics = new Directory("Pictures");

            var file1 = new File("example.txt");
            var file2 = new File("image.jpg");

            root.Add(docs);
            root.Add(pics);
            docs.Add(file1);
            pics.Add(file2);

            // 统一操作
            root.Operation();
        }
    }
}
