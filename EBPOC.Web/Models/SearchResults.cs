using EBPOC.Web.Configuration;
using EBPOC.Web.Configuration.Search.Models;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Masters;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Xml.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;

namespace EBPOC.Web.Models
{
    // search result temp comment
    public class SearchResults
    {
        public string SearchString { get; set; }

        public string SearchItemName { get; set; }

        public string SearchitemDescription { get; set; }

        public string SearchItemTitle { get; set; }

        public string searchItemUrl { get; set; }

        public string mediaItemPath { get; set; }


        public string[] SelectedFacets
        {
            get { return null; }
        }

        public List<SimpleItem> Results { get; set; }

        public List<ResultItems> resultItems { get; set; }

        public List<Facet> Facets { get; set; }

        public bool isSearchReturningItem { get; set; }

        public SearchResults(string searchStr, string[] facets)
        {
            try
            {
                isSearchReturningItem = false;
                // if we don't have a searchStr for some reason...
                //if (searchStr == string.Empty) searchStr = "*";

                Item folderTemplateId = Sitecore.Context.Database.GetItem("{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}");
                Item profileDataTemplateId = Sitecore.Context.Database.GetItem("{E4BAA41C-C350-40E7-9795-F1E937D8A792}");
                Item profileFieldsTemplateId = Sitecore.Context.Database.GetItem("{7B2FBB10-1448-466C-82AB-9D6359BE091C}");
                Item profileListTemplateId = Sitecore.Context.Database.GetItem("{EE5273A1-8E36-4A36-8CE7-FDF98B0D0157}");
                Item contactFieldTemplateId = Sitecore.Context.Database.GetItem("{08C77347-B0CF-48DF-B9CD-7F33E5A7FBC0}");
                Item medialibrarytemplateId = Sitecore.Context.Database.GetItem("{CC80011D-8EAE-4BFC-84F1-67ECD0223E9E}");
                List<Item> ResultsList = new List<Item>();

                string indexname = "sitecore_master_index";
                if (Sitecore.Context.PageMode.IsNormal || Sitecore.Context.PageMode.IsDebugging) indexname = "sitecore_web_index";

                using (var context = ContentSearchManager.GetIndex(indexname).CreateSearchContext())
                {
                    //generic search
                    var query = context.GetQueryable<SitecoreItem>().Where(item => item.Path.StartsWith("/sitecore/content/EmployeeBenefits")).
                        Where(item => item.TemplateId != folderTemplateId.ID).
                        Where(item => item.TemplateId != profileDataTemplateId.ID).
                        Where(item => item.TemplateId != profileFieldsTemplateId.ID)
                       .Where(item => item.TemplateId != profileListTemplateId.ID).
                       Where(item=>item.TemplateId!=contactFieldTemplateId.ID);

                    //var query1=context.GetQueryable<Item>().Where(item=>item.templa)

                    // we will split the spaces and require all words to be in the index.
                    foreach (string word in searchStr.Split(' '))
                    {
                        query = query.Where(item => item.Title.Contains(word) || item.Content.Contains(word));
                    }

                    if (facets != null)
                    {
                        foreach (string facet in facets)
                        {
                            if (facet.Contains("|"))
                            {
                                string[] values = facet.Split('|');
                                string val = values[1];
                                if (values[0] == SiteConfiguration.GetDictionaryText("Type")) { query = query.Where(item => item.TemplateName.Equals(val)); }
                                if (values[0] == SiteConfiguration.GetDictionaryText("Tags")) { query = query.Where(item => item.Tags.Equals(val)); }
                            }
                        }
                    }

                   // var folderTemplateId=Sitecore.Data.Database.GetItem()
                    var results = query
                      .Filter(item => item.Language == Sitecore.Context.Language.Name)//.Filter(item=>item.TemplateId!=Sitecore.Data.Database.GetItem("{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}"))
                      //.Filter(item => item.HasPresentation)
                      //.Filter(item => item.ShowInSearchResults)
                      //.FacetOn(item => item.Tags).
                      //.FacetOn(item => item.ArticleGroupName)
                      //.FacetOn(item => item.ArticleGroupName)
                      .GetResults();

                    if (results.Count() > 0)
                    {
                        
                        this.Results = new List<SimpleItem>();
                        this.Facets = new List<Facet>();

                        this.SearchString = searchStr;
                        this.isSearchReturningItem = true;

                        foreach (SearchHit<SitecoreItem> result in results.Hits)
                        {

                            this.Results.Add(new SimpleItem(result.Document.GetItem()));
                            if (result.Document.Name != null && result.Document.GetItem().TemplateID!= medialibrarytemplateId.ID)
                            {
                                this.SearchItemName = result.Document.GetItem().Fields["Title"].ToString();
                                this.SearchitemDescription = result.Document.GetItem().Fields["Body"].ToString();
                                // this.SearchitemDescription = result.Document.GetItem().Fields["Description"].ToString();
                                this.searchItemUrl = Sitecore.Links.LinkManager.GetItemUrl(result.Document.GetItem());
                            }
                            else if (result.Document.GetItem().Fields["Extension"].Value == "pdf" || (result.Document.GetItem().Fields["Extension"].Value == "PDF"))
                            {
                                Media media = MediaManager.GetMedia(result.Document.GetItem());
                                this.SearchItemTitle = media.MediaData.MediaItem.Title;
                                this.SearchItemName = media.MediaData.MediaItem.Name;
                                this.SearchitemDescription = media.MediaData.MediaItem.Description;
                                this.mediaItemPath = media.MediaData.MediaItem.Path;
                                this.searchItemUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(result.Document.GetItem()));
                                                              
                            }

                            else
                            {
                                this.SearchItemName = "Item not Found";
                                this.SearchitemDescription = "Item not Found";
                            }

                        }

                        foreach (FacetCategory fc in results.Facets.Categories)
                        {
                            Facet f = new Facet();
                            f.Items = new List<FacetItem>();
                            //if(fc.Name==)
                            if (fc.Name == "_templatename")
                            {
                                f.FacetName = SiteConfiguration.GetDictionaryText("Type");
                                foreach (var a in fc.Values)
                                {
                                    f.Items.Add(new FacetItem(a.Name, String.Format("{0} ({1})", a.Name, a.AggregateCount), false));
                                }
                            }

                            if (fc.Name == "__semantics")
                            {
                                f.FacetName = SiteConfiguration.GetDictionaryText("Tags");
                                foreach (var a in fc.Values)
                                {
                                    Item tag = Sitecore.Context.Database.GetItem(new ID(a.Name));
                                    f.Items.Add(new FacetItem(a.Name, String.Format("{0} ({1})", tag.Name, a.AggregateCount), false));
                                }
                            }

                            // set the correct facet entries to selected
                            if (facets != null)
                            {
                                foreach (string facet in facets)
                                {
                                    if (facet.Contains("|"))
                                    {
                                        string[] values = facet.Split('|');
                                        if (values[0] == f.FacetName)
                                        {
                                            foreach (FacetItem fi in f.Items) { if (fi.Id == values[1]) fi.Selected = true; }
                                        }
                                    }
                                }
                            }

                            if (f.Items.Count > 0) this.Facets.Add(f);
                        }
                    }

                    else
                    {
                        this.isSearchReturningItem = false;
                        this.SearchItemName = "Sorry!! No results Found";
                        this.SearchitemDescription = searchStr + "is not Found";


                    }
                }


            }
            catch (Exception ex)
            {

                Sitecore.Diagnostics.Log.Info("Exception occured in SearchResults Page", ex.Message);

            }

            finally
            {
                this.SearchItemTitle = "Some Error Occured";


            }
        }
    }

    public class Facet
    {
        public Facet() { }
        public string FacetName { get; set; }
        public List<FacetItem> Items { get; set; }
    }

    public class FacetItem
    {
        public FacetItem() { }
        public FacetItem(string id, string name, bool selected)
        {
            Id = id;
            Name = name;
            Selected = selected;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}

