namespace Mediator_Pattern
{
    public class User
    {
        public string Name { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public User(string name, ChatRoom chatRoom)
        {
            Name = name;
            ChatRoom = chatRoom;
        }

        // 发送消息
        public void SendMessage(string message)
        {
            ChatRoom.Broadcast(this, message);
        }

        // 接收消息
        public void ReceiveMessage(string fromUserName, string message)
        {
            Console.WriteLine($"[{fromUserName}] -> [{Name}]: {message}");
        }
    }

    public class ChatRoom
    {
        private List<User> _users = new List<User>();

        // 添加用户到聊天室
        public void Join(User user)
        {
            _users.Add(user);
        }

        // 广播消息给所有用户
        public void Broadcast(User fromUser, string message)
        {
            foreach (var user in _users)
            {
                if (user != fromUser) // 排除自己
                {
                    user.ReceiveMessage(fromUser.Name, message);
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建聊天室
            var chatRoom = new ChatRoom();

            // 创建用户并加入聊天室
            var user1 = new User("Alice", chatRoom);
            var user2 = new User("Bob", chatRoom);
            var user3 = new User("Charlie", chatRoom);

            chatRoom.Join(user1);
            chatRoom.Join(user2);
            chatRoom.Join(user3);

            // 用户发送消息
            user1.SendMessage("Hello, everyone!");
            user2.SendMessage("Hi, Alice!");
            user3.SendMessage("Hey, what's up?");
        }
    }
}
