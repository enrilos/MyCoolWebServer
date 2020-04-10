namespace MyCoolWebServer.Server.Http
{
    using Common;
    using Contracts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            if (!this.headers.ContainsKey(header.Key))
            {
                this.headers[header.Key] = new List<HttpHeader>(); 
            }

            this.headers[header.Key].Add(header);
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public ICollection<HttpHeader> Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present.");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                foreach (var headerValue in header.Value)
                {
                    result.AppendLine($"{header.Key}: {headerValue.Value}");
                }
            }

            return result.ToString().Trim();
        }

        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
          => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
          => this.headers.Values.GetEnumerator();
    }
}
