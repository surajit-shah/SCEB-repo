using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{

    public class SCLanguageVM
    {
        private string languageName;
        private string languageUrl;
        private string languageIsSelected;
        public string LanguageName
        {
            get
            {
                return languageName;
            }
            set
            {
                languageName = value;
            }
        }
        public string LanguageUrl
        {
            get
            {
                return languageUrl;
            }
            set
            {
                languageUrl = value;
            }
        }
        public string LanguageIsSelected
        {
            get
            {
                return languageIsSelected;
            }
            set
            {
                languageIsSelected = value;
            }
        }
    }
}