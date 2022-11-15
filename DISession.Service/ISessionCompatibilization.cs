using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DISession.Service
{
    public interface ISessionCompatibilization
    {
        byte[] Get(string key);
        int? GetInt32(string key);
        string GetString(string key);
        void SetInt32(string key, int value);
        void SetString(string key, string value);
        bool IsAvailable { get; }
        string Id { get; }
        IEnumerable<string> Keys { get; }
        void Clear();
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Remove(string key);
        void Set(string key, byte[] value);
        bool TryGetValue(string key, out byte[] value);
    }
}
