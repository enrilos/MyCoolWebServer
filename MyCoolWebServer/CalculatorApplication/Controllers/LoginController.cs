namespace MyCoolWebServer.CalculatorApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using System.Collections.Generic;

    public class LoginController : Controller
    {
        public IHttpResponse Index() => this.FileViewResponse("\\login", new Dictionary<string, string>()
        {
            ["display"] = "none"
        });

        public IHttpResponse Index(string username, string password)
        {
            return this.FileViewResponse("\\login", new Dictionary<string, string>
            {
                ["content"] = $"Hi {username}, your password is {password}",
                ["display"] = "block"
            });
        }
    }
}
