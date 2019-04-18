using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Models
{
    public class SCDialBoxVM : CustomItem
    {
        public SCDialBoxVM(Item item)
          : base(item) { }


        public string DialBoxText
        {
            get { return InnerItem["DialBoxText"]; }
        }
        public string DialBoxBGImage
        {
            get
            {
                return GetImageUrl();
            }
        }

        //public HtmlString DialBoxBGImage
        //{
        //    get
        //    {
        //        return new HtmlString(FieldRenderer.Render(InnerItem, "DialBoxBGImage"));
        //    }
        //}

        //public string DialBoxRedirect
        //{
        //    get
        //    {
        //        Item linkItem = Sitecore.Context.Database.GetItem(InnerItem["CarouselLinkItem"]);
        //        if (linkItem != null)
        //            return LinkManager.GetItemUrl(linkItem);
        //        return "";
        //    }
        //}
        private string GetImageUrl()
        {
            var currentItem = Sitecore.Context.Item;
            var imageUrl = string.Empty;

            Sitecore.Data.Fields.ImageField imageField = InnerItem.Fields["DialBoxBGImage"];
            if (imageField?.MediaItem != null)
            {
                var image = new MediaItem(imageField.MediaItem);
                imageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
            }
            return imageUrl;
        }
    }
}