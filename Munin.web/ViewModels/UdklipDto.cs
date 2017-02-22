using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Munin.web.ViewModels
{
    public class UdklipDto
    {
        public int Id { get; set; }
        public string Mappe { get; set; }
        public string Overskrift { get; set; }
        public string Datering { get; set; }
        public long Ticks { get; set; }
        public int ErrorCode { get; set; }       
    }
}