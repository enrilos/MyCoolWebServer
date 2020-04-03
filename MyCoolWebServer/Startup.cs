namespace MyCoolWebServer
{
    using Application;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Startup : IRunnable
    {
        private const int port = 1337;

        public static void Main()
        {
            new Startup().Run();
        }

        public void Run()
        {
            var mainApplication = new MainApplication();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);
            var webServer = new WebServer(port, appRouteConfig);
            webServer.Run();
        }
    }
}
