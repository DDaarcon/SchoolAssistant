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




        public static bool EnableDataManagement => TakeBool(EnableDataManagementLabel);    // 1
        public static string EnableDataManagementLabel => OfEnableNavLink("DataManagement");
        public static bool EnableScheduleArranger => TakeBool(EnableScheduleArrangerLabel);    // 2
        public static string EnableScheduleArrangerLabel => OfEnableNavLink("ScheduleArranger");
        public static bool EnableUsersManagement => TakeBool(EnableUsersManagementLabel);  // 3
        public static string EnableUsersManagementLabel => OfEnableNavLink("UsersManagement");
        public static bool EnableUsersList => TakeBool(EnableUsersListLabel);  // 3,5
        public static string EnableUsersListLabel => OfEnableNavLink("UsersList");
        public static bool EnableUsersListStudents => TakeBool(EnableUsersListStudentsLabel);  // 3,5
        public static string EnableUsersListStudentsLabel => OfEnableNavLink("UsersListStudents");
        public static bool EnableUsersListTeachers => TakeBool(EnableUsersListTeachersLabel);  // 3,5
        public static string EnableUsersListTeachersLabel => OfEnableNavLink("UsersListTeachers");



        private const string ENABLE_NAV_LINK_PREFIX = "Enable";
        private static bool TakeBool(string fullLabel) => (_VD[fullLabel] as bool?) ?? false;
        private static string OfEnableNavLink(string what) => ENABLE_NAV_LINK_PREFIX + what;
    }
}
