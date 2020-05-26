namespace MyCoolWebServer.ByTheCakeApplication
{
    using Controllers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Handlers;
    using Server.Routing.Contracts;
    using ViewModels.Account;

    public class ByTheCakeApp : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new ByTheCakeDbContext())
            {
                db.Database.Migrate();
            }
        }

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
                .AddRoute("/register", new GetHandler(req => new AccountController().Register()));

            appRouteConfig
                .AddRoute("/register", new PostHandler(req => new AccountController().Register(req, new RegisterUserViewModel
                {
                    Username = req.FormData["username"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirmPassword"]
                })));

            appRouteConfig
                .AddRoute("/login", new GetHandler(req => new AccountController().Login()));

            appRouteConfig
                .AddRoute("/login", new PostHandler(req => new AccountController().Login(req, new LoginUserViewModel
                {
                    Username = req.FormData["username"],
                    Password = req.FormData["password"]
                })));

            appRouteConfig
                .AddRoute("/profile", new GetHandler(req => new AccountController().Profile(req)));

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
