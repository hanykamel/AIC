using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.Permissions
{
   public class CustomClaimTypes
    {
        public const string Roles = "Roles";
        public const string Permission = "projectname/permission";
        public const string FullName = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/fullname";
        //public const string ContactNumber = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/ContactNumber";
        public const string UserID = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/userid";
        public const string Tenants = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/Tenants"; 
        public const string RoleTenants = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/RoleTenants"; 
    }
}
