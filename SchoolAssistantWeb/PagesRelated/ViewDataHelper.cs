using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolAssistant.Infrastructure.Models.PagesRelated;

namespace SchoolAssistant.Web
{
    public static class ViewDataHelper
    {
        private static ViewDataDictionary? _vd;
        private static MockViewDataDictionary _mock = new();
        public static IDictionary<string, object?> _VD => (_vd as IDictionary<string, object?>) ?? (_mock as IDictionary<string, object?>);
        public static void For(ViewDataDictionary<dynamic> vd) => _vd = vd;




        public static bool EnableDataManagement => TakeBool(EnableDataManagementLabel);
        public static string EnableDataManagementLabel => OfEnableNavLink("DataManagement");
        public static bool EnableScheduleArranger => TakeBool(EnableScheduleArrangerLabel);
        public static string EnableScheduleArrangerLabel => OfEnableNavLink("ScheduleArranger");
        public static bool EnableUsersManagement => TakeBool(EnableUsersManagementLabel);
        public static string EnableUsersManagementLabel => OfEnableNavLink("UsersManagement");
        public static bool EnableUsersList => TakeBool(EnableUsersListLabel);
        public static string EnableUsersListLabel => OfEnableNavLink("UsersList");
        public static bool EnableUsersListStudents => TakeBool(EnableUsersListStudentsLabel);
        public static string EnableUsersListStudentsLabel => OfEnableNavLink("UsersListStudents");
        public static bool EnableUsersListTeachers => TakeBool(EnableUsersListTeachersLabel);
        public static string EnableUsersListTeachersLabel => OfEnableNavLink("UsersListTeachers");



        private const string ENABLE_NAV_LINK_PREFIX = "Enable";
        private static bool TakeBool(string fullLabel) => (_VD[fullLabel] as bool?) ?? false;
        private static string OfEnableNavLink(string what) => ENABLE_NAV_LINK_PREFIX + what;
    }
}
