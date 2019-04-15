using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Models
{
    public class SCArticleVM : CustomItem
    {
        public SCArticleVM(Item item)
          : base(item) { }


        public string MetaTitle
        {
            get { return InnerItem["Meta Title"]; }
        }
        public string MetaDescription
        {
            get { return InnerItem["Meta Description"]; }
        }

        //public HtmlString Image
        //{
        //    get
        //    {
        //        return new HtmlString(FieldRenderer.Render(InnerItem, "ArticleImage"));
        //    }
        //}

        public string ArticleRedirect
        {
            get
            {
                Item linkItem = Sitecore.Context.Database.GetItem(InnerItem["CarouselLinkItem"]);
                if (linkItem != null)
                    return LinkManager.GetItemUrl(linkItem);
                return "";
            }
        }
    }
}