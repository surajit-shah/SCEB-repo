using EBPOC.Web.Models;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data.Fields;
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
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
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

        public ActionResult SCCarousel()
        {
            List<CarouselSlide> slides = new List<CarouselSlide>();

            MultilistField multilistField = Sitecore.Context.Item.Fields["PageCarouselSlides"];
            if (multilistField != null)
            {
                Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in carouselItems)
                {
                    slides.Add(new CarouselSlide(item));
                }
            }
            return PartialView(slides);
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
