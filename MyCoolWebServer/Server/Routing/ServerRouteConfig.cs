namespace MyCoolWebServer.Server.Routing
{
    using Enums;
    using MyCoolWebServer.Server.Routing.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>>();

            var httpMethods = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            foreach (var method in httpMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeRouteConfig(appRouteConfig);
        }

        public Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes => this.routes;

        private void InitializeRouteConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var registrationRoute in appRouteConfig.Routes)
            {
                var requestMethod = registrationRoute.Key;
                var routesWithHandlers = registrationRoute.Value;

                foreach (var routeWithHander in routesWithHandlers)
                {
                    var route = routeWithHander.Key;
                    var handler = routeWithHander.Value;

                    var parameters = new List<string>();
                    var parsedRouteRegex = this.ParseRoute(route, parameters);
                    var routingContext = new RoutingContext(parameters, handler);

                    this.routes[requestMethod].Add(parsedRouteRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var result = new StringBuilder();
            result.Append("^");

            if (route == "/")
            {
                // It should be /$ not $/ ($ always at the end of a string) so the Regex can match it later on.
                // This was the first major mistake.
                result.Append("/$");
                return result.ToString();
            }

            var tokens = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(tokens, parameters, result);

            return result.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder result)
        {
            // ^account/details$
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = tokens.Length - 1 == i ? "$" : "/";
                var currentToken = tokens[i];

                if (!currentToken.StartsWith("{") && !currentToken.EndsWith("}"))
                {
                    // NOTE:
                    // Bug that wasn't routing (with tokens) due to the slash that I didn't put below.
                    // BEFORE:
                    // result.Append($"{currentToken}{end}");
                    // NOW:

                    result.Append($"/{currentToken}{end}");
                    continue;

                    // The routing with tokens works correctly now.
                    // The routes just need to be added in the MainApplication class.
                }

                var parameterRegex = new Regex("<\\w+>");
                var paramaterMatch = parameterRegex.Match(currentToken);

                if (!paramaterMatch.Success)
                {
                    continue;
                }

                // ^account/{(?<name>[a-z]+)}$

                var match = paramaterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);
                parameters.Add(parameter);

                var currentTokenWithoutCurlyBrackets = currentToken.Substring(1, currentToken.Length - 2);

                result.Append($"{currentTokenWithoutCurlyBrackets}{end}");
            }
        }
    }
}
