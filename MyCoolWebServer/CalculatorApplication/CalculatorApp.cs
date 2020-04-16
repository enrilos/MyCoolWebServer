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
        }
    }
}
