//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Services.Description;


namespace Munin.web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Billeder
    {
        [Required]
        public int BilledID { get; set; }

        [Required(ErrorMessage = "Journal skal udfyldes")]
        public string Journal { get; set; }

        [RegularExpression(@"^(?i)B.\d{4}$", ErrorMessage = "Billedindex skal opfylde formatet B.1234")]
        public string Billedindex { get; set; }

        [RegularExpression(@"^\d{2}.\d{2}[a-�A-�0-9]*$", ErrorMessage = "Numordning skal opfylde formatet 12.34a�")]
        public string Numordning { get; set; }

        //[Required(ErrorMessage = "Der skal v�lges en ordning.")]
        public string Ordning { get; set; }
        public string CDnr { get; set; }
        public string Fotograf { get; set; }
        public string Format { get; set; }

        [Required(ErrorMessage = "Der skal v�lges materiale til billede.")]
        public Nullable<double> Materiale { get; set; }
        public string Placering { get; set; }
        public bool Ophavsret { get; set; }
        public bool Klausul { get; set; }
        
        [ValidDate(ErrorMessage = "Dateringen er ikke godkendt.")]
        public DateTime Datering { get; set; }
        public string Indlevering { get; set; }
        public string Note { get; set; }
        public Nullable<int> JournalID { get; set; }    
        public virtual Journaler Journaler { get; set; }
    }
}
