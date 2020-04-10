namespace MyCoolWebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using Views;
    using Views.Home;

    public class HomeController
    {
        // GET /
        // GET /index.html
        public IHttpResponse Index()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new IndexView());

            response.Cookies.Add(new HttpCookie("lang", "en"));

            return response;
        }

        // GET /sessionTest.html
        public IHttpResponse SessionTest(IHttpRequest request)
        {
            var session = request.Session;

            if (session.Get("saved_date") == null)
            {
                session.Add("saved_date", DateTime.UtcNow);
            }

            var response = new ViewResponse(HttpStatusCode.Ok, new SessionTestView(session.Get<DateTime>("saved_date")));

            return response;
        }
    }
}
