using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Munin.web.ViewModels
{
    public class UdklipViewModel
    {
        public int Count { get; set; }

        public int Pages { get; set; }
        
        public ICollection<UdklipDto> Data { get; set; } = new HashSet<UdklipDto>();
    }
}