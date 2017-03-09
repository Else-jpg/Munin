using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Munin.web.Controllers
{
    public class MuninListViewModel<T>
    {
        public int Count { get; set; }

        public int Pages { get; set; }

        public ICollection<T> Data { get; set; } = new HashSet<T>();
    }
}