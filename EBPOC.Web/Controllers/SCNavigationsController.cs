using EBPOC.Web.Models;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
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

        public ActionResult Breadcrumbs()
        {
            if (Sitecore.Context.Item.ID != EBPOC.Web.Helpers.SiteHelper.GetHomeItem().ID)
            {
                List<SimpleItem> items = new List<SimpleItem>();
                Item temp = Sitecore.Context.Item;

                while (temp.ID != EBPOC.Web.Helpers.SiteHelper.GetHomeItem().ParentID)
                {
                    items.Add(new SimpleItem(temp));
                    temp = temp.Parent;
                }

                items.Reverse();
                return View(items);
            }
            return null;
        }

        public ActionResult FooterNavigation()
        {
            List<FooterNavigation> items = new List<FooterNavigation>();
            Item homeItem = EBPOC.Web.Helpers.SiteHelper.GetHomeItem();
            if (homeItem != null)
            {
                if (homeItem["Show in Footer Menu"] == "1") items.Add(new FooterNavigation(homeItem));
                foreach (Item i in homeItem.Axes.GetDescendants().Where(x => x["Show in Footer Menu"] == "1"))
                {
                    items.Add(new FooterNavigation(i));
                }
            }
            return items.Count > 0 ? View(items) : null;
        }

        public ActionResult SCNavigationBoxList()
        {
            List<SCNavigationBoxVM> navboxlist = new List<SCNavigationBoxVM>();
            var navboxitem = Sitecore.Context.Database.GetItem("{85DEE9E5-DF7A-4D61-9D8C-5D9815086CF2}");
            SCNavigationBoxVM navigationBoxVM = new SCNavigationBoxVM(navboxitem);
            List<SCNavigationVM> articles = new List<SCNavigationVM>();
            MultilistField multilistField = Sitecore.Context.Database.GetItem("{85DEE9E5-DF7A-4D61-9D8C-5D9815086CF2}").Fields["NavigationList"];
            if (multilistField != null)
            {
                Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in carouselItems)
                {
                    articles.Add(new SCNavigationVM(item));
                }
            }
            navigationBoxVM.NavigationList = articles;
            navboxlist.Add(navigationBoxVM);
            return PartialView(navboxlist);
        }
    }
}
