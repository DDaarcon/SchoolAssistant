using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SchoolAssistant.Infrastructure.Models.PagesRelated
{
    public class MockViewDataDictionary : IDictionary<string, object?>
    {
        public object? this[string key] { get => null; set => throw new DoNotCallThisException(); }

        #region Irrelevant

        private sealed class DoNotCallThisException : Exception
        {
            public DoNotCallThisException() : base("It is a mockup class, it's not intended to call its methods.") { }
        }
        public ICollection<string> Keys => throw new DoNotCallThisException();

        public ICollection<object?> Values => throw new DoNotCallThisException();

        public int Count => throw new DoNotCallThisException();

        public bool IsReadOnly => throw new DoNotCallThisException();

        public void Add(string key, object? value)
        {
            throw new DoNotCallThisException();
        }

        public void Add(KeyValuePair<string, object?> item)
        {
            throw new DoNotCallThisException();
        }

        public void Clear()
        {
            throw new DoNotCallThisException();
        }

        public bool Contains(KeyValuePair<string, object?> item)
        {
            throw new DoNotCallThisException();
        }

        public bool ContainsKey(string key)
        {
            throw new DoNotCallThisException();
        }

        public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
        {
            throw new DoNotCallThisException();
        }

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            throw new DoNotCallThisException();
        }

        public bool Remove(string key)
        {
            throw new DoNotCallThisException();
        }

        public bool Remove(KeyValuePair<string, object?> item)
        {
            throw new DoNotCallThisException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object? value)
        {
            throw new DoNotCallThisException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new DoNotCallThisException();
        }

        #endregion
    }
}
