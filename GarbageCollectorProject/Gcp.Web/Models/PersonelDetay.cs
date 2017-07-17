using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class PersonelDetay
    {
        public int PersonelID { get; set; }
        public int? UnvanID { get; set; }
        public DateTime? GirisTarihi { get; set; }
        public DateTime? CikisTarihi { get; set; }
        public DateTime? IzinTarihi { get; set; }
        public decimal? Maas { get; set; }
        public int? EgitimID { get; set; }
        public int? VardiyaID { get; set; }
        public bool? CalismaDurumu { get; set; }
        public int? AmirID { get; set; }
        public bool? AmirMi { get; set; }

        public IEnumerable<Egitim>  Egitim { get; set; }
        public IEnumerable<Personel> Personel { get; set; }
        public IEnumerable<Vardiya> Vardiya { get; set; }
    }
}