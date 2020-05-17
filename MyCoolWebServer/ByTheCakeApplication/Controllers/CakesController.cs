namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Data;
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System.Linq;
    using System.Text;

    public class CakesController : Controller
    {
        private readonly CakesData cakesData;

        public CakesController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";

            return this.FileViewResponse("cakes\\add");
        }

        public IHttpResponse Add(string name, string price)
        {
            this.cakesData.Add(name, price);

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["display"] = "block";

            return this.FileViewResponse("cakes\\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            this.ViewData[searchTermKey] = string.Empty;
            this.ViewData["showCart"] = "none";

            var results = new StringBuilder();

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                string searchTerm = req.UrlParameters[searchTermKey];

                this.ViewData[searchTermKey] = searchTerm;

                var allCakes = this.cakesData.GetCakes()
                    .Where(c => c.Name.ToLower().Contains(req.UrlParameters[searchTermKey].ToLower()))
                    .Select(c => 
                            results.AppendLine($@"<div>{c.Name} ${c.Price} <button><a href=""shopping/add/{c.Id}?searchTerm={req.UrlParameters[searchTermKey]}"">Order</a></button></div>"))
                    .ToArray();


            }

            // The current session ~
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            this.ViewData["productsCount"] = shoppingCart.Orders.Count.ToString();
            this.ViewData["showCart"] = "block";
            this.ViewData["results"] = results.ToString();

            return this.FileViewResponse("cakes\\search");
        }

        public IHttpResponse Login() => this.FileViewResponse("cakes\\login");
    }
}
