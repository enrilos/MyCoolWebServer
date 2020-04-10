namespace MyCoolWebServer.Server.Http
{
    using System.Collections.Concurrent;

    public static class SessionStore
    {
        private static readonly ConcurrentDictionary<string, HttpSession> sessions =
            new ConcurrentDictionary<string, HttpSession>();

        public static string SessionCookieKey { get; } = "MY_SID";

        public static HttpSession Get(string id)
            => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}
