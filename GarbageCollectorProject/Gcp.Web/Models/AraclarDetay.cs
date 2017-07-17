using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class AraclarDetay
    {
        public string AracID { get; set; }
        public DateTime? AlinisTarihi { get; set; }
        public DateTime? BakimTarihi { get; set; }
        public DateTime? MuayeneTarihi { get; set; }
        public bool? AktifMi { get; set; }

        public IEnumerable<Araclar>  Araclar { get; set; }
    }
}