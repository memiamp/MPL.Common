using System;
using System.Linq;
using System.Web.Mvc;

namespace MPL.Common.Web.Mvc
{
    /// <summary>
    /// A class that defines extensions to the MvcHtmlString class.
    /// </summary>
    public static class MvcHtmlStringExtensions
    {
        #region Methods
        #region _Private_
        private static MvcHtmlString BuildString(params MvcHtmlString[] mvcHtmlStrings)
        {
            MvcHtmlString ReturnValue;

            ReturnValue = MvcHtmlString.Create(string.Concat(mvcHtmlStrings.Select(s => s.ToString())));

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Concatenate the specified parameters to the specified source.
        /// </summary>
        /// <param name="source">A MvcHtmlString that is the source to concatenate to.</param>
        /// <param name="strings">An array of string containing the data to be concatenated.</param>
        /// <returns>A MvcHtmlString containing the results.</returns>
        public static MvcHtmlString Concat(this MvcHtmlString source, params string[] strings)
        {
            MvcHtmlString[] NewParams;
            MvcHtmlString ReturnValue;

            NewParams = new MvcHtmlString[strings.Length];
            for (int i = 0; i < NewParams.Length; i++)
                NewParams[i] = new MvcHtmlString(strings[i]);
            ReturnValue = Concat(source, NewParams);

            return ReturnValue;
        }
        /// <summary>
        /// Concatenate the specified parameters to the specified source.
        /// </summary>
        /// <param name="source">A MvcHtmlString that is the source to concatenate to.</param>
        /// <param name="strings">An array of MvcHtmlString containing the data to be concatenated.</param>
        /// <returns>A MvcHtmlString containing the results.</returns>
        public static MvcHtmlString Concat(this MvcHtmlString source, params MvcHtmlString[] mvcHtmlStrings)
        {
            MvcHtmlString[] NewParams;
            MvcHtmlString ReturnValue;

            NewParams = new MvcHtmlString[mvcHtmlStrings.Length + 1];
            NewParams[0] = source;
            Array.Copy(mvcHtmlStrings, 0, NewParams, 1, mvcHtmlStrings.Length);
            ReturnValue = BuildString(NewParams);

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}