using EBPOC.Web.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SCDialBoxesController : Controller
    {

        public ActionResult SCDialBox()
        {
            List<SCDialBoxVM> dialBoxs = new List<SCDialBoxVM>();
            MultilistField multilistField = Sitecore.Context.Database.GetItem("{AE039C2F-0C6D-4C2B-A388-F61CCF6417A9}").Fields["DialBoxGroupItems"];
            if (multilistField != null)
            {
                Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in carouselItems)
                {
                    dialBoxs.Add(new SCDialBoxVM(item));
                }
            }
            return PartialView(dialBoxs);
        }
    }
}