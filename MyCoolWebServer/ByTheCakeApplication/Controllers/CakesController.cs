namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CakesController : Controller
    {
        private static string defaultDataFilePath = "../../../ByTheCakeApplication\\Data\\database.csv";
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";

            return this.FileViewResponse("cakes\\add");
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);

            var id = File.ReadAllLines(defaultDataFilePath).Length;

            using (var streamWriter = new StreamWriter(defaultDataFilePath, true))
            {
                streamWriter.WriteLine($"{id + 1},{name},{price}");
            }

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["display"] = "block";

            return this.FileViewResponse("cakes\\add");
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            var results = new StringBuilder();

            if (urlParameters.ContainsKey("searchTerm"))
            {
                var allCakes = File.ReadAllLines(defaultDataFilePath)
                    .Where(x => x.ToLower().Contains(urlParameters["searchTerm"].ToLower()))
                    .ToArray();

                for (int i = 0; i < allCakes.Length; i++)
                {
                    var splittedKvp = allCakes[i].Split(new[] { ',' });
                    results.Append("<div>");
                    results.Append(splittedKvp[1]);
                    results.Append($" ${splittedKvp[2]} ");
                    results.Append("<input type=\"submit\" name=\"order\" value=\"Order\" />");
                    results.AppendLine("</div>");
                }
            }

            this.ViewData["results"] = results.ToString().Trim();

            return this.FileViewResponse("cakes\\search");
        }

        public IHttpResponse Login() => this.FileViewResponse("cakes\\login");
    }
}
