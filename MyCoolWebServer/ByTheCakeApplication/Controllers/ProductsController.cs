namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using System;
    using System.Linq;
    using ViewModels;
    using ViewModels.Products;

    public class ProductsController : Controller
    {
        private const string ProductAddView = "products\\add";

        private readonly IProductService productService;

        public ProductsController()
        {
            this.productService = new ProductService();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(ProductAddView);
        }

        public IHttpResponse Add(AddProductViewModel model)
        {
            string modelName = model.Name;
            string modelPrice = model.Price.ToString();
            string modelImageUrl = model.ImageUrl;

            if (modelName.Length < 3
                || modelName.Length > 30
                || model.ImageUrl.Length < 3
                || model.ImageUrl.Length > 2000)
            {
                this.ViewData["showResult"] = "none";
                this.AddError("Field characters are not in between the range.");
            }
            else
            {
                this.productService.Create(model.Name, model.Price, model.ImageUrl);

                this.ViewData["showError"] = "none";
                this.ViewData["name"] = modelName;
                this.ViewData["price"] = modelPrice.ToString();
                this.ViewData["imageUrl"] = modelImageUrl;
                this.ViewData["showResult"] = "block";
            }

            return this.FileViewResponse(ProductAddView);
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData[searchTermKey] = string.Empty;
            this.ViewData["showCart"] = "none";

            var searchTermValue = urlParameters.ContainsKey(searchTermKey)
                ? urlParameters[searchTermKey]
                : null;

            var result = this.productService.All(searchTermValue);

            if (!result.Any())
            {
                this.ViewData["results"] = "No cakes found";
            }
            else
            {
                var allProducts = result
                    .Select(c => $@"<div><a href=""/products/{c.Id}"">{c.Name}</a> - ${c.Price} <button><a href=""shopping/add/{c.Id}?searchTerm={searchTermValue}"">Order</a></button></div>");

                this.ViewData["results"] = string.Join(Environment.NewLine, allProducts);
            }

            // The current session ~
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.ProductIds.Any())
            {
                var totalOrdersCount = shoppingCart.ProductIds.Count;
                var singularOrPluralOrderType = totalOrdersCount > 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["productsCount"] = $"{totalOrdersCount} {singularOrPluralOrderType}";
            }

            return this.FileViewResponse("products\\search");
        }

        public IHttpResponse Details(int id)
        {
            var product = this.productService.Find(id);

            if (product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString("f2");
            this.ViewData["imageUrl"] = product.ImageUrl;

            return this.FileViewResponse("products\\details");
        }
    }
}
