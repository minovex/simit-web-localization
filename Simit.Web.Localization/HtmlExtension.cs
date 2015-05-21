
namespace System.Web.Mvc
{
    #region Using Directives
    using Simit.Web.Localization;
    using System;
    #endregion

    public static class HtmlExtension
    {
        #region Public Static Methods
        public static MvcHtmlString GetResourceFromContainer(this HtmlHelper helper, string key, string defaultValue = "N/A")
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            string value = Container.Get(Container.CurrentLanguageCode, key, defaultValue);

            return new MvcHtmlString(value);
        }
        #endregion
    }
}
