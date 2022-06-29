using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests
{
    internal class MockedSession : ISession
    {
        public bool IsAvailable => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();

        public IEnumerable<string> Keys => _storage.Keys;

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _storage.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _storage.Add(key, value);
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return _storage.TryGetValue(key, out value);
        }

        private readonly IDictionary<string, byte[]> _storage = new Dictionary<string, byte[]>();
    }
}
