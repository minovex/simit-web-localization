
namespace Simit.Web.Localization
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    #endregion

    public static class Container
    {
        #region Public Static Fields
        public static string ApplicationLocalizationKeyName = "LocalizationContainer";
        public static string SessionCurrentLanguageCodeKeyName = "CurrentLanguageCodeKeyName";
        #endregion

        #region Private Static Properties
        private static Dictionary<string, Dictionary<string, string>> LocationContainer
        {
            get
            {
                return System.Web.HttpContext.Current.Application[Container.ApplicationLocalizationKeyName] as Dictionary<string, Dictionary<string, string>>;
            }
            set
            {
                System.Web.HttpContext.Current.Application[Container.ApplicationLocalizationKeyName] = value;
            }
        }
        #endregion

        #region Public Properties
        public static string CurrentLanguageCode
        {
            get
            {
                return System.Web.HttpContext.Current.Session[Container.SessionCurrentLanguageCodeKeyName] as string;
            }
            set
            {
                System.Web.HttpContext.Current.Session[Container.SessionCurrentLanguageCodeKeyName] = value;
            }
        }
        #endregion

        #region Public Static Methods
        public static void AddLocalizationSet(string languageCode, Dictionary<string, string> list)
        {
            if (string.IsNullOrEmpty(languageCode)) throw new ArgumentException("languageCode is null or empty");
            if (list == null) throw new ArgumentNullException("list");
            if (list.Count == 0) throw new ArgumentException("list cannot be empty");

            if (Container.LocationContainer == null)
            {
                Container.LocationContainer = new Dictionary<string, Dictionary<string, string>>();
            }
            if (Container.LocationContainer.ContainsKey(languageCode))
            {
                foreach (KeyValuePair<string, string> item in list)
                {
                    Container.LocationContainer[languageCode].Add(item.Key, item.Value);
                }
            }
            else
            {
                Container.LocationContainer.Add(languageCode, list);
            }
        }
        public static void UpdateLocalizationSet(string languageCode, Dictionary<string, string> list)
        {
            Container.Clear(languageCode);
            Container.AddLocalizationSet(languageCode, list);
        }
        public static void Clear()
        {
            Container.LocationContainer = new Dictionary<string, Dictionary<string, string>>();
        }
        public static void Clear(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode)) throw new ArgumentException("languageCode is null or empty");
            if (!Container.LocationContainer.ContainsKey(languageCode)) throw new ArgumentException(languageCode + " not found in location container");

            Container.LocationContainer.Remove(languageCode);
        }
        public static string Get(string languageCode, string key, string defaultValue = "N/A")
        {
            if (string.IsNullOrEmpty(languageCode)) throw new ArgumentException("languageCode is null or empty");
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key is null or empty");

            if (Container.LocationContainer.ContainsKey(languageCode) && Container.LocationContainer[languageCode].ContainsKey(key))
            {
                return Container.LocationContainer[languageCode][key];
            }
            else
            {
                return defaultValue;
            }
        }
        public static string Get(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key is null or empty");

            if (Container.LocationContainer.ContainsKey(Container.CurrentLanguageCode) && Container.LocationContainer[Container.CurrentLanguageCode].ContainsKey(key))
            {
                return Container.LocationContainer[Container.CurrentLanguageCode][key];
            }

            throw new ArgumentException(key + " not found in " + Container.CurrentLanguageCode);
        }

        #endregion
    }
}
