using EBPOC.Web.Helpers;
using EBPOC.Web.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
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
            List<SCProfileFieldVM> profileFields = new List<SCProfileFieldVM>();
            //MultilistField multilistField = Sitecore.Context.Database.GetItem("{E8AB9719-69BB-4912-9DA4-55C8995E0A7A}").Fields["Categories"];
            Item[] multilistField = SiteHelper.ProductItems("/sitecore/content/EmployeeBenefits/ContactProfiles/ProfileFields").Children.ToArray();
            if (multilistField != null)
            {
                //Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in multilistField)
                {
                    if ((item != null) && (item.Name.Contains(categoryId.Replace(" ", ""))))
                    {
                        profileFields.Add(new SCProfileFieldVM(item));
                    }
                }
            }
            contact.ContactProfileFields = profileFields;
            return PartialView("SCContactForm", contact);
        }
        [HttpPost]
        public ActionResult SCSubmitContactProfile(FormCollection formCollection)
        {
            //var profileData0 = Sitecore.Context.Database.GetItem("{62718D33-41F7-47F2-984D-67B865374408}");
            //SCProfileFieldVM profileData = new SCProfileFieldVM(profileData0);
            //List<SCProfileFieldVM> profileDataList = new List<SCProfileFieldVM>();
            var selectedProfile = formCollection["SelectedProfileId"].ToString();
            var profileDataList = new List<KeyValuePair<string, string>>();
            foreach (string key in formCollection.Keys)
            {
                if (key.Contains(selectedProfile.Replace(" ", "")))
                {
                    profileDataList.Add(new KeyValuePair<string, string>(key, Convert.ToString(formCollection[key])));
                    //profileData.FieldName = key;
                    ////profileData.FieldType = Convert.ToString(formCollection[""]);
                    //profileData.FieldValues = Convert.ToString(formCollection[key]);
                    ////profileData.FieldValues = Convert.ToString(formCollection[""]);
                    ////profileData.FieldValues = Convert.ToString(formCollection[""]);
                    //profileDataList.Add(profileData);
                }
            }

            CreateProfileData(selectedProfile.Replace(" ", ""), profileDataList);

            //return View();

            var url = LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem("{BDDFF05D-F7E8-468C-8A57-6933A9CD94FC}"));
            return Redirect(url);
        }
        private void CreateProfileData(string profile, List<KeyValuePair<string, string>> profileDataList)
        {
            // The SecurityDisabler is required which will overrides the current security model, allowing the code
            // to access the item without any security.
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                Item newItem = null;
                // Get the master database
                Sitecore.Data.Database master = Sitecore.Data.Database.GetDatabase("master");
                // Get the template for which you need to create item
                TemplateItem template = master.GetItem("/sitecore/templates/SCEBTemplates/Forms/ContactProfileData");

                // Get the place in the site tree where the new item must be inserted
                Item parentItem = master.GetItem("/sitecore/content/EmployeeBenefits/ContactProfiles/ProfileData");
                try
                {
                    var dataCount = 0;
                    if (parentItem.HasChildren)
                    {
                        dataCount = parentItem.Children.Count;
                    }
                    // Assign values to the fields of the new item
                    foreach (var profileData in profileDataList)
                    {
                        dataCount = dataCount + 1;
                        // Add the item to the site tree
                        newItem = parentItem.Add("ProfileData"+Convert.ToString(dataCount), template);

                        // Set the new item in editing mode
                        // Fields can only be updated when in editing mode
                        // (It's like the begin transaction on a database)
                        newItem.Editing.BeginEdit();
                        newItem.Fields["ProfileDataId"].Value = Convert.ToString(dataCount); ;
                        newItem.Fields["ContactProfile"].Value = profile;
                        newItem.Fields["ProfileField"].Value = profileData.Key;
                        newItem.Fields["ProfileValue"].Value = profileData.Value;
                        newItem.Fields["IsActive"].Value = Convert.ToString(1);

                        // End editing will write the new values back to the Sitecore
                        // database (It's like commit transaction of a database)
                        newItem.Editing.EndEdit();
                    }
                }
                catch (System.Exception ex)
                {
                    // Log the message on any failure to sitecore log
                    Sitecore.Diagnostics.Log.Error("Could not update item " + newItem.Paths.FullPath + ": " + ex.Message, this);

                    // Cancel the edit (not really needed, as Sitecore automatically aborts
                    // the transaction on exceptions, but it wont hurt your code)
                    newItem.Editing.CancelEdit();
                }
            }
        }
    }
}