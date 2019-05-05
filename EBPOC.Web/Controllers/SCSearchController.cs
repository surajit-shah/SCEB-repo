using EBPOC.Web.Configuration.Search.Models;
using EBPOC.Web.Configuration.SiteUI;
using EBPOC.Web.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Fields;
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

            //var pathInfo = LinkManager.GetItemUrl(item, UrlOptions.DefaultOptions);

            //var route = RedirectToRoute(MvcSettings.SitecoreRouteName, new { pathInfo = pathInfo.TrimStart(new char[] { '/' }) });
            //return View(new SearchResults(searchStr, facets));
            //}
            //return null;
            // make product data accessible to all components that need it (more in Part 2)
            this.ShareProductData(new SearchResults(searchStr, facets));
            //ContextService.Get().GetCurrent<ViewContext>().ViewData.Add("_SharedModel", new SearchResults(searchStr, facets));
            var url = LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem("{1C9826B5-5C14-4DC2-9054-861D9C6C6F6B}"));
            return Redirect(url);
        }

        public ActionResult Results()
        {
            if (IsDataSourceItemNull) return null;

            IEnumerable<SimpleItem> items = DataSourceItems.Select(x => new SimpleItem(x)).Where(x => Configuration.SiteConfiguration.DoesItemExistInCurrentLanguage(x.Item));
            SimpleItemList results = new SimpleItemList(DataSourceItem["Meta Title"], items);
            return !items.IsNullOrEmpty() ? View("LinkList", results) : null;

        }
        protected virtual void ShareProductData(SearchResults searchResult)
        {
            ViewBag.SearchResult = searchResult;
            SaveSearchResults(searchResult);
        }
        private void SaveSearchResults(SearchResults searchResult)
        {

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                // Get the master database
                Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");
                // Get the template for which you need to create item
                TemplateItem searchItemTemplate = master.GetItem("/sitecore/templates/SCEBTemplates/Forms/SearchResultData");
                TemplateItem resultItemtemplate = master.GetItem("/sitecore/templates/SCEBTemplates/Forms/ResultItem");

                // Get the place in the site tree where the new item must be inserted
                Item searchItemDataFolder = master.GetItem("{0508A62A-7B83-492D-A21E-F1B6580E26D1}");
                Item searchItemFolder = master.GetItem("{4D5319C6-B7E1-4137-8AC0-EB92637A594D}");
                Item resultItemFolder = master.GetItem("{28304EE5-F79B-4FB6-AE45-9926A63AC41D}");
                Item searchItem = null;
                Item resultItem = null;
                try
                {

                    searchItemFolder.Editing.BeginEdit();
                    searchItemFolder.DeleteChildren();
                    searchItemFolder.Editing.EndEdit();
                    resultItemFolder.Editing.BeginEdit();
                    resultItemFolder.DeleteChildren();
                    resultItemFolder.Editing.EndEdit();
                    //searchItemDataFolder.Editing.BeginEdit();
                    // Add the item to the site tree
                    searchItem = searchItemFolder.Add("SearchItemData", searchItemTemplate);
                    //MultilistField mlf = new MultilistField(newItem.Fields["ResultItems"]);
                    //string resultItems = string.Empty;
                    if (searchResult.Results != null)
                    {
                        searchItem.Editing.BeginEdit();
                        searchItemFolder.DeleteChildren();
                        //newItem.Fields["ResultItems"].Value = resultItem;
                        searchItem.Fields["SearchString"].Value = searchResult.SearchString;
                        searchItem.Fields["SearchItemName"].Value = searchResult.SearchItemName;
                        searchItem.Fields["SearchitemDescription"].Value = searchResult.SearchitemDescription;
                        searchItemFolder.Editing.EndEdit();
                        foreach (var result in searchResult.Results)
                        {
                            // Add the item to the site tree;
                            resultItem = resultItemFolder.Add("ResultItemData", resultItemtemplate);
                            resultItem.Editing.BeginEdit();
                            resultItem.DeleteChildren();
                            resultItem.Fields["Description"].Value =searchResult.SearchitemDescription;
                            resultItem.Fields["Name"].Value = result.Name;
                            resultItem.Fields["Title"].Value = result.Title;
                            resultItem.Fields["Url"].Value = result.Url;
                            resultItem.Fields["MediaUrl"].Value = searchResult.searchItemUrl;
                            resultItem.Editing.EndEdit();
                        }
                    }
                    else
                    {
                        resultItem.Editing.BeginEdit();
                        resultItem.DeleteChildren();
                        resultItem.Fields["Title"].Value = searchResult.SearchString;
                        resultItem.Fields["Name"].Value = searchResult.SearchItemName;
                        resultItem.Fields["Description"].Value = searchResult.SearchitemDescription;
                        resultItem.Editing.EndEdit();


                    }
                    // End editing will write the new values back to the Sitecore
                    // database (It's like commit transaction of a database)
                    //searchItemDataFolder.Editing.EndEdit();
                }
                //}


                catch (System.Exception ex)
                {
                    // Log the message on any failure to sitecore log
                    Sitecore.Diagnostics.Log.Error("Could not update item " + searchItemDataFolder.Paths.FullPath + ": " + ex.Message, this);

                    // Cancel the edit (not really needed, as Sitecore automatically aborts
                    // the transaction on exceptions, but it wont hurt your code)
                    //searchItemDataFolder.Editing.CancelEdit();
                    searchItem.Editing.CancelEdit();
                    resultItem.Editing.CancelEdit();
                }
            }
        }
    }

}

