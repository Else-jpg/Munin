using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Munin.web.Models;

namespace Munin.web.ViewModels
{
    public class BibliotekVm
    {
        public Bibliotek Model { get; set; }
        public ICollection<UISelectItem> JournalList { get; set; } = new HashSet<UISelectItem>();
    }
}