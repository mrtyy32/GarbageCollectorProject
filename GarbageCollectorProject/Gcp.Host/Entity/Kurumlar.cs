//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gcp.Host.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kurumlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kurumlar()
        {
            this.Vergi = new HashSet<Vergi>();
        }
    
        public int KurumID { get; set; }
        public string KurumIsmi { get; set; }
        public string KurumAdresi { get; set; }
        public string TemsilciKisiNo { get; set; }
        public string TemsilciKisiEmail { get; set; }
        public Nullable<bool> CalismaDurumu { get; set; }
        public string VergiID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vergi> Vergi { get; set; }
    }
}
