using Microsoft.AspNetCore.Http;
using SchoolAssistant.Logic.General.Other.Help;

namespace SchoolAssistant.Logic.General.Other
{
    public interface ITextCryptographicService
    {
        Task<(bool keyIsStored, string? encrypted)> GetEncryptedAsync(string text, string id);
        Task<string?> GetDecryptedAsync(string text, string id);
    }
    [Injectable]
    public class TextCryptographicService : ITextCryptographicService, IDisposable
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICryptographicTools _tools;

        public TextCryptographicService(
            IHttpContextAccessor contextAccessor,
            ICryptographicTools tools)
        {
            _contextAccessor = contextAccessor;
            _tools = tools;
        }


        public async Task<(bool keyIsStored, string? encrypted)> GetEncryptedAsync(string text, string id)
        {
            _id = id; _entered = text;

            await ExecuteAsync(new Operations(
                GenerateKeyAsync,
                (string text) => _tools.Encode(text),
                SaveKeyToSessionAsync)).ConfigureAwait(false);

            return (_storedInSession, _response);
        }

        public async Task<string?> GetDecryptedAsync(string text, string id)
        {
            _id = id; _entered = text;

            await ExecuteAsync(new Operations(
                FetchKeyFromSessionAsync,
                (string text) => _tools.Decode(text),
                RemoveKeyFromSessionAsync)).ConfigureAwait(false);

            return _response;
        }


        private string _id = null!;

        private string _entered = null!;
        private string? _response;

        private ISession _session = null!;
        private bool _storedInSession;



        private record Operations(
            Func<ValueTask> fetchKeyAsync,
            Func<string, string> cryptoAction,
            Func<Task> sessionUpdate);
        private async Task ExecuteAsync(Operations operations)
        {
            try
            {
                FetchSession();

                await operations.fetchKeyAsync().ConfigureAwait(false);

                _response = operations.cryptoAction(_entered);

                await operations.sessionUpdate().ConfigureAwait(false);

            }
            catch (CryptographicProcessException e)
            {
                System.Diagnostics.Trace.WriteLine(e.ToString());

                _storedInSession = false;
                _response = null;
            }
        }

        #region Fetching (or generating) key

        private ValueTask GenerateKeyAsync()
        {
            _tools.GenerateKey();
            _storedInSession = false;

            return ValueTask.CompletedTask;
        }

        private async ValueTask FetchKeyFromSessionAsync()
        {
            await _session.LoadAsync().ConfigureAwait(false);
            var key = _session.Get(_SessionId);

            if (key is null) throw new CryptographicProcessException("Not found key in session");
            _tools.Key = key;
        }

        #endregion

        #region Updating Session storage

        private async Task SaveKeyToSessionAsync()
        {
            _session.Set(_SessionId, _tools.Key);
            await _session.CommitAsync().ConfigureAwait(false);

            _storedInSession = true;
        }

        private async Task RemoveKeyFromSessionAsync()
        {
            _session.Remove(_SessionId);
            await _session.CommitAsync().ConfigureAwait(false);
        }

        #endregion

        private string _SessionId => $"passEnc-{_id}";

        private void FetchSession()
        {
            _session ??= _contextAccessor.HttpContext?.Session!;

            if (_session is null)
                throw new CryptographicProcessException("Could not fetch ISession");
        }

        public void Dispose()
        {
            _tools.Dispose();
        }
    }
}
