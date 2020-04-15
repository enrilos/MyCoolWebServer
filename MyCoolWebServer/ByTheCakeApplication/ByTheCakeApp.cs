namespace MyCoolWebServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Handlers;
    using Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/", new GetHandler(req => new HomeController().Index()));

            appRouteConfig
                .AddRoute("/about", new GetHandler(req => new HomeController().About()));

            // An annoying bug occured here.
            // I was adding the form data in the QueryParameters dict (in the HttpRequest) not the FormData one
            // which obviously returned KeyNotFoundException when using req.FormData["name"].
            // That was my mistake. Fixed it. Now adding in the correct FormData dictionary (HttpRequest/ParseFormData).
            appRouteConfig
                .AddRoute("/add", new PostHandler(req => new CakesController().Add(req.FormData["name"], req.FormData["price"])));

            appRouteConfig
                .AddRoute("/add", new GetHandler(req => new CakesController().Add()));
        }
    }
}
