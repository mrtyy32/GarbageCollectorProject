using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Personel
    {
        public int PersonelID { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
        public DateTime? DogumTarihi { get; set; }

        public IEnumerable<PersonelDetay> PersonelDetay { get; set; }
    }
}