namespace MyCoolWebServer.Application.Controllers
{
    using Views.BrowseCakes;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class BrowseCakesController
    {
        public IHttpResponse Browse()
        {
            return new ViewResponse(HttpStatusCode.Ok, new BrowseCakesView());
        }
    }
}
