namespace Facade_Pattern
{
    public class Projector
    {
        public void On() => Console.WriteLine("放映机开启");
        public void Off() => Console.WriteLine("放映机关闭");
        public void Focus() => Console.WriteLine("放映机聚焦");
    }

    public class Amplifier
    {
        public void On() => Console.WriteLine("音响系统开启");
        public void Off() => Console.WriteLine("音响系统关闭");
        public void SetVolume(int volume) => Console.WriteLine($"音响系统音量设为 {volume}");
    }

    public class DvdPlayer
    {
        public void On() => Console.WriteLine("DVD 播放器开启");
        public void Off() => Console.WriteLine("DVD 播放器关闭");
        public void Play(string movie) => Console.WriteLine($"正在播放电影：{movie}");
    }

    public class Screen
    {
        public void Up() => Console.WriteLine("屏幕升起");
        public void Down() => Console.WriteLine("屏幕降下");
    }

    public class TheaterLights
    {
        public void Dim() => Console.WriteLine("灯光调暗");
        public void Bright() => Console.WriteLine("灯光调亮");
    }

    public class HomeTheaterFacade
    {
        private readonly Projector _projector;
        private readonly Amplifier _amplifier;
        private readonly DvdPlayer _dvdPlayer;
        private readonly Screen _screen;
        private readonly TheaterLights _lights;

        public HomeTheaterFacade(
            Projector projector,
            Amplifier amplifier,
            DvdPlayer dvdPlayer,
            Screen screen,
            TheaterLights lights)
        {
            _projector = projector;
            _amplifier = amplifier;
            _dvdPlayer = dvdPlayer;
            _screen = screen;
            _lights = lights;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine("准备播放电影...");
            _lights.Dim();
            _screen.Down();
            _projector.On();
            _projector.Focus();
            _amplifier.On();
            _amplifier.SetVolume(5);
            _dvdPlayer.On();
            _dvdPlayer.Play(movie);
        }

        public void EndMovie()
        {
            Console.WriteLine("结束播放电影...");
            _lights.Bright();
            _screen.Up();
            _projector.Off();
            _amplifier.Off();
            _dvdPlayer.Off();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var projector = new Projector();
            var amplifier = new Amplifier();
            var dvdPlayer = new DvdPlayer();
            var screen = new Screen();
            var lights = new TheaterLights();

            var homeTheater = new HomeTheaterFacade(projector, amplifier, dvdPlayer, screen, lights);

            homeTheater.WatchMovie("复仇者联盟");
            Console.WriteLine();
            homeTheater.EndMovie();
        }
    }
}
