using System;
using System.Web.Mvc;

namespace MPL.Common.Web.Mvc
{
    /// <summary>
    /// A class that defines extension functions for the ControllerBase class.
    /// </summary>
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// Gets the display name of model utilised by the specified controller.
        /// </summary>
        /// <param name="controllerBase">A ControllerBase that is the controller to get the model from.</param>
        /// <returns>A string containing the display name of the controller model.</returns>
        public static string GetModelDisplayName(this ControllerBase controllerBase)
        {
            HelperFunctions.GetDisplayNameAttributeValue(HelperFunctions.GetModelType(controllerBase), out string ReturnValue);
            return ReturnValue;
        }
    }
}