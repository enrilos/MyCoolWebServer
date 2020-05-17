namespace MyCoolWebServer.Server.Common
{
    using Contracts;

    public class NotFoundView : IView
    {
        public string View()
        {
            return "<h1>404 Not Found</h1>";
        }
    }
}
