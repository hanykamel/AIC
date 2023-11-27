using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.Permissions
{
    public static class Permissions
    {
        public static class AdminUsers
        {
            public const string LookupsManage = "lookups.manage";
            public const string SysetmUsersManage = "systemusers.manage";
        }

        public static class CitizenManagers
        {
            public const string Manage = "citizen.manage";
        }
        public static class MemberManagers
        {
            public const string Manage = "member.manage";
        }
        public static class Members
        {
            public const string Memeber = "member";
        }
        public static class Citizens
        {
            public const string Citizen = "citizen";
        }
        public static class SubscribersManagers
        {
            public const string Manage = "subscribers.manage";
        }
        public static class ProsecutionsRequestsAdmin
        {
            public const string Manage = "citizenrequests.manage";
        }
        public static class GeneralSecretariatAdmin
        {
            public const string Manage = "GeneralSecretariat.manage";
        }
    }
}
