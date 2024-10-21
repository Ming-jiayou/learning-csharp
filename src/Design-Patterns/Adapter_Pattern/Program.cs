namespace Adapter_Pattern
{

    /// <summary>
    /// 音频播放器接口（支持 MP3）
    /// </summary>
    public interface IAudioPlayer
    {
        void PlayMP3(string fileName);
    }

    /// <summary>
    /// VLC 播放器接口（支持 VLC 专有音频格式）
    /// </summary>
    public interface IVLCPlayer
    {
        void PlayVLC(string fileName);
    }

    /// <summary>
    /// VLC 播放器实现
    /// </summary>
    public class VLCPlayer : IVLCPlayer
    {
        public void PlayVLC(string fileName)
        {
            Console.WriteLine($"VLC 播放器：播放 {fileName}（VLC 专有音频格式）");
        }
    }

    /// <summary>
    /// VLC 适配器
    /// </summary>
    public class VLCAdapter : IAudioPlayer
    {
        private readonly IVLCPlayer _vlcPlayer;

        public VLCAdapter(IVLCPlayer vlcPlayer)
        {
            _vlcPlayer = vlcPlayer;
        }

        public void PlayMP3(string fileName)
        {
            // 适配逻辑：将 MP3 请求转换为 VLC 请求
            Console.WriteLine("VLC 适配器：转换 MP3 请求为 VLC 请求...");
            string vlcFileName = ConvertToVLCFileName(fileName); // 假设有转换方法
            _vlcPlayer.PlayVLC(vlcFileName);
            Console.WriteLine("VLC 适配器：请求转换完成。");
        }

        // 假设的文件名转换方法
        private string ConvertToVLCFileName(string mp3FileName)
        {
            return mp3FileName.Replace(".mp3", ".vlc");
        }
    }

    /// <summary>
    /// 音频播放器应用程序
    /// </summary>
    public class AudioPlayerApp
    {
        public void PlayAudio(IAudioPlayer audioPlayer, string fileName)
        {
            Console.WriteLine("音频播放器应用程序：播放音频...");
            audioPlayer.PlayMP3(fileName);
            Console.WriteLine("音频播放器应用程序：播放完成。\n");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {       
           // 创建 VLC 播放器
            IVLCPlayer vlcPlayer = new VLCPlayer();

            // 创建 VLC 适配器
            IAudioPlayer vlcAdapter = new VLCAdapter(vlcPlayer);

            // 创建音频播放器应用程序
            AudioPlayerApp audioApp = new AudioPlayerApp();

            // 播放 VLC 音频文件（通过适配器）
            audioApp.PlayAudio(vlcAdapter, "example.mp3");
        }
    }
 
}
