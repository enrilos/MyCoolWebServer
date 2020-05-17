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
                .AddRoute("/index", new GetHandler(req => new HomeController().Index()));

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

            appRouteConfig
                .AddRoute("/search", new GetHandler(req => new CakesController().Search(req)));

            appRouteConfig
                .AddRoute("/login", new GetHandler(req => new AccountController().Login()));

            appRouteConfig
                .AddRoute("/login", new PostHandler(req => new AccountController().Login(req))); // passing the IHttpRequest itself.

            appRouteConfig
                .AddRoute("/logout", new PostHandler(req => new AccountController().Logout(req)));

            // So, adding routes with more than 1 parameter is irrecognizable for the server.
            // Therefore, the server cannot return a response.
            // I ought to fix that.
            // Done. ~ The process was being located in the ServerRouteConfig class.
            appRouteConfig
                .AddRoute("/shopping/add/{(?<id>[0-9]+)}", new GetHandler(req => new ShoppingController().AddToCart(req)));

            appRouteConfig
                .AddRoute("/myShoppingCart", new GetHandler(req => new ShoppingController().ShowOrders(req)));

            appRouteConfig
                .AddRoute("/success", new PostHandler(req => new ShoppingController().Success(req)));
        }
    }
}
