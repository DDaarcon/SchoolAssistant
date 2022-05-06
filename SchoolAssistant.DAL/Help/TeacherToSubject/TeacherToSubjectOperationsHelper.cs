using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Help
{
    public abstract class TeacherToSubjectOperationsHelper<TThis, TRelated>
        where TThis : DbEntity
        where TRelated : DbEntity
    {
        protected readonly TThis _this;
        protected ICollection<TeacherToMainSubject> _MainLinkings => _getMainLinkings();
        protected ICollection<TeacherToAdditionalSubject> _AdditionalLinkings => _getAdditionalLinkings();

        // required due to ef core lazy-loading 
        protected readonly Func<ICollection<TeacherToMainSubject>> _getMainLinkings;
        protected readonly Func<ICollection<TeacherToAdditionalSubject>> _getAdditionalLinkings;

        public TeacherToSubjectOperationsHelper(
            TThis thisObject,
            Func<ICollection<TeacherToMainSubject>> getMainLinkings,
            Func<ICollection<TeacherToAdditionalSubject>> getAdditionalLinkings)
        {
            _this = thisObject;
            _getMainLinkings = getMainLinkings;
            _getAdditionalLinkings = getAdditionalLinkings;
        }

        protected abstract TeacherToMainSubject NewMainLinkingForNewlyCreated(TRelated related);
        protected abstract TeacherToMainSubject NewMainLinking(TRelated related);

        protected abstract TeacherToAdditionalSubject NewAdditionalLinkingForNewlyCreated(TRelated related);
        protected abstract TeacherToAdditionalSubject NewAdditionalLinking(TRelated related);

        protected abstract IEnumerable<TRelated> SelectMain { get; }
        protected abstract IEnumerable<TRelated> SelectAdditional { get; }



        protected ICollection<TRelated> _newlyAddedExistingMain = new List<TRelated>();
        protected ICollection<TRelated> _newlyAddedExistingAdditional = new List<TRelated>();

        public IEnumerable<TRelated> MainIter => SelectMain
            .Where(x => x is not null)
            .Concat(_newlyAddedExistingMain)
            .Distinct();

        public IEnumerable<TRelated> AdditionalIter => SelectAdditional
            .Where(x => x is not null)
            .Concat(_newlyAddedExistingAdditional)
            .Distinct();




        public void AddNewlyCreatedMain(TRelated? related)
        {
            if (GetMainIfReferenced(related).linking is not null)
                return;

            _MainLinkings.Add(NewMainLinkingForNewlyCreated(related!));
        }

        public void AddMain(TRelated? related)
        {
            if (GetMainIfReferenced(related).linking is not null)
                return;

            _MainLinkings.Add(NewMainLinking(related!));
            _newlyAddedExistingMain.Add(related!);
        }



        public void AddNewlyCreatedAdditional(TRelated? related)
        {
            if (GetAdditionalIfReferenced(related).linking is not null)
                return;

            _AdditionalLinkings.Add(NewAdditionalLinkingForNewlyCreated(related!));
        }
        public void AddAdditional(TRelated? related)
        {
            if (GetAdditionalIfReferenced(related).linking is not null)
                return;

            _AdditionalLinkings.Add(NewAdditionalLinking(related!));
            _newlyAddedExistingAdditional.Add(related!);
        }



        public void RemoveMain(TRelated? related)
        {
            var (linking, addedRelated) = GetMainIfReferenced(related);
            if (linking is null)
                return;

            _MainLinkings.Remove(linking);
            if (addedRelated is not null)
                _newlyAddedExistingMain.Remove(addedRelated);
        }

        public void RemoveAdditional(TRelated? related)
        {
            var (linking, addedRelated) = GetAdditionalIfReferenced(related);
            if (linking is null)
                return;

            _AdditionalLinkings.Remove(linking);
            if (addedRelated is not null)
                _newlyAddedExistingAdditional.Remove(addedRelated);
        }


        private (TeacherToMainSubject? linking, TRelated? addedRelated) GetMainIfReferenced(TRelated? related) =>
            (_MainLinkings.FirstOrDefault(x => x.Subject == related || x.SubjectId == related?.Id),
            _newlyAddedExistingMain.FirstOrDefault(x => x == related));

        private (TeacherToAdditionalSubject? linking, TRelated? addedRelated) GetAdditionalIfReferenced(TRelated? related) =>
            (_AdditionalLinkings.FirstOrDefault(x => x.Subject == related || x.SubjectId == related?.Id),
            _newlyAddedExistingAdditional.FirstOrDefault(x => x == related));
    }
}
