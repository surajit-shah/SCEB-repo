using EBPOC.Web.Models;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SCContactController : Controller
    {
        // GET: Default
        public ActionResult SCContact()
        {
            return View();
        }
        // POST: Default
        [HttpPost]
        public ActionResult SCContact(SCContactVM carViewModel)
        {
            // Deal with form post here

            // Do what base.Index() eventually does
            IView pageView = PageContext.Current.PageView;
            if (pageView == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                return (ActionResult)this.View(pageView);
            }
            //return View();
        }
        // POST: Default
        [HttpPost]
        public ActionResult GetContactProfileFields(string categoryId)
        {
            var lookupId = int.Parse(categoryId);
            var model = this.GetFullAndPartialViewModel(lookupId);
            return PartialView("SCContactForm", model);
        }
        private List<SCProfileFieldVM> GetFullAndPartialViewModel(int categoryId = 0)
        {
            var xxx = new List<SCProfileFieldVM>();
            // populate the viewModel and return it
            return fullAndPartialViewModel;
        }

    }
}