namespace MyCoolWebServer.Server.Http
{
    using Common;
    using Contracts;
    using System;
    using System.Collections.Generic;

    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> parameters;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.parameters = new Dictionary<string, object>();
            this.Id = id;
        }

        public string Id { get; private set; }

        public void Add(string key, object value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));

            this.parameters[key] = value;
        }

        public void Clear()
        {
            this.parameters.Clear();
        }

        public bool Contains(string key) => this.parameters.ContainsKey(key);

        public object Get(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (!this.parameters.ContainsKey(key))
            {
                return null;
            }

            return this.parameters[key];
        }

        public T Get<T>(string key)
            => (T)this.Get(key);
    }
}
