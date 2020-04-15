namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System.Collections.Generic;

    public class CakesController : Controller
    {
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add() => this.FileViewResponse("cakes\\add");

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);

            return this.FileViewResponse("cakes\\add", new Dictionary<string, string>
            {
                ["name"] = name,
                ["price"] = price
            });
        }
    }
}
