using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Munin.DAL.Models;

namespace Munin.web.ViewModels
{
    public class UdklipVm
    {
        public Udklip Model { get; set; }
        public ICollection<UISelectItem> AvisList { get; set; } = new HashSet<UISelectItem>();
    }
}