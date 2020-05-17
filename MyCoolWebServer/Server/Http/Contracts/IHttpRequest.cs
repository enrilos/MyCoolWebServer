namespace MyCoolWebServer.Server.Http.Contracts
{
    using Enums;
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        HttpHeaderCollection Headers { get; }

        HttpCookieCollection Cookies { get; }

        string Path { get; }

        HttpRequestMethod Method { get; }

        string Url { get; }

        IDictionary<string, string> UrlParameters { get; }

        IHttpSession Session { get; set; }

        void AddUrlParameter(string key, string value);
    }
}
