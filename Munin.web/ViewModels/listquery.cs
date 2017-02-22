using System.ComponentModel;

namespace Munin.web.ViewModels
{
    public class ListQuery
    {
        [Description("Query in text")]
        public string Q { get; set; } //query
        [Description("Number of items per page")]
        public int Size { get; set; } // pagesize
        [Description("Current page")]
        public int P { get; set; } // current page
        [Description("Order direction - asc or desc")]
        public string O { get; set; } // Order by - asc/desc

        [Description("Sort - columnname")]
        public string S { get; set; } //Sort - column name
    }
}