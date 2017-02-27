using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using Munin.web.Models;

namespace Munin.web.ViewModels
{
    public class BilledeVm
    {
        public Billeder Model { get; set; }

        public ICollection<UISelectItem> MaterialeList { get; set; } = new HashSet<UISelectItem>();
        public ICollection<UISelectItem> JournalList { get; set; } = new HashSet<UISelectItem>();

    }
}