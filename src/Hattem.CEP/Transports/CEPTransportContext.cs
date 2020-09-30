using System.Collections.Generic;

namespace Hattem.CEP.Transports
{
    public sealed class CEPTransportContext
    {
        private Dictionary<string, object> _bag;

        public void Add(string key, object value)
        {
            EnsureInitialized();

            _bag[key] = value;
        }

        public object? Get(string key)
        {
            EnsureInitialized();

            return _bag.TryGetValue(key, out var value) ? value : null;
        }

        private void EnsureInitialized()
        {
            _bag ??= new Dictionary<string, object>();
        }
    }
}
