using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace MPL.Common.Security.Principal
{
    /// <summary>
    /// A class that defines extension methods for an IPrincipal.
    /// </summary>
    public static class IPrincipalExtensions
    {
        #region Methods
        #region _Private_
        private static IIdentity GetIdentity(this IPrincipal principal)
        {
            IIdentity returnValue;

            if (principal != null)
            {
                if (principal.Identity != null)
                    returnValue = principal.Identity;
                else
                    throw new ArgumentException("The specified principal has no identity", nameof(principal));
            }
            else
                throw new ArgumentException("The specified principal is NULL", nameof(principal));

            return returnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Gets the name for the user with the specified principal.
        /// </summary>
        /// <param name="principal">An IPrincipal that is the principal to get the name for.</param>
        /// <param name="removeDomainName">A bool that indicates whether to remove the domain name from the principal.</param>
        /// <returns>A string containing the name.</returns>
        public static string GetUserName(this IPrincipal principal, bool removeDomainName = true)
        {
            string returnValue;

            returnValue = principal.GetIdentity().Name;

            if (removeDomainName && returnValue.Contains(@"\"))
                returnValue = returnValue.Remove(0, returnValue.IndexOf(@"\") + 1);

            return returnValue;
        }

        /// <summary>
        /// Gets the user principal for the specified IPrincipal.
        /// </summary>
        /// <param name="principal">An IPrincipal to get the UserPrincipal for.</param>
        /// <returns>An UserPrincipal that is the user principal.</returns>
        public static UserPrincipal GetUserPrincipal(this IPrincipal principal)
        {
            return principal.GetIdentity().GetUserPrincipal();
        }

        #endregion
        #endregion
    }
}