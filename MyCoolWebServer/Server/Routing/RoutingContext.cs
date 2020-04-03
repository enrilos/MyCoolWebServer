namespace MyCoolWebServer.Server.Routing
{
    using Common;
    using Contracts;
    using Handlers;
    using System.Collections.Generic;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IEnumerable<string> parameters, RequestHandler requestHandler)
        {
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));
            CoreValidator.ThrowIfNull(requestHandler, nameof(requestHandler));

            this.Parameters = parameters;
            this.RequestHandler = requestHandler;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler RequestHandler { get; private set; }
    }
}
