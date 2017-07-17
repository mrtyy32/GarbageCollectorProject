using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Vardiya
    {
        public int VardiyaID { get; set; }
        public string Aciklama { get; set; }
        public DateTime? BaslamaSaati { get; set; }
        public DateTime? BitirmeSaati { get; set; }

        public IEnumerable<PersonelDetay> PersonelDetay { get; set; }
    }
}