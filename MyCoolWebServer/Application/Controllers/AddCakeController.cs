namespace MyCoolWebServer.Application.Controllers
{
    using Views.AddCake;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class AddCakeController
    {
        public IHttpResponse Add()
        {
            return new ViewResponse(HttpStatusCode.Ok, new AddCakeView());
        }
    }
}
