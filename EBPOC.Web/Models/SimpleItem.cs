using EBPOC.Web.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using System.Collections.Generic;
using System.Linq;

namespace EBPOC.Web.Models
{
    public class SimpleItem : CustomItem
    {
        public SimpleItem(Item item) : base(item)
        {
            Assert.IsNotNull(item, "item");
        }

        public string Title
        {
            get { return InnerItem[FieldId.MetaTitle]; }
        }

        public string MetaDescription
        {
            get { return InnerItem[FieldId.MetaDescription]; }
        }

        public string MetaKeywords
        {
            get { return InnerItem[FieldId.MetaKeywords]; }
        }

        //public string Body
        //{
        //  get { return InnerItem[FieldId.Body]; }
        //}

        //public string ItemIcon
        //{
        //  get { return InnerItem[FieldId.Icon]; }
        //}

        public string Url
        {
            get { return LinkManager.GetItemUrl(InnerItem); }
        }

        //public string SearchDescription
        //{
        //    get { return EBPOC.Web.Helpers.SiteHelper.GetPageDescripton(Item); }
        //}

        public Item Item
        {
            get { return InnerItem; }
        }

        public IEnumerable<SimpleItem> ChildrenInCurrentLanguage
        {
            get
            {
                return InnerItem.Children.Select(x => new SimpleItem(x)).Where(x => SiteConfiguration.DoesItemExistInCurrentLanguage(x.Item));
            }
        }

        public IEnumerable<SimpleItem> Children
        {
            get
            {
                return InnerItem.Children.Select(x => new SimpleItem(x));
            }
        }

        public static class FieldId
        {
            public static readonly ID MetaTitle = new ID("{53E0DAED-D572-4A80-9A29-6BD14E37FC6F}");
            public static readonly ID MetaDescription = new ID("{E9A71581-8AD6-4038-AE75-A659CB472838}");
            public static readonly ID MetaKeywords = new ID("{51137A28-1D85-4300-AE72-058708960A5D}");
            //public static readonly ID Body = new ID("{5A5684BB-8B54-44F6-ABCC-2BADA05ADA5D}");
            //public static readonly ID Icon = new ID("{2B60D8C1-81DB-45A7-B1CB-654CDDA96AE3}");
        }
    }
}