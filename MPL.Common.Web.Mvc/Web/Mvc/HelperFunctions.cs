using HtmlAgilityPack;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.Compilation;
using System.Web.Mvc;

namespace MPL.Common.Web.Mvc
{
    /// <summary>
    /// A class that defines helper functions for Web UI components.
    /// </summary>
    internal static class HelperFunctions
    {
        #region Methods
        #region _Internal_
        /// <summary>
        /// Gets the specified attribute from the specified type.
        /// </summary>
        /// <typeparam name="T">A T that is the attribute to get.</typeparam>
        /// <param name="memberInfo">A MemberInfo that is the member to get the attribute from.</param>
        /// <returns>A T that is the attribute from the type, or NULL.</returns>
        internal static T GetAttribute<T>(MemberInfo memberInfo)
            where T : Attribute
        {
            object[] Attributes;
            T ReturnValue = default(T);

            Attributes = memberInfo.GetCustomAttributes(true);
            if (Attributes != null)
            {
                foreach (object Item in Attributes)
                    if (Item is T)
                    {
                        ReturnValue = (T)Item;
                        break;
                    }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the specified attribute from the specified field of the specified type.
        /// </summary>
        /// <typeparam name="T">A T that is the attribute to get.</typeparam>
        /// <param name="type">A Type that is the item to get the value for.</param>
        /// <param name="fieldName">A string containing the name of the field on the type to get the display name for.</param>
        /// <param name="bindingFlags">A BindingFlags indicating the binding flags to use to find the method.</param>
        /// <returns>A T that is the attribute from the type, or NULL.</returns>
        internal static T GetAttributeFromField<T>(Type type, string fieldName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
            where T : Attribute
        {
            FieldInfo Field;
            T ReturnValue = default(T);

            // Find the method
            Field = type.GetField(fieldName, bindingFlags);
            if (Field != null)
                ReturnValue = GetAttribute<T>(Field);

            return ReturnValue;
        }

        /// <summary>
        /// Gets the specified attribute from the specified method of the specified type.
        /// </summary>
        /// <typeparam name="T">A T that is the attribute to get.</typeparam>
        /// <param name="type">A Type that is the item to get the value for.</param>
        /// <param name="methodName">A string containing the name of the method on the type to get the display name for.</param>
        /// <param name="bindingFlags">A BindingFlags indicating the binding flags to use to find the method.</param>
        /// <returns>A T that is the attribute from the type, or NULL.</returns>
        internal static T GetAttributeFromMethod<T>(Type type, string methodName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
            where T : Attribute
        {
            MethodInfo[] Methods;
            T ReturnValue = default(T);

            // Find the method
            Methods = type.GetMethods(bindingFlags);
            if (Methods != null)
                foreach (MethodInfo Method in Methods)
                    if (Method.Name == methodName)
                    {
                        ReturnValue = GetAttribute<T>(Method);
                        break;
                    }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the name from a display attribute, automatically looking up resource names as necessary.
        /// </summary>
        /// <param name="attribute">A DisplayAttribute that is the attribute to get the name for.</param>
        /// <param name="displayAttributeName">A string that will be set to the name of the attribute.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayAttributeName(DisplayAttribute attribute, out string displayAttributeName)
        {
            bool ReturnValue = false;

            // Defaults
            displayAttributeName = null;

            if (attribute != null)
            {
                if (attribute.ResourceType == null)
                {
                    displayAttributeName = attribute.Name;
                }
                else
                {
                    ResourceManager Resource;

                    Resource = new ResourceManager(attribute.ResourceType);
                    displayAttributeName = Resource.GetString(attribute.Name);
                }
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the name of the display attribute for the specified item.
        /// </summary>
        /// <param name="memberInfo">A MemberInfo that is the item to get the value for.</param>
        /// <param name="displayAttributeValue">A string that will be set to the name of the display attribute.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayAttributeName(MemberInfo memberInfo, out string displayAttributeName)
        {
            return GetDisplayAttributeName(GetAttribute<DisplayAttribute>(memberInfo), out displayAttributeName);
        }

        /// <summary>
        /// Gets the name of the display attribute for the field of the specified type.
        /// </summary>
        /// <param name="type">A Type that is the item to get the value for.</param>
        /// <param name="fieldName">A string containing the name of the field on the type to get the display name for.</param>
        /// <param name="displayAttributeName">A string that will be set to the name of the display attribute.</param>
        /// <param name="bindingFlags">A BindingFlags indicating the binding flags to use to find the method.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayAttributeNameFromField(Type type, string fieldName, out string displayAttributeName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            return GetDisplayAttributeName(GetAttributeFromField<DisplayAttribute>(type, fieldName, bindingFlags), out displayAttributeName);
        }

        /// <summary>
        /// Gets the name of the display attribute for the method of the specified type.
        /// </summary>
        /// <param name="type">A Type that is the item to get the value for.</param>
        /// <param name="methodName">A string containing the name of the method on the type to get the display name for.</param>
        /// <param name="displayAttributeName">A string that will be set to the name of the display attribute.</param>
        /// <param name="bindingFlags">A BindingFlags indicating the binding flags to use to find the method.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayAttributeNameFromMethod(Type type, string methodName, out string displayAttributeName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            return GetDisplayAttributeName(GetAttributeFromMethod<DisplayAttribute>(type, methodName, bindingFlags), out displayAttributeName);
        }

        /// <summary>
        /// Gets the value of the display name attribute for the specified item.
        /// </summary>
        /// <param name="memberInfo">A MemberInfo that is the item to get the value for.</param>
        /// <param name="displayNameAttributeValue">A string that will be set to the value of the display name attribute.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayNameAttributeValue(MemberInfo memberInfo, out string displayNameAttributeValue)
        {
            DisplayNameAttribute Attribute;
            bool ReturnValue = false;

            // Defaults
            displayNameAttributeValue = null;

            // Get the attribute value
            Attribute = GetAttribute<DisplayNameAttribute>(memberInfo);
            if (Attribute != null)
            {
                displayNameAttributeValue = Attribute.DisplayName;
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the value of the display name attribute for the field of the specified type.
        /// </summary>
        /// <param name="type">A Type that is the item to get the value for.</param>
        /// <param name="fieldName">A string containing the name of the field on the type to get the display name for.</param>
        /// <param name="displayNameAttributeValue">A string that will be set to the value of the display name attribute.</param>
        /// <param name="bindingFlags">A BindingFlags indicating the binding flags to use to find the method.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayNameAttributeValueFromField(Type type, string fieldName, out string displayNameAttributeValue, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            DisplayNameAttribute Attribute;
            bool ReturnValue = false;

            // Defaults
            displayNameAttributeValue = null;

            // Get the attribute value
            Attribute = GetAttributeFromField<DisplayNameAttribute>(type, fieldName, bindingFlags);
            if (Attribute != null)
            {
                displayNameAttributeValue = Attribute.DisplayName;
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the value of the display name attribute for the method of the specified type.
        /// </summary>
        /// <param name="type">A Type that is the item to get the value for.</param>
        /// <param name="methodName">A string containing the name of the method on the type to get the display name for.</param>
        /// <param name="displayNameAttributeValue">A string that will be set to the value of the display name attribute.</param>
        /// <param name="bindingFlags">A BindingFlags indicating the binding flags to use to find the method.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool GetDisplayNameAttributeValueFromMethod(Type type, string methodName, out string displayNameAttributeValue, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            DisplayNameAttribute Attribute;
            bool ReturnValue = false;

            // Defaults
            displayNameAttributeValue = null;

            // Get the attribute value
            Attribute = GetAttributeFromMethod<DisplayNameAttribute>(type, methodName, bindingFlags);
            if (Attribute != null)
            {
                displayNameAttributeValue = Attribute.DisplayName;
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the model type from the specified object.
        /// </summary>
        /// <param name="controllerBase">A ControllerBase that is the obejct to get the model type from.</param>
        /// <returns>A Type representing the model type.</returns>
        internal static Type GetModelType(ControllerBase controllerBase)
        {
            Type ReturnValue = null;

            if (controllerBase != null)
            {
                if (controllerBase.ViewData != null && controllerBase.ViewData.ModelMetadata != null)
                    ReturnValue = controllerBase.ViewData.ModelMetadata.ModelType;
            }
            else
                throw new ArgumentException("The specified object is NULL", nameof(controllerBase));

            return ReturnValue;

        }
        /// <summary>
        /// Gets the model type from the specified object.
        /// </summary>
        /// <param name="htmlHelper">A HtmlHelper that is the obejct to get the model type from.</param>
        /// <returns>A Type representing the model type.</returns>
        internal static Type GetModelType(HtmlHelper htmlHelper)
        {
            Type ReturnValue = null;

            if (htmlHelper != null)
            {
                string ViewPath;

                ViewPath = GetViewPath(htmlHelper);
                if (ViewPath != null)
                {
                    Type TargetType;

                    TargetType = BuildManager.GetCompiledType(ViewPath);
                    ReturnValue = GetModelType(TargetType);
                }
            }
            else
                throw new ArgumentException("The specified object is NULL", nameof(htmlHelper));

            return ReturnValue;
        }
        /// <summary>
        /// Gets the model type from the specified type.
        /// </summary>
        /// <param name="type">A Type that is the type to get the model type from.</param>
        /// <returns>A Type representing the model type.</returns>
        internal static Type GetModelType(Type type)
        {
            Type ReturnValue = null;

            while (type != null)
            {
                if (type.Name == "WebViewPage`1" && type.GenericTypeArguments != null && type.GenericTypeArguments.Length == 1)
                {
                    if (type.GenericTypeArguments[0].Name != "IEnumerable`1")
                    {
                        ReturnValue = type.GenericTypeArguments[0];
                        break;
                    }
                    else
                    {
                        ReturnValue = type.GenericTypeArguments[0].GenericTypeArguments[0];
                        break;
                    }
                }
                else
                    type = type.BaseType;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Generates a TagBuilder from the specified HTML string.
        /// </summary>
        /// <param name="html">A string containing the HTML to get the TagBuilder from.</param>
        /// <returns>A TagBuilder generated from the HTML.</returns>
        internal static TagBuilder GetTagBuilder(string html)
        {
            TagBuilder ReturnValue;

            if (html != null && html.Length > 0)
            {
                try
                {
                    HtmlNode TheNode;

                    TheNode = HtmlNode.CreateNode(html);
                    ReturnValue = new TagBuilder(TheNode.Name);
                    ReturnValue.MergeAttributes(TheNode.Attributes.ToDictionary(x => x.Name, x => x.Value));
                    ReturnValue.InnerHtml = TheNode.InnerHtml;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Could not generate TagBuilder from HTML", nameof(html), ex);
                }
            }
            else
                throw new ArgumentException("The specified HTML is NULL or empty", nameof(html));

            return ReturnValue;
        }

        /// <summary>
        /// Gets the current view path from the specified object.
        /// </summary>
        /// <param name="htmlHelper">A HtmlHelper that is the object to get the view path from.</param>
        /// <returns>A string containing the view path.</returns>
        internal static string GetViewPath(HtmlHelper htmlHelper)
        {
            string ReturnValue = null;

            if (htmlHelper != null)
            {
                if (htmlHelper.ViewContext != null && htmlHelper.ViewContext.View != null && htmlHelper.ViewContext.View is BuildManagerCompiledView)
                    ReturnValue = ((BuildManagerCompiledView)htmlHelper.ViewContext.View).ViewPath;
                else
                    throw new ArgumentException("The specified object cannot be used to obtain the view path", nameof(htmlHelper));
            }
            else
                throw new ArgumentException("The specified object is NULL", nameof(htmlHelper));

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}