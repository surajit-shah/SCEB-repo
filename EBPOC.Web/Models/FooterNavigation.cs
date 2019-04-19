using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{
    public class FooterNavigation: CustomItem
    {
        public FooterNavigation(Item item)
            : base(item) { }
        public string Title
        {
            get { return InnerItem["Title"]; }
        }

        public string FooterText
        {
            get { return InnerItem["Footer Text"]; }

        }
    }
}