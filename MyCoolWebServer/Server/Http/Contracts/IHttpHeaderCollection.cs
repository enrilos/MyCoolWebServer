namespace MyCoolWebServer.Server.Http.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader httpHeader);

        bool ContainsKey(string key);

        HttpHeader Get(string key);
    }
}
