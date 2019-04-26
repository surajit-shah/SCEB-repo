using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{

    public class SCNavigationBoxVM : CustomItem
    {
        public SCNavigationBoxVM(Item item)
          : base(item) { }


        public string NavigationBoxHeader
        {
            get { return InnerItem["NavigationBoxHeader"]; }
        }

        public string NavigationBoxSummery
        {
            get { return InnerItem["NavigationBoxSummery"]; }
        }
        private List<SCNavigationVM> _navigationList;
        public List<SCNavigationVM> NavigationList
        {
            get { return _navigationList; }
            set { _navigationList = value; }
        }

        //public HtmlString Image
        //{
        //    get
        //    {
        //        return new HtmlString(FieldRenderer.Render(InnerItem, "NavigationBoxImage"));
        //    }
        //}
    }
    public class SCNavigationVM : CustomItem
    {
        public SCNavigationVM(Item item)
          : base(item) { }


        public string NavigationText
        {
            get { return InnerItem["NavigationText"]; }
        }
        public string NavigationAltText
        {
            get { return InnerItem["NavigationAltText"]; }
        }

        public HtmlString NavigationBGImage
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(InnerItem, "NavigationBGImage"));
            }
        }

        public string NavigationRedirect
        {
            get
            {
                Item linkItem = Sitecore.Context.Database.GetItem(InnerItem["NavigationRedirect"]);
                if (linkItem != null)
                    return LinkManager.GetItemUrl(linkItem);
                return "";
            }
        }
    }
}