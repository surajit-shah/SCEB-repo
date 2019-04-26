﻿using EBPOC.Web.Helpers;
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
    public class SCContactProfilesController : Controller
    {
        public ActionResult SCContactProfiles()
        {
            var contact = new SCContactVM();
            List<SCContactProfileVM> contactProfiles = new List<SCContactProfileVM>();
            //MultilistField multilistField = Sitecore.Context.Database.GetItem("{E8AB9719-69BB-4912-9DA4-55C8995E0A7A}").Fields["Categories"];
            Item[] multilistField = SiteHelper.ProductItems("/sitecore/content/EmployeeBenefits/ContactProfiles/ProfileList").Children.ToArray();
            if (multilistField != null)
            {
                //Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in multilistField)
                {
                    contactProfiles.Add(new SCContactProfileVM(item));
                }
            }
            contact.ContactProfiles = contactProfiles;
            return PartialView(contact);
        }
        // POST: Default
        [HttpPost]
        public ActionResult GetContactProfileFields(string categoryId)
        {
            //var lookupId = int.Parse(categoryId);
            //var model = this.GetFullAndPartialViewModel(lookupId);

            var contact = new SCContactVM();
            List<SCContactProfileVM> contactProfiles = new List<SCContactProfileVM>();
            //MultilistField multilistField = Sitecore.Context.Database.GetItem("{E8AB9719-69BB-4912-9DA4-55C8995E0A7A}").Fields["Categories"];
            Item[] multilistField = SiteHelper.ProductItems("/sitecore/content/EmployeeBenefits/ContactProfiles/ProfileFields").Children.ToArray();
            if (multilistField != null)
            {
                //Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in multilistField)
                {
                    if (item != null)
                    {
                        contactProfiles.Add(new SCContactProfileVM(item));
                    }
                }
            }
            contact.ContactProfiles = contactProfiles;
            return PartialView("SCContactForm", contact);
        }
        private List<SCProfileFieldVM> GetFullAndPartialViewModel(int categoryId = 0)
        {
            var xxx = new List<SCProfileFieldVM>();
            // populate the viewModel and return it
            return xxx;
        }
    }
}