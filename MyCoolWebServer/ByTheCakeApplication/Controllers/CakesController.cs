namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesController : Controller
    {
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add() => this.FileViewResponse("cakes\\add", new Dictionary<string, string>()
        {
            ["display"] = "none"
        });

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);

            using (var streamWriter = new StreamWriter("../../../ByTheCakeApplication\\Data\\database.csv", true))
            {
                streamWriter.WriteLine($"{name},{price}");
            }

            return this.FileViewResponse("cakes\\add", new Dictionary<string, string>
            {
                ["name"] = name,
                ["price"] = price,
                ["display"] = "block"
            });
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            var results = string.Empty;

            if (urlParameters.ContainsKey("searchTerm"))
            {
                var allCakes = File.ReadAllLines("../../../ByTheCakeApplication\\Data\\database.csv")
                    .Where(x => x.ToLower().Contains(urlParameters["searchTerm"].ToLower()))
                    .ToArray();

                for (int i = 0; i < allCakes.Length; i++)
                {
                    var splittedKvp = allCakes[i].Split(new[] { ',' });
                    results += splittedKvp[0];
                    results += $" ${splittedKvp[1]}";
                    results += "\n";
                }
            }

            return this.FileViewResponse("cakes\\search", new Dictionary<string, string>
            {
                ["results"] = results
            });
        }
    }
}
