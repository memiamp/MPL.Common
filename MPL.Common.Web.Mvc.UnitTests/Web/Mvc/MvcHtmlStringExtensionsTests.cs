using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;

namespace MPL.Common.Web.Mvc
{
    [TestClass]
    public class MvcHtmlStringExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_GetEnumDisplayName_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(MvcHtmlString), typeof(MvcHtmlStringExtensions), nameof(MvcHtmlStringExtensions.Concat), new[] { typeof(string[]) }));
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(MvcHtmlString), typeof(MvcHtmlStringExtensions), nameof(MvcHtmlStringExtensions.Concat), new[] { typeof(MvcHtmlString[]) }));
        }

        [TestMethod]
        public void Concat_WithMvcHtmlString_Works()
        {
            string[] data;
            MvcHtmlString[] elements;
            MvcHtmlString htmlString;

            data = TestDataTestHelper.GetStrings(arraySize: 5);
            elements = new MvcHtmlString[data.Length];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = new MvcHtmlString(data[i]);

            htmlString = new MvcHtmlString(data[0]);
            Assert.AreEqual(htmlString.ToString(), data[0]);
            htmlString = htmlString.Concat(elements.Skip(1).ToArray());
            Assert.AreEqual(htmlString.ToString(), string.Concat(data));
        }

        [TestMethod]
        public void Concat_WithStringArray_Works()
        {
            string[] data;
            MvcHtmlString htmlString;

            data = TestDataTestHelper.GetStrings(arraySize: 5);
            htmlString = new MvcHtmlString(data[0]);
            Assert.AreEqual(htmlString.ToString(), data[0]);
            htmlString = htmlString.Concat(data.Skip(1).ToArray());
            Assert.AreEqual(htmlString.ToString(), string.Concat(data));
        }
    }
}