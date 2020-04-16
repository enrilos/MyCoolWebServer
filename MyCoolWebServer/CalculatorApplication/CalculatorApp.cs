namespace MyCoolWebServer.CalculatorApplication
{
    using Controllers;
    using Server.Handlers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class CalculatorApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/", new GetHandler(req => new CalculatorController().Index()));

            appRouteConfig
                .AddRoute("/", new PostHandler(
                    req => new CalculatorController().Index(req.FormData["operandOne"], req.FormData["operator"], req.FormData["operandTwo"])));

            appRouteConfig
                .AddRoute("/login", new GetHandler(req => new LoginController().Index()));

            appRouteConfig
                .AddRoute("/login", new PostHandler(req => new LoginController().Index(req.FormData["username"], req.FormData["passwd"])));
        }
    }
}
