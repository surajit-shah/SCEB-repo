using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace EBPOC.Web.Helpers
{
    public static class SiteHelper
    {
        public static Item HomeItem()
        {
            string homePath = Sitecore.Context.Site.StartPath;
            return Sitecore.Context.Database.GetItem(homePath);
        }

        public static Item ProductItem()
        {
            string productPath = "/sitecore/content/SCEBHome";
            return Sitecore.Context.Database.GetItem(productPath);
        }
        public static string GetMediaUrl(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldName)
        {
            return GetMediaUrl(sitecoreHelper, fieldName, sitecoreHelper.CurrentItem);
        }

        public static string GetMediaUrl(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldName, Item item)
        {
            ImageField imageField = item.Fields[fieldName];
            return Sitecore.Resources.Media.MediaManager.GetMediaUrl(imageField.MediaItem);
        }

        public static bool DoesItemExistInCurrentLanguage(Item i)
        {
            if (i.Versions.Count != 0) return true;

            return false;
        }
        public static string GetDictionaryText(string key)
        {
            return Sitecore.Globalization.Translate.Text(key);
        }
    }
}