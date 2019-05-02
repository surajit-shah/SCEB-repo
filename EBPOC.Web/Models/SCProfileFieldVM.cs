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

        public string FieldID
        {
            get { return InnerItem["FieldID"]; }
            set { InnerItem["FieldID"] = value; }
        }
        public string FieldTitle
        {
            get { return InnerItem["FieldTitle"]; }
            set { InnerItem["FieldTitle"] = value; }
        }
        public string FieldLabel
        {
            get { return InnerItem["FieldLabel"]; }
            set { InnerItem["FieldLabel"] = value; }
        }
        public string FieldType
        {
            get { return InnerItem["FieldType"]; }
            set { InnerItem["FieldType"] = value; }
        }
        public string FieldName
        {
            get { return InnerItem["FieldName"]; }
            set { InnerItem["FieldName"] = value; }
        }
        public string FieldRequired
        {
            get { return InnerItem["FieldRequired"]; }
            set { InnerItem["FieldRequired"] = value; }
        }
        public string FieldValues
        {
            get { return InnerItem["FieldValues"]; }
            set { InnerItem["FieldValues"] = value; }
        }
        public string FieldOrder
        {
            get { return InnerItem["FieldOrder"]; }
            set { InnerItem["FieldOrder"] = value; }
        }
        public string FieldProfile
        {
            get { return InnerItem["FieldProfile"]; }
            set { InnerItem["FieldProfile"] = value; }
        }
    }
}