using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Mvc.Extensions;
using Sitecore.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [HttpPost]
        public ActionResult Search(string SearchStr)
        {
            List<SearchResultItem> results = new List<SearchResultItem>();
            results = CustomSearch(SearchStr);

            SearchResultItem searchResultItem = new SearchResultItem();
            searchResultItem.Name = results.Where(x => x.Language.Contains(Sitecore.Context.Language.Name)).SingleOrDefault().Name;
            //results.Add(searchResultItem);
            Models.SearchResults s = new Models.SearchResults();
            s.SearchString = searchResultItem.Name;
            // s.Item= results.SingleOrDefault().GetItem();
            //s.ItemDescription = results.SingleOrDefault().GetField("Body").ToString();
            return View("SearchResult", s);
        }

        public List<SearchResultItem> CustomSearch(string str)
        {
            string index = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
            using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
            {
                var query = context.GetQueryable<SearchResultItem>().Where(i => i.Path.StartsWith("/sitecore/content/SCEBHome")).Where(x => x.TemplateName == "EBArticleTemplate").Where(x => x.Name.Contains(str));

                return query.ToList();
            }

        }
    }
}