namespace Observer_Pattern
{
    /// <summary>
    /// 观察者接口
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="message">通知消息</param>
        void Update(string message);
    }

    /// <summary>
    /// 主题接口
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// 注册观察者
        /// </summary>
        /// <param name="observer">观察者实例</param>
        void RegisterObserver(IObserver observer);

        /// <summary>
        /// 移除观察者
        /// </summary>
        /// <param name="observer">观察者实例</param>
        void RemoveObserver(IObserver observer);

        /// <summary>
        /// 通知所有观察者
        /// </summary>
        /// <param name="message">通知消息</param>
        void NotifyObservers(string message);
    }

    /// <summary>
    /// 天气主题
    /// </summary>
    public class WeatherSubject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        // 模拟天气变化
        public void SetMeasurements(string weatherInfo)
        {
            NotifyObservers(weatherInfo);
        }
    }

    /// <summary>
    /// 天气预报观察者
    /// </summary>
    public class WeatherForecastObserver : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine("天气预报：收到更新 - " + message);
        }
    }

    /// <summary>
    /// 早报观察者
    /// </summary>
    public class MorningNewsObserver : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine("早报：收到更新 - " + message);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 创建主题
            var weatherSubject = new WeatherSubject();

            // 创建观察者
            var forecastObserver = new WeatherForecastObserver();
            var morningNewsObserver = new MorningNewsObserver();

            // 注册观察者
            weatherSubject.RegisterObserver(forecastObserver);
            weatherSubject.RegisterObserver(morningNewsObserver);

            // 模拟天气变化并通知观察者
            weatherSubject.SetMeasurements("明天：晴天，高温25℃，低温18℃");
            weatherSubject.SetMeasurements("后天：多云，高温22℃，低温15℃");

            // 移除观察者
            weatherSubject.RemoveObserver(morningNewsObserver);

            // 再次模拟天气变化并通知观察者
            weatherSubject.SetMeasurements("大后天：雨天，高温20℃，低温12℃");
        }
    }
}
