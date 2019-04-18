using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{
    public class SCProfileFieldVM : CustomItem
    {
        public SCProfileFieldVM(Item item)
          : base(item) { }

        public string FieldID { get { return InnerItem["FieldID"]; } }
        public string FieldTitle { get { return InnerItem["FieldTitle"]; } }
        public string FieldLabel { get { return InnerItem["FieldLabel"]; } }
        public string FieldType { get { return InnerItem["FieldType"]; } }
        public string FieldName { get { return InnerItem["FieldName"]; } }
        public string FieldRequired { get { return InnerItem["FieldRequired"]; } }
        public string FieldValues { get { return InnerItem["FieldValues"]; } }
        public string FieldOrder { get { return InnerItem["FieldOrder"]; } }
        public string FieldProfile { get { return InnerItem["FieldProfile"]; } }
    }
}