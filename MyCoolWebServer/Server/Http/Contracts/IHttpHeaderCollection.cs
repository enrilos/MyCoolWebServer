namespace MyCoolWebServer.Server.Http.Contracts
{
    using System.Collections.Generic;

    public interface IHttpHeaderCollection : IEnumerable<ICollection<HttpHeader>>
    {
        void Add(HttpHeader httpHeader);

        bool ContainsKey(string key);

        ICollection<HttpHeader> Get(string key);
    }
}
