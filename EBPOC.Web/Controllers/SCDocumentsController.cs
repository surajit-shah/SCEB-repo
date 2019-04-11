using EBPOC.Web.Models;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBPOC.Web.Controllers
{
    public class SCDocumentsController : Controller
    {
        // GET: SCDocuments
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SCDocumentList()
        {
            List<DocumentDetails> lstStandardizedSolutions = new List<DocumentDetails>();
            string productPath = "/sitecore/Media Library/Files/SCEBFiles/EBStandardizedSolutions";
            ChildList fileItems = Sitecore.Context.Database.GetItem(productPath).Children;
            //var itemId = Sitecore.Context.Database.GetItem(productPath).ID;
            //////var itemId = Sitecore.Context.Database.GetItem(RenderingContext.Current.Rendering.DataSource).ID;
            //Sitecore.Data.Items.MediaItem itemMedia = new Sitecore.Data.Items.MediaItem(Sitecore.Context.Database.GetItem(itemId));
            //var fileType = xmlMedia.Fields["Extension"].Value;
            //if (fileType == "xml" || fileType == "XML")
            //{
            //    //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    var stream = MediaManager.GetMedia(xmlMedia).GetStream().Stream;
            //    using (StreamReader r = new StreamReader(stream))
            //    {
            //       var myData = r.ReadToEnd();
            //    }
            //}
            //MultilistField msf = Sitecore.Context.Item.Fields[""];
            //MediaManager.GetMedia("/sitecore/media library/Files/SCEBFiles/EBStandardizedSolutions");
            string pdfData = string.Empty;
            foreach (var imgItem in fileItems)
            {
                //Sitecore.Data.Items.Item sampleItem = Sitecore.Context.Database.GetItem("/sitecore/media library/Files/yourhtmlfile");
                Sitecore.Data.Items.Item sampleMedia = (Sitecore.Data.Items.Item)imgItem;
                Media xxx = MediaManager.GetMedia(sampleMedia);
                var fileType = sampleMedia.Fields["Extension"].Value;
                if (fileType == "pdf" || fileType == "PDF")
                {
                    using (var reader = new StreamReader(xxx.GetStream().Stream))
                    {
                        pdfData = reader.ReadToEnd();
                    }
                }

                var documentDetails = new DocumentDetails();
                //documentDetails.Scource = HashingUtils.ProtectAssetUrl(Sitecore.StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(imgItem)));
                //Sitecore.Data.Items.MediaItem itemMedia = new Sitecore.Data.Items.MediaItem(Sitecore.Context.Database.GetItem(itemId));
                documentDetails.Type = fileType;
                documentDetails.Name = xxx.MediaData.MediaItem.Title;
                documentDetails.Image = xxx.MediaData.MediaItem.Name;
                documentDetails.Scource = xxx.MediaData.MediaItem.Path;
                lstStandardizedSolutions.Add(documentDetails);
            }
            //LanguageCollection languageCollection = LanguageManager.GetLanguages(Context.Database);
            //foreach (Language language in languageCollection)
            //{
            //    string url = GetItemUrl(Context.Item, language);
            //    list.Add(language.CultureInfo.DisplayName, url);

            //}

            return View(lstStandardizedSolutions);
        }
    }
}