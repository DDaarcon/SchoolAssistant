namespace SchoolAssistant.Web.PagesRelated.ViewDataHelperGetters
{
    public sealed class ViewDataBoolGetter : ViewDataGetter<bool>
    {
        public ViewDataBoolGetter(string label, Func<IDictionary<string, object?>> vdGetter) : base(label, vdGetter) { }

        public override bool Value => (_Value as bool?) ?? false;

        public static implicit operator bool(ViewDataBoolGetter obj) => obj.Value;
    }
}
