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
    public class SCArticlesController : Controller
    {

        public ActionResult SCContentBox()
        {
            List<SCArticleVM> articles = new List<SCArticleVM>();
            MultilistField multilistField = Sitecore.Context.Database.GetItem("{E8AB9719-69BB-4912-9DA4-55C8995E0A7A}").Fields["Categories"];
            if (multilistField != null)
            {
                Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in carouselItems)
                {
                    articles.Add(new SCArticleVM(item));
                }
            }
            return PartialView(articles);
        }
    }
}