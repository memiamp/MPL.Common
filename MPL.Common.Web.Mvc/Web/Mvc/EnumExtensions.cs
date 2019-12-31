using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MPL.Common.Web.Mvc
{
    /// <summary>
    /// A class that provides additional extensions to enumerations in support of MVC.
    /// </summary>
    public static class EnumExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Gets the display name (set via the DisplayAttribute) for the specified enumeration value.
        /// </summary>
        /// <param name="enumeration">An Enum that is the value to get the display name of.</param>
        /// <returns>A string containing the display name.</returns>
        public static string GetEnumDisplayName(this Enum enumeration)
        {
            string EnumName;
            Type EnumType;
            string ReturnValue;

            EnumType = enumeration.GetType();
            EnumName = Enum.GetName(EnumType, enumeration);
            ReturnValue = EnumName;

            try
            {
                MemberInfo[] Members;

                Members = EnumType.GetMember(EnumName);
                if (Members != null && Members.Length > 0)
                {
                    object[] Attributes;

                    Attributes = Members[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (Attributes != null && Attributes.Length > 0)
                    {
                        DisplayAttribute Attribute;

                        Attribute = (DisplayAttribute)Attributes[0];
                        if (Attribute.ResourceType != null)
                            ReturnValue = Attribute.GetName();
                        else
                            ReturnValue = Attribute.Name;
                    }
                }
            }
            catch { }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}