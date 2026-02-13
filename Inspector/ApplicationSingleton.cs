using Renga;

namespace Inspector
{
    internal class ApplicationSingleton
    {
        private static ApplicationSingleton? _instance;
        private readonly IApplication _application;

        private ApplicationSingleton()
        {
            _application = new Application();
        }

        public static IApplication GetApp()
        {
            _instance ??= new ApplicationSingleton();
            return _instance._application;
        }
    }
}
