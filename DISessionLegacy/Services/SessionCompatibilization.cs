using DISession.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace DISessionLegacy.Services
{
    public class SessionCompatibilization : ISessionCompatibilization
    {
        public bool IsAvailable => throw new NotImplementedException();

        public string Id => HttpContext.Current.Session.SessionID;

        public IEnumerable<string> Keys => (IEnumerable<string>)HttpContext.Current.Session.Keys;

        public void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string key)
        {

            return (byte[])HttpContext.Current.Session[key];

        }

        public int? GetInt32(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            return (string)HttpContext.Current.Session[key];
        }

        public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public void SetInt32(string key, int value)
        {
            throw new NotImplementedException();
        }

        public void SetString(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}