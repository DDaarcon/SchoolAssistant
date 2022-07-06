namespace SchoolAssistant.Web.PagesRelated.ViewDataHelperGetters
{
    public sealed class ViewDataEnumGetter<TEnum> : ViewDataGetter<TEnum?>
        where TEnum : struct
    {
        public ViewDataEnumGetter(string label, Func<IDictionary<string, object?>> vdGetter) : base(label, vdGetter) { }

        public override TEnum? Value => Enum.TryParse<TEnum>(_Value?.ToString(), out var value) ? value : null;

        public static implicit operator TEnum?(ViewDataEnumGetter<TEnum> obj) => obj.Value;
        public static implicit operator int?(ViewDataEnumGetter<TEnum> obj) => (object?)(TEnum?)obj is null ? null : (int)(object)(TEnum)obj;
    }
}
