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

            string sessionIdToSend = null;

            if (!httpContext.Request.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var sessionId = Guid.NewGuid().ToString();

                httpContext.Request.Session = SessionStore.Get(sessionId);

                sessionIdToSend = sessionId;
            }

            IHttpResponse response = this.handlerFunc.Invoke(httpContext.Request);

            if (sessionIdToSend != null)
            {
                response.Headers.Add(new HttpHeader("Set-Cookie",
                    $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/"));
            }

            if (!response.Headers.ContainsKey("Content-Type"))
            {
                response.Headers.Add(new HttpHeader("Content-Type", "text/html"));
            }

            foreach (var cookie in response.Cookies)
            {
                response.Headers.Add(new HttpHeader("Set-Cookie", cookie.ToString()));
            }

            return response;
        }
    }
}
