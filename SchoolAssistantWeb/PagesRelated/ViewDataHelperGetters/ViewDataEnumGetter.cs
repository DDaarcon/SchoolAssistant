namespace SchoolAssistant.Web.PagesRelated.ViewDataHelperGetters
{
    public sealed class ViewDataEnumGetter<TEnum> : ViewDataGetter<TEnum>
        where TEnum : struct
    {
        public ViewDataEnumGetter(string label, Func<IDictionary<string, object?>> vdGetter) : base(label, vdGetter) { }

        public static implicit operator TEnum?(ViewDataEnumGetter<TEnum> obj) => Enum.TryParse<TEnum>(obj._Value?.ToString(), out var value) ? value : null;
    }
}
