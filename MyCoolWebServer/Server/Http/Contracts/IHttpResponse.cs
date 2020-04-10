namespace MyCoolWebServer.Server.Http.Contracts
{
    using Enums;

    public interface IHttpResponse
    {
        HttpHeaderCollection Headers { get; }

        HttpStatusCode StatusCode { get; }

        HttpCookieCollection Cookies { get; }
    }
}
