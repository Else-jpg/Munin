using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Munin.web.ViewModels
{
    public class BillederDto
    {
        public int Id { get; set; }

        public string Note { get; set; }

        public string Datering { get; set; }

        public long TicksDatering { get; set; }

        public string BilledIndex { get; set; }

        public int ErrorCode { get; set; }
        
    }
}