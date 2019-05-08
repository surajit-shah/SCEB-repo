using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{

    public class SCPromotionBoxVM : CustomItem
    {
        public SCPromotionBoxVM(Item item)
          : base(item) { }


        public string PromotionBoxHeader
        {
            get { return InnerItem["PromotionBoxHeader"]; }
        }

        public string PromotionBoxSummery
        {
            get { return InnerItem["PromotionBoxSummery"]; }
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
}