using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Munin.web.ViewModels
{
    public class BiblioteksDto
    {
        public int ID { get; set; }
        public string BogKode { get; set; }
        public string Titel { get; set; }
        public string Forfatter { get; set; }
        public double Udgivet { get; set; }
        public int ErrorCode { get; set; }
    }
}