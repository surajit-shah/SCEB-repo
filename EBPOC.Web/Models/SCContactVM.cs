using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBPOC.Web.Models
{
    public class SCContactVM
    {
        public string SelectedProfileId { get; set; }
        public IEnumerable<SCContactProfileVM> ContactProfiles { get; set; }
        public List<SCProfileFieldVM> ContactProfileFields { get; set; }
    }
}