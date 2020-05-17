namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Data;
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using System.Linq;
    using System.Text;

    public class ShoppingController : Controller
    {
        private readonly CakesData cakesData;

        public ShoppingController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse ShowOrders(IHttpRequest req)
        {
            this.ViewData["showOrders"] = "none";
            this.ViewData["totalCost"] = "$0.00";

            var result = new StringBuilder();
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            var orders = shoppingCart.Orders;
            var totalCost = orders.Select(c => c.Price).Sum();

            for (int i = 0; i < orders.Count; i++)
            {
                result.AppendLine($"<p>{orders[i].Name} - ${orders[i].Price:f2}</p>");
            }

            this.ViewData["showOrders"] = "block";
            this.ViewData["orders"] = result.ToString();
            this.ViewData["totalCost"] = totalCost.ToString("f2");

            return this.FileViewResponse("shopping\\myShoppingCart");
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);
            var cake = this.cakesData.Find(id);

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Orders.Add(cake);

            return new RedirectResponse($"/search?searchTerm={req.UrlParameters["searchTerm"]}&searchButton=Search");
        }

        public IHttpResponse Success(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Orders.Clear();

            return this.FileViewResponse("shopping\\success");
        }
    }
}