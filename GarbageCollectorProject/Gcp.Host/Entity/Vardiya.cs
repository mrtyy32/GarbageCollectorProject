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
    
    public partial class Vardiya
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vardiya()
        {
            this.Personel = new HashSet<Personel>();
        }
    
        public int VardiyaID { get; set; }
        public string Aciklama { get; set; }
        public System.DateTime BaslamaSaati { get; set; }
        public System.DateTime BitirmeSaati { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personel> Personel { get; set; }
    }
}
