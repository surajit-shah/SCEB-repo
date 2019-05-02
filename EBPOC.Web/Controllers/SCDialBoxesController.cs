using EBPOC.Web.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SCDialBoxesController : Controller
    {

        public ActionResult SCDialBox(Sitecore.Data.Items.Item ItemId)
        {
            List<SCDialBoxVM> dialBoxs = new List<SCDialBoxVM>();
            string itemId = ItemId.ID.ToString() == "{809D9D4C-A7A1-4B00-853D-CEDC3CB03FF2}" ? "{AE039C2F-0C6D-4C2B-A388-F61CCF6417A9}" : "{28A78460-ADB6-46B3-9E97-33C4E689150E}";
            MultilistField multilistField = Sitecore.Context.Database.GetItem(itemId).Fields["DialBoxGroupItems"];
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