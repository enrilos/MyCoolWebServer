namespace MyCoolWebServer.Application
{
    using Controllers;
    using Server.Contracts;
    using Server.Handlers;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/", new GetHandler(request => new HomeController().Index()));

            appRouteConfig
                .AddRoute("/index", new GetHandler(request => new HomeController().Index()));

            appRouteConfig
                .AddRoute("/sessionTest", new GetHandler(request => new HomeController().SessionTest(request)));

            appRouteConfig
                .AddRoute("/add", new GetHandler(request => new AddCakeController().Add()));

            appRouteConfig
                .AddRoute("/search", new GetHandler(request => new BrowseCakesController().Browse()));
        }
    }
}
