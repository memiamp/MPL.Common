using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace MPL.Common.Security.Principal
{
    /// <summary>
    /// A class that defines extensions to an IIdentity.
    /// </summary>
    public static class IIdentityExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Gets the user principal for the specified IIdentity.
        /// </summary>
        /// <param name="principal">An IIdentity to get the UserPrincipal for.</param>
        /// <returns>An UserPrincipal that is the user principal.</returns>
        public static UserPrincipal GetUserPrincipal(this IIdentity principal)
        {
            PrincipalContext context;
            UserPrincipal returnValue = null;

            context = new PrincipalContext(ContextType.Domain);
            returnValue = UserPrincipal.FindByIdentity(context, principal.Name);

            return returnValue;
        }

        #endregion
        #endregion
    }
}