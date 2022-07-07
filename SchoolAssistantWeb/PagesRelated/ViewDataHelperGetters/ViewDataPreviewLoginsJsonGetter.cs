using SchoolAssistant.Infrastructure.Models.PreviewMode;

namespace SchoolAssistant.Web.PagesRelated.ViewDataHelperGetters
{
    public sealed class ViewDataPreviewLoginsJsonGetter : ViewDataGetter<PreviewLoginsJson>
    {
        public ViewDataPreviewLoginsJsonGetter(string label, Func<IDictionary<string, object?>> vdGetter) : base(label, vdGetter) { }

        public override PreviewLoginsJson? Value => (_Value as PreviewLoginsJson) ?? null;

        public static implicit operator PreviewLoginsJson?(ViewDataPreviewLoginsJsonGetter obj) => obj.Value;
    }
}
