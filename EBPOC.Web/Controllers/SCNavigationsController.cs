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
            var currentItem = RenderingContext.Current.Rendering.Item;
            List<SCLanguageVM> list = new List<SCLanguageVM>();
            //Dictionary<string, string> list = new Dictionary<string, string>();
            LanguageCollection languageCollection = LanguageManager.GetLanguages(Context.Database);
            foreach (Language language in languageCollection)
            {
                SCLanguageVM langObject = new SCLanguageVM();
                string url = GetItemUrl(Context.Item, language);
                //list.Add(language.CultureInfo.DisplayName, url);
                langObject.LanguageName = language.CultureInfo.DisplayName;
                langObject.LanguageUrl = url;
                if (currentItem.Language.Name.ToUpper() == "EN")
                {
                    langObject.LanguageIsSelected = currentItem.Language.Name;
                }
                else
                {
                    langObject.LanguageIsSelected = currentItem.Language.Name.Substring(currentItem.Language.Name.Length - 2);
                }
                list.Add(langObject);
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
            ChildList navBoxItems = Sitecore.Context.Database.GetItem("{2BDE8942-7089-46D4-A959-32A1C709B566}").Children;
            foreach (var navBox in navBoxItems)
            {
                Sitecore.Data.Items.Item navBoxItem = (Sitecore.Data.Items.Item)navBox;
                var navigationBoxVM = new SCNavigationBoxVM(navBoxItem);
                //var navboxitem = Sitecore.Context.Database.GetItem("{568325BB-0387-4FC2-90B5-6DE79965FDCF}");
                //SCNavigationBoxVM navigationBoxVM = new SCNavigationBoxVM(navboxitem);
                List<SCNavigationVM> navList = new List<SCNavigationVM>();
                MultilistField multilistField = Sitecore.Context.Database.GetItem(navBoxItem.ID.ToString()).Fields["NavigationList"];
                if (multilistField != null)
                {
                    Item[] navItems = multilistField.GetItems();
                    foreach (Item item in navItems)
                    {
                        navList.Add(new SCNavigationVM(item));
                    }
                }
                navigationBoxVM.NavigationList = navList;
                navboxlist.Add(navigationBoxVM);
            }
            return PartialView(navboxlist);
        }

        public ActionResult SCPromotionBox()
        {
            List<SCNavigationVM> navList = new List<SCNavigationVM>();
            MultilistField multilistField = Sitecore.Context.Database.GetItem("{568325BB-0387-4FC2-90B5-6DE79965FDCF}").Fields["NavigationList"];
            //ChildList linkItems = Sitecore.Context.Database.GetItem("{82D6DC6C-3264-405B-9A8A-84540B0178C1}").Children;
            //foreach (var linkItem in linkItems)
            //{
            //    Sitecore.Data.Items.Item sampleMedia = (Sitecore.Data.Items.Item)linkItem;
            //    navList.Add(new SCNavigationVM(sampleMedia));
            //}
            if (multilistField != null)
            {
                Item[] fieldItems = multilistField.GetItems();
                foreach (Item item in fieldItems)
                {
                    navList.Add(new SCNavigationVM(item));
                }
            }

            return PartialView(navList);
        }
    }
}
