using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IConductLessonSessionService
    {
        static string SESSION_KEY = "con-les";
        string SessionKey => SESSION_KEY;

        Task<long?> GetConductedLessonIdFromSessionAsync();
        Task RemoveConductedLessonFromSessionAsync();
        Task SetConductedLessonInSessionAsync(long lessonId);
    }

    [Injectable]
    public class ConductLessonSessionService : IConductLessonSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string SessionKey => IConductLessonSessionService.SESSION_KEY;

        private HttpContext _context = null!;

        public ConductLessonSessionService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<long?> GetConductedLessonIdFromSessionAsync()
        {
            if (!TryGetContext())
                return null;

            await _context.Session.LoadAsync().ConfigureAwait(false);
            var value = _context.Session.GetString(SessionKey);

            if (String.IsNullOrWhiteSpace(value))
                return null;

            if (!long.TryParse(value, out long lessonId))
                return null;

            return lessonId;
        }

        public async Task SetConductedLessonInSessionAsync(long lessonId)
        {
            if (!TryGetContext())
                return;

            await _context.Session.LoadAsync().ConfigureAwait(false);
            _context.Session.SetString(SessionKey, lessonId.ToString());

            await _context.Session.CommitAsync().ConfigureAwait(false);
        }

        public async Task RemoveConductedLessonFromSessionAsync()
        {
            if (!TryGetContext())
                return;

            await _context.Session.LoadAsync().ConfigureAwait(false);
            _context.Session.Remove(SessionKey);

            await _context.Session.CommitAsync().ConfigureAwait(false);
        }

        private bool TryGetContext()
        {
            _context ??= _httpContextAccessor.HttpContext!;
            return _context is not null;
        }
    }
}
