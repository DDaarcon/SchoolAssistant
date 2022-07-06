using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolAssistant.Infrastructure.Enums.PreviewHelper;
using SchoolAssistant.Infrastructure.Models.PagesRelated;
using SchoolAssistant.Web.PagesRelated.ViewDataHelperGetters;

namespace SchoolAssistant.Web
{
    public static class ViewDataHelper
    {
        private static ViewDataDictionary? _vd;
        private static MockViewDataDictionary _mock = new();
        public static IDictionary<string, object?> _VD => (_vd as IDictionary<string, object?>) ?? (_mock as IDictionary<string, object?>);
        public static void For(ViewDataDictionary<dynamic> vd) => _vd = vd;



        #region
        public readonly static ViewDataBoolGetter EnableDataManagement = new ViewDataBoolGetter(GetEnableNavLinkLabelFor("DataManagement"), GetVD);
        public readonly static ViewDataBoolGetter EnableScheduleArranger = new ViewDataBoolGetter(GetEnableNavLinkLabelFor("ScheduleArranger"), GetVD);
        public readonly static ViewDataBoolGetter EnableUsersManagement = new ViewDataBoolGetter(GetEnableNavLinkLabelFor("UsersManagement"), GetVD);
        public readonly static ViewDataBoolGetter EnableUsersList = new ViewDataBoolGetter(GetEnableNavLinkLabelFor("UsersList"), GetVD);
        public readonly static ViewDataBoolGetter EnableUsersListStudents = new ViewDataBoolGetter(GetEnableNavLinkLabelFor("UsersListStudents"), GetVD);
        public readonly static ViewDataBoolGetter EnableUsersListTeachers = new ViewDataBoolGetter(GetEnableNavLinkLabelFor("UsersListTeachers"), GetVD);


        private const string ENABLE_NAV_LINK_PREFIX = "Enable";
        private static string GetEnableNavLinkLabelFor(string what) => ENABLE_NAV_LINK_PREFIX + what;
        #endregion


        #region
        public readonly static ViewDataBoolGetter IsPreviewModeOn = new ViewDataBoolGetter("PreviewModeOn", GetVD);
        public readonly static ViewDataEnumGetter<PreviewMenuType> PreviewMenuType = new ViewDataEnumGetter<PreviewMenuType>("PreviewMenuType", GetVD);
        public readonly static ViewDataPreviewLoginsJsonGetter PreviewMenuLogins = new ViewDataPreviewLoginsJsonGetter("PreviewMenuLogins", GetVD);
        #endregion


        private static IDictionary<string, object?> GetVD() => _VD;
    }
}
