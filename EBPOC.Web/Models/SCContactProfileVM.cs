using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{
    public class SCContactProfileVM : CustomItem
    {
        public SCContactProfileVM(Item item)
          : base(item) { }

        public string ContactProfileName { get { return InnerItem["ContactProfileName"]; } }
        public string ContactProfileValue { get { return InnerItem["ContactProfileValue"]; } }
    }
}