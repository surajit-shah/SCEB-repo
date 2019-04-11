using Sitecore;
using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SCNavigationsController : Controller
    {
        // GET: Navigation
        public ActionResult SCLanguageSwitcher()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            LanguageCollection languageCollection = LanguageManager.GetLanguages(Context.Database);
            foreach (Language language in languageCollection)
            {
                string url = GetItemUrl(Context.Item, language);
                list.Add(language.CultureInfo.DisplayName, url);

            }

            return View(list);
        }

        private static string GetItemUrl(Item item, Language language)
        {
            //bool isLanguageAvailable = false;
            //if(Helpers.SiteHelper.GetLanguages().Contains(Sitecore.Context.Language.))
            string url = LinkManager.GetItemUrl(item,
                new UrlOptions
                {
                    LanguageEmbedding = LanguageEmbedding.Always,
                    LanguageLocation = LanguageLocation.FilePath,
                    Language = language
                }
            );
            return url;
        }
    }
}