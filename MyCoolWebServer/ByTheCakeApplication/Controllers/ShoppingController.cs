namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using System;
    using System.Linq;
    using ViewModels;

    public class ShoppingController : Controller
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IShoppingService shoppingService;

        public ShoppingController()
        {
            this.productService = new ProductService();
            this.userService = new UserService();
            this.shoppingService = new ShoppingService();
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.ProductIds.Any())
            {
                this.ViewData["orders"] = "No items in your cart.";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var productsInCart = this.productService.FindProductsInCart(shoppingCart.ProductIds);

                var items = productsInCart.Select(pr => $"<div>{pr.Name} - ${pr.Price:f2}</div><br />");
                var totalCost = productsInCart.Sum(pr => pr.Price);

                this.ViewData["orders"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalCost:f2}";
            }

            return this.FileViewResponse("shopping\\myShoppingCart");
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);
            var productExists = this.productService.Exists(id);

            if (!productExists)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.ProductIds.Add(id);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse($"/search?searchTerm={req.UrlParameters["searchTerm"]}&searchButton=Search");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.userService.GetUserId(username);

            if (userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist.");
            }

            var productsIds = shoppingCart.ProductIds;

            if (!productsIds.Any())
            {
                return new RedirectResponse("/");
            }

            this.shoppingService.CreateOrder(userId.Value, productsIds);

            shoppingCart.ProductIds.Clear();

            return this.FileViewResponse("shopping\\success");
        }
    }
}