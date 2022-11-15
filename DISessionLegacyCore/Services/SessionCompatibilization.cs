namespace DISessionLegacyCore.Services
{
    public class SessionCompatibilization : ISessionCompatibilization
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public SessionCompatibilization(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        public byte[] Get(string key)
        {
            throw new NotImplementedException();
        }

        public int? GetInt32(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            return System.Web.HttpContext.Current.Session[key].ToString();
        }

        public void SetInt32(string key, int value)
        {
            throw new NotImplementedException();
        }

        public void SetString(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool IsAvailable { get; }
        public string Id { get; }
        public IEnumerable<string> Keys { get; }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}
