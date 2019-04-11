using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{
    public class SearchResults
    {
        public string SearchString { get; set; }

        public Item Item  {get;set;}

        public string ItemDescription { get; set; }

    }
}