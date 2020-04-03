namespace MyCoolWebServer.Server.Handlers
{
    using Common;
    using Contracts;
    using Http.Contracts;
    using Http;
    using System;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlerFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlerFunc)
        {
            CoreValidator.ThrowIfNull(handlerFunc, nameof(handlerFunc));

            this.handlerFunc = handlerFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            CoreValidator.ThrowIfNull(httpContext, nameof(httpContext));

            IHttpResponse httpResponse = this.handlerFunc.Invoke(httpContext.Request);

            httpResponse.Headers.Add(new HttpHeader("Content-Type", "text/html"));

            return httpResponse;
        }
    }
}
