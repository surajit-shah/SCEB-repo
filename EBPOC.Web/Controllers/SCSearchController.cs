using EBPOC.Web.Configuration.Search.Models;
using EBPOC.Web.Configuration.SiteUI;
using EBPOC.Web.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Common;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Sitecore.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SCSearchController : EBPOCSitecoreBaseController
    {
        // GET: Search
        //[HttpPost]
        //public ActionResult Search(string SearchStr)
        //{

        //    if (SearchStr == null) return View("Error");
        //    //if (SearchStr != null)
        //    //{
        //        List<Item> ResultsList = new List<Item>();

        //        string indexname = "sitecore_master_index";
        //        if (Sitecore.Context.PageMode.IsNormal || Sitecore.Context.PageMode.IsDebugging) indexname = "sitecore_web_index";

        //        using (var context = ContentSearchManager.GetIndex(indexname).CreateSearchContext())
        //        {
        //            // Start the search query building
        //            var query = context.GetQueryable<SitecoreItem>().Where(item => item.Path.StartsWith("/sitecore/content/EmployeeBenefits")).Where(x => x.TemplateName == "EBArticleTemplate");

        //            // we will split the spaces and require all words to be in the index.
        //            foreach (string word in SearchStr.Split(' '))
        //            {
        //                query = query.Where(item => item.Title.Contains(word) || item.Content.Contains(word));
        //            }


        //            var results = query
        //               .Filter(item => item.Language == Sitecore.Context.Language.Name).GetResults();

        //            SearchResults searchResults = new SearchResults();
        //            searchResults.Results = new List<SimpleItem>();
        //            searchResults.SearchString = SearchStr;
        //            foreach (SearchHit<SitecoreItem> result in results.Hits)
        //            {
        //                searchResults.Results.Add(new SimpleItem(result.Document.GetItem()));
        //            }

        //            return View("Search", searchResults);




        //        }
        [HttpGet]
        public ActionResult Search(string searchStr, string tag)
        {
            return View(new SearchResults("*", new string[] { String.Format("{0}|{1}", EBPOC.Web.Helpers.SiteHelper.GetDictionaryText("Tags"), tag) }));
        }

        [HttpPost]
        public ActionResult Search(string searchStr, string[] facets)
        {
            //string DataSourceId = RenderingContext.Current.Rendering.DataSource;
            //if (DataSourceId != null)
            //{
                Item item = Sitecore.Context.Database.GetItem(Sitecore.Data.ID.Parse("{07F9D462-768E-4126-ADB0-AFB0C2567B98}"));

                var pathInfo = LinkManager.GetItemUrl(item, UrlOptions.DefaultOptions);

               var route= RedirectToRoute(MvcSettings.SitecoreRouteName, new { pathInfo = pathInfo.TrimStart(new char[] { '/' }) });
                return View(new SearchResults(searchStr, facets));
            //}


            //return null;
            
           // ContextService.Get().GetCurrent<ViewContext>().ViewData.Add("_SharedModel", new SearchResults(searchStr, facets));
            //var url = LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem("{07F9D462-768E-4126-ADB0-AFB0C2567B98}"));
            //return Redirect(url);
        }

        public ActionResult Results()
        {
            if (IsDataSourceItemNull) return null;

            IEnumerable<SimpleItem> items = DataSourceItems.Select(x => new SimpleItem(x)).Where(x => Configuration.SiteConfiguration.DoesItemExistInCurrentLanguage(x.Item));
            SimpleItemList results = new SimpleItemList(DataSourceItem["Meta Title"], items);
            return !items.IsNullOrEmpty() ? View("LinkList", results) : null;

        }

    }

}

