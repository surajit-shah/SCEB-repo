using Sitecore;
using Sitecore.Data;
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
    public class ResultData : CustomItem
    {
        public ResultData(Item item)
          : base(item) { }

        public string ItemName
        {
            get { return InnerItem[FieldId.ItemName]; }

        }

        public string Description
        {
            get { return InnerItem[FieldId.Description]; }
        }

        public string Title
        {
            get { return InnerItem[FieldId.Title]; }
        }

        public string Url
        {
            get { return InnerItem[FieldId.Url]; }
        }

        public string MediaUrl
        {
            get { return InnerItem[FieldId.MediaUrl]; }
        }
        public Item Item
        {
            get { return InnerItem; }
        }

       
        public static class FieldId
        {
            public static readonly ID ItemName = new ID("{F3DA4331-1D46-4ED6-B060-3277EA0D300D}");
            public static readonly ID Description = new ID("{2AB9E70D-2EB0-494E-8659-A199C0CC45A3}");
            public static readonly ID Title = new ID("{3720DB8C-6B9F-40C4-8061-0AFB12DDFBAD}");
            public static readonly ID Url = new ID("{136EFE3E-8F2E-4A87-B09D-16462CCE94CC}");
            public static readonly ID MediaUrl = new ID("{15427A3F-AF4D-43C7-A6EB-C0714FC37414}");
            //public static readonly ID Icon = new ID("{2B60D8C1-81DB-45A7-B1CB-654CDDA96AE3}");
        }
    }

}
