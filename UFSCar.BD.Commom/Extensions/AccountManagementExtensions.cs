using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace UFSCar.BD.Commom.Extensions
{
    public static class AccountManagementExtensions
    {

        public static String GetProperty(this Principal principal, String property)
        {
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
            if (directoryEntry.Properties.Contains(property))
                return directoryEntry.Properties[property].Value.ToString();
            else
                return String.Empty;
        }

        public static String GetCompany(this Principal principal)
        {
            return principal.GetProperty("company");
        }

        public static String GetDepartment(this Principal principal)
        {
            return principal.GetProperty("department");
        }

        public static String GetOffice(this Principal principal)
        {
            return principal.GetProperty("physicalDeliveryOfficeName");
        }

        public static Boolean IsLockedOut(this UserPrincipal user)
        {
            if (user.Enabled.HasValue && user.Enabled.Value)
                return user.IsAccountLockedOut();
            else
                return false;
        }

        public static Boolean Unlock(UserPrincipal user)
        {
            user.UnlockAccount();
            return true;
        }
    }
}
