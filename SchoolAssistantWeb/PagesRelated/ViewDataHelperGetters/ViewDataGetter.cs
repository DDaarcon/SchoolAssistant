namespace SchoolAssistant.Web.PagesRelated.ViewDataHelperGetters
{
    public abstract class ViewDataGetter<TStored>
    {
        protected readonly Func<IDictionary<string, object?>> _vdGetter;
        protected readonly string _label;

        protected ViewDataGetter(
            string label,
            Func<IDictionary<string, object?>> vdGetter)
        {
            _label = label;
            _vdGetter = vdGetter;
        }

        public string Label => _label;

        protected object? _Value => _vdGetter()[_label];
    }
}
