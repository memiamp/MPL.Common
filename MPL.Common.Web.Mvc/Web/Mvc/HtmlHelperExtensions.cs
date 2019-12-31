using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;

namespace MPL.Common.Web.Mvc
{
    /// <summary>
    /// A class that defines extensions to the HtmlHelper class.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region Declarations
        #region _Constants_
        private const string cSCRIPT_REFERENCE = "dynscript";
        private const string cSCRIPT_FORMAT = "{0}_{1}_{2}";

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private static MvcHtmlString DisableDropDownItem(MvcHtmlString source, string sourceItemName, string sourceItemValue = "", string targetItemValue = "")
        {
            string htmlString;
            MvcHtmlString returnValue;
            string sourceString;

            sourceString = $"<option value=\"{sourceItemValue}\">{sourceItemName}</option>";

            htmlString = source.ToHtmlString();
            if (htmlString.Contains(sourceString))
            {
                string replaceString;

                replaceString = $"<option value=\"{targetItemValue}\" disabled=\"disabled\" selected=\"selected\">{sourceItemName}</option>";
                htmlString = htmlString.Replace(sourceString, replaceString);
            }
            returnValue = new MvcHtmlString(htmlString);

            return returnValue;
        }

        private static bool GetRouteValue(this HtmlHelper html, string name, out string value)
        {
            bool ReturnValue = false;

            // Defaults
            value = null;

            // Verify route key exists
            if (html.ViewContext != null &&
                html.ViewContext.RouteData != null &&
                html.ViewContext.RouteData.Values != null &&
                html.ViewContext.RouteData.Values.ContainsKey(name))
            {
                value = html.ViewContext.RouteData.Values[name].ToString();
                ReturnValue = true;
            }

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Adds a script section to be output by a call to OutputScripts.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="script">A function delagate that accepts object and returns HelperResult that contains the script.</param>
        /// <param name="order">An int that indicates the load order for a script (or 0 for any order).</param>
        /// <returns>A MvcHtmlString that is empty.</returns>
        public static MvcHtmlString AddScript(this HtmlHelper html, Func<object, HelperResult> script, int order = 0)
        {
            html.ViewContext.HttpContext.Items[string.Format(cSCRIPT_FORMAT, cSCRIPT_REFERENCE, order.ToString(), Guid.NewGuid())] = new OrderedScript(order, script);

            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items, and option label, with the opton label disabled.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="selectList">An IEnumerable of SelectListItem objects that are used to populate the drop-down list.</param>
        /// <param name="optionLabel">A string containing the text for a default empty item. This parameter can be null.</param>
        /// <param name="optionLabelValue">A string containing the value that should be assigned to the option label.</param>
        /// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
        public static MvcHtmlString DropDownListWithDisabledFirstItemFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, int optionLabelValue = 0)
        {
            return DisableDropDownItem(htmlHelper.DropDownListFor(expression, selectList, optionLabel), optionLabel, string.Empty, optionLabelValue.ToString());
        }
        /// <summary>
        /// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items, option label, and HTML attributes, with the opton label disabled.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="selectList">An IEnumerable of SelectListItem objects that are used to populate the drop-down list.</param>
        /// <param name="optionLabel">A string containing the text for a default empty item. This parameter can be null.</param>
        /// <param name="htmlAttributes">An IDictionary of key string and value object that contains the HTML attributes to set for the element.</param>
        /// <param name="optionLabelValue">A string containing the value that should be assigned to the option label.</param>
        /// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
        public static MvcHtmlString DropDownListWithDisabledFirstItemFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes, int optionLabelValue = 0)
        {
            return DisableDropDownItem(htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes), optionLabel, string.Empty, optionLabelValue.ToString());
        }
        /// <summary>
        /// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items, option label, and HTML attributes, with the opton label disabled.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="selectList">An IEnumerable of SelectListItem objects that are used to populate the drop-down list.</param>
        /// <param name="optionLabel">A string containing the text for a default empty item. This parameter can be null.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="optionLabelValue">A string containing the value that should be assigned to the option label.</param>
        /// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
        public static MvcHtmlString DropDownListWithDisabledFirstItemFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, string optionLabelValue = "0")
        {
            return DisableDropDownItem(htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes), optionLabel, string.Empty, optionLabelValue);
        }

        /// <summary>
        /// Loads and embeds the content of an SVG file.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="svgPath">A string indicating the virtual path to the file.</param>
        /// <param name="htmlAttributes">A dictionary with key string and value object that contains the HTML attributes to set for the element.</param>
        /// <returns>A MvcHtmlString containing the embedded SVG.</returns>
        public static MvcHtmlString EmbedSvg(this HtmlHelper html, string svgPath, IDictionary<string, object> htmlAttributes)
        {
            MvcHtmlString ReturnValue = MvcHtmlString.Empty;
            string SvgPath;

            SvgPath = HttpContext.Current.Server.MapPath(svgPath);
            if (File.Exists(SvgPath))
            {
                TagBuilder ControlBuilder;
                string Content;

                Content = File.ReadAllText(SvgPath);
                ControlBuilder = HelperFunctions.GetTagBuilder(Content);
                ControlBuilder.MergeAttributes(htmlAttributes);
                ReturnValue = new MvcHtmlString(ControlBuilder.ToString());
            }

            return ReturnValue;
        }
        /// <summary>
        /// Loads and embeds the content of an SVG file.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="svgPath">A string indicating the virtual path to the file.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>A MvcHtmlString containing the embedded SVG.</returns>
        public static MvcHtmlString EmbedSvg(this HtmlHelper html, string svgPath, object htmlAttributes)
        {
            return EmbedSvg(html, svgPath, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Returns an HTML select element for each value in the enumeration that is represented by the specified expression and predicate.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TEnum">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="optionLabel">The text for a default empty item. This parameter can be null.</param>
        /// <param name="predicate">A <see cref="Func{TEnum, bool}"/> to filter the items in the enums.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML select element for each value in the enumeration that is represented by the expression and the predicate.</returns>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, Func<TEnum, bool> predicate, string optionLabel, object htmlAttributes)
            where TEnum : struct, IConvertible
        {
            ModelMetadata Metadata;
            MvcHtmlString ReturnValue;
            IList<SelectListItem> SelectList;

            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The specified TEnum is not an enumeration", "TEnum");
            if (expression == null)
                throw new ArgumentNullException("The specified expression is NULL", "expression");

            Metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            SelectList = Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Where(e => predicate(e))
                    .Select(e => new SelectListItem
                    {
                        Value = Convert.ToUInt64(e).ToString(),
                        Text = ((Enum)(object)e).GetEnumDisplayName(),
                    }).ToList();
            if (!string.IsNullOrEmpty(optionLabel))
                SelectList.Insert(0, new SelectListItem { Text = optionLabel, Disabled = true, Selected = true });

            ReturnValue = htmlHelper.DropDownListFor(expression, SelectList, htmlAttributes);

            return ReturnValue;
        }

        /// <summary>
        /// Generates a lookup function in JavaScript for the specified enum.
        /// </summary>
        /// <typeparam name="T">An enum that is the enumeration to generate the lookup function for.</typeparam>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="ignoreZeroValues">A bool indicating whether to ignore zero values.</param>
        /// <returns>A MvcHtmlString containing the lookup function.</returns>
        public static MvcHtmlString GenerateJavascriptEnumLookupFunction<T>(this HtmlHelper html, bool ignoreZeroValues = true)
            where T : struct, IConvertible
        {
            int[] IgnoreValues = null;
            MvcHtmlString ReturnValue;

            if (ignoreZeroValues)
                IgnoreValues = new int[] { 0 };
            ReturnValue = html.GenerateJavascriptEnumLookupFunction<T>(IgnoreValues);

            return ReturnValue;
        }
        /// <summary>
        /// Generates a lookup function in JavaScript for the specified enum.
        /// </summary>
        /// <typeparam name="T">An enum that is the enumeration to generate the lookup function for.</typeparam>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="ignoreValues">An array of int indicating any values to be ignored.</param>
        /// <returns>A MvcHtmlString containing the lookup function.</returns>
        public static MvcHtmlString GenerateJavascriptEnumLookupFunction<T>(this HtmlHelper html, int[] ignoreValues)
            where T : struct, IConvertible
        {
            bool HasOutput = false;
            string ControlOutput;
            MvcHtmlString ReturnValue = MvcHtmlString.Empty;
            Type EnumType;

            EnumType = typeof(T);
            if (ignoreValues == null)
                ignoreValues = new int[0];

            ControlOutput = string.Format("function {0}(id){{", html.GetJavascriptEnumLookupFunctionName<T>());
            ControlOutput += "var ReturnValue='';switch(id){";
            foreach (int Value in Enum.GetValues(EnumType))
            {
                if (!ignoreValues.Contains(Value))
                {
                    string Name;

                    // Lookup display name
                    Name = Enum.GetName(EnumType, Value);
                    if (!HelperFunctions.GetDisplayAttributeNameFromField(EnumType, Name, out string DisplayName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                        DisplayName = Name;

                    ControlOutput += string.Format("case {0}:ReturnValue='{1}';break;", Value, DisplayName);
                    HasOutput = true;
                }
            }
            ControlOutput += "}return ReturnValue;}";

            if (HasOutput)
                ReturnValue = new MvcHtmlString(ControlOutput);

            return ReturnValue;
        }

        /// <summary>
        /// Gets the display name of the current action being executed.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <returns>A string containing the display name of the model.</returns>
        public static string GetActionDisplayName(this HtmlHelper html)
        {
            string ReturnValue = "Unknown";

            if (html.ViewContext != null &&
                html.ViewContext.Controller != null &&
                html.ViewContext.RouteData != null &&
                html.ViewContext.RouteData.Values != null &&
                html.ViewContext.RouteData.Values.ContainsKey("Action"))
            {
                string ActionName;
                Type ControllerType;

                ActionName = html.ViewContext.RouteData.Values["Action"].ToString();
                ControllerType = html.ViewContext.Controller.GetType();
                HelperFunctions.GetDisplayNameAttributeValueFromMethod(ControllerType, ActionName, out ReturnValue);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the name of the current action.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="actionName">A string that will be set to the name of the action.</param>
        /// <returns>A bool indicataing success.</returns>
        public static bool GetActionName(this HtmlHelper html, out string actionName)
        {
            return html.GetRouteValue("action", out actionName);
        }

        /// <summary>
        /// Gets the breadcrumb for the current location.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="separator">A MvcHtmlString containing the separator to use between the breadcrumbs.</param>
        /// <param name="homeController">A string containing the name of the homepage controller.</param>
        /// <param name="indexAction">A string containing the name of the index action.</param>
        /// <returns>A MvcHtmlString containing the breadcrumb.</returns>
        public static MvcHtmlString GetBreadcrumb(this HtmlHelper html, MvcHtmlString separator, string homeController = "Home", string indexAction = "Index")
        {
            MvcHtmlString ReturnValue;

            if (html.GetControllerName(out string ControllerName) && html.GetActionName(out string ActionName))
            {
                if (ControllerName != homeController)
                {
                    string ModelName;

                    ModelName = html.GetModelDisplayName();
                    if (ModelName == null)
                        ModelName = ControllerName;

                    ReturnValue = LinkExtensions.ActionLink(html, homeController, indexAction, homeController);
                    ReturnValue = ReturnValue.Concat(separator);
                    if (ActionName != indexAction)
                    {
                        string ActionDisplayName;

                        ActionDisplayName = html.GetActionDisplayName();
                        if (ActionDisplayName == null)
                            ActionDisplayName = ActionName;

                        ReturnValue = ReturnValue.Concat(LinkExtensions.ActionLink(html, ModelName, indexAction, html.ViewContext.RouteData.Values["controller"].ToString()));
                        ReturnValue = ReturnValue.Concat(separator);
                        ReturnValue = ReturnValue.Concat(ActionDisplayName);
                    }
                    else
                        ReturnValue = ReturnValue.Concat(ModelName);

                }
                else
                    ReturnValue = new MvcHtmlString(homeController);
            }
            else
                ReturnValue = MvcHtmlString.Empty;

            return ReturnValue;
        }
        /// <summary>
        /// Gets the breadcrumb for the current location.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="separator">A string containing the separator HTML to use between the breadcrumbs.</param>
        /// <param name="homeController">A string containing the name of the homepage controller.</param>
        /// <param name="indexAction">A string containing the name of the index action.</param>
        /// <returns>A MvcHtmlString containing the breadcrumb.</returns>
        public static MvcHtmlString GetBreadcrumb(this HtmlHelper html, string separator = " | ", string homeController = "Home", string indexAction = "Index")
        {
            return GetBreadcrumb(html, MvcHtmlString.Create(separator), homeController, indexAction);
        }

        /// <summary>
        /// Gets the name of the current controller.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="controllerName">A string that will be set to the name of the controller.</param>
        /// <returns>A bool indicataing success.</returns>
        public static bool GetControllerName(this HtmlHelper html, out string controllerName)
        {
            return html.GetRouteValue("controller", out controllerName);
        }

        /// <summary>
        /// Gets the name of the JavaScript lookup function for an enumeration.
        /// </summary>
        /// <typeparam name="T">An enum that is the enumeration to get the lookup function name for.</typeparam>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <returns>A string containing the name.</returns>
        public static string GetJavascriptEnumLookupFunctionName<T>(this HtmlHelper html)
            where T : struct, IConvertible
        {
            return string.Format("lookupEnum{0}", (typeof(T)).Name);
        }

        /// <summary>
        /// Gets the display name of model being output by the current view.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <returns>A string containing the display name of the model.</returns>
        public static string GetModelDisplayName(this HtmlHelper html)
        {
            if (!HelperFunctions.GetDisplayNameAttributeValue(HelperFunctions.GetModelType(html), out string returnValue))
                returnValue = "Unknown";

            return returnValue;
        }

        /// <summary>
        /// Returns the specified string if the current page is the specified page.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="controllers">An array of string containing name of controllers to check.</param>
        /// <param name="actions">An array of string containing the name of actions to check.</param>
        /// <param name="outputString">A string containing the string to output.</param>
        /// <returns>A string containing the specified output or NULL.</returns>
        public static string IsSelected(this HtmlHelper html, string[] controllers, string[] actions, string outputString)
        {
            string CurrentAction;
            string CurrentController;
            string ReturnValue = null;
            RouteValueDictionary RouteValues;
            ViewContext ViewContext;

            ViewContext = html.ViewContext;
            if (ViewContext.Controller.ControllerContext.IsChildAction)
                ViewContext = html.ViewContext.ParentActionViewContext;

            RouteValues = ViewContext.RouteData.Values;
            CurrentAction = RouteValues["action"].ToString();
            CurrentController = RouteValues["controller"].ToString();

            if (actions.Length == 0 || (actions.Length > 0 && actions.Contains(CurrentAction)) &&
                controllers.Length == 0 || (controllers.Length > 0 && controllers.Contains(CurrentController)))
                ReturnValue = outputString;

            return ReturnValue;
        }
        /// <summary>
        /// Returns the specified string if the current page is the specified page.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <param name="controllers">A string containing a comma separated list of the name of the controller to check.</param>
        /// <param name="actions">A string containing a comma separated list of the name of the action to check.</param>
        /// <param name="outputString">A string containing the string to output.</param>
        /// <returns>A string containing the specified CSS class or NULL.</returns>
        public static string IsSelected(this HtmlHelper html, string controllers, string actions, string outputString)
        {
            string[] Actions;
            string[] Controllers;

            if (controllers == null)
                Controllers = new string[] { };
            else
                Controllers = controllers.Trim().Split(',').Distinct().ToArray();
            if (actions == null)
                Actions = new string[] { };
            else
                Actions = actions.Trim().Split(',').Distinct().ToArray();

            return IsSelected(html, Controllers, Actions, outputString);
        }

        /// <summary>
        /// Outputs any scripts that were added via AddScript.
        /// </summary>
        /// <param name="html">A HtmlHelper that is the current HTML helper.</param>
        /// <returns>A MvcHtmlString that is empty.</returns>
        public static MvcHtmlString OutputScripts(this HtmlHelper html)
        {
            List<OrderedScript> Scripts;

            Scripts = new List<OrderedScript>();

            foreach (object key in html.ViewContext.HttpContext.Items.Keys)
                if (key.ToString().StartsWith(cSCRIPT_REFERENCE))
                {
                    OrderedScript Script;

                    Script = html.ViewContext.HttpContext.Items[key] as OrderedScript;
                    if (Script != null)
                        Scripts.Add(Script);
                }

            Scripts.Sort();
            for (int i = 0; i < Scripts.Count; i++)
                html.ViewContext.Writer.Write(Scripts[i].Script(null));

            return MvcHtmlString.Empty;
        }

        #endregion
        #endregion
    }
}