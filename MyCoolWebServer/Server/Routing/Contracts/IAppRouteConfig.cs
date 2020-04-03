namespace MyCoolWebServer.Server.Routing.Contracts
{
    using Enums;
    using Handlers;
    using System.Collections.Generic;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes { get; }

        void AddRoute(string route, RequestHandler handler);
    }
}
