using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Help
{
    public abstract class TeacherToSubjectOperationsHelper<TThis, TRelated>
        where TThis : DbEntity
        where TRelated : DbEntity
    {
        protected readonly TThis _this;
        protected readonly ICollection<TeacherToMainSubject> _mainLinkings;
        protected readonly ICollection<TeacherToAdditionalSubject> _additionalLinkings;

        public TeacherToSubjectOperationsHelper(
            TThis thisObject,
            ICollection<TeacherToMainSubject> mainLinkings,
            ICollection<TeacherToAdditionalSubject> additionalLinkings)
        {
            _this = thisObject;
            _mainLinkings = mainLinkings;
            _additionalLinkings = additionalLinkings;
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

            _mainLinkings.Add(NewMainLinkingForNewlyCreated(related!));
        }

        public void AddMain(TRelated? related)
        {
            if (GetMainIfReferenced(related).linking is not null)
                return;

            _mainLinkings.Add(NewMainLinking(related!));
            _newlyAddedExistingMain.Add(related!);
        }



        public void AddNewlyCreatedAdditional(TRelated? related)
        {
            if (GetAdditionalIfReferenced(related).linking is not null)
                return;

            _additionalLinkings.Add(NewAdditionalLinkingForNewlyCreated(related!));
        }
        public void AddAdditional(TRelated? related)
        {
            if (GetAdditionalIfReferenced(related).linking is not null)
                return;

            _additionalLinkings.Add(NewAdditionalLinking(related!));
            _newlyAddedExistingAdditional.Add(related!);
        }



        public void RemoveMain(TRelated? related)
        {
            var (linking, addedRelated) = GetMainIfReferenced(related);
            if (linking is null)
                return;

            _mainLinkings.Remove(linking);
            if (addedRelated is not null)
                _newlyAddedExistingMain.Remove(addedRelated);
        }

        public void RemoveAdditional(TRelated? related)
        {
            var (linking, addedRelated) = GetAdditionalIfReferenced(related);
            if (linking is null)
                return;

            _additionalLinkings.Remove(linking);
            if (addedRelated is not null)
                _newlyAddedExistingAdditional.Remove(addedRelated);
        }


        private (TeacherToMainSubject? linking, TRelated? addedRelated) GetMainIfReferenced(TRelated? related) =>
            (_mainLinkings.FirstOrDefault(x => x.Subject == related || x.SubjectId == related?.Id),
            _newlyAddedExistingMain.FirstOrDefault(x => x == related));

        private (TeacherToAdditionalSubject? linking, TRelated? addedRelated) GetAdditionalIfReferenced(TRelated? related) =>
            (_additionalLinkings.FirstOrDefault(x => x.Subject == related || x.SubjectId == related?.Id),
            _newlyAddedExistingAdditional.FirstOrDefault(x => x == related));
    }
}
