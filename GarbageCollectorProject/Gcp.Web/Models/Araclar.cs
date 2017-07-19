using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Araclar
    {
        public string AracID { get; set; }
        public int MarkaID { get; set; }
        public int? PersonelID { get; set; }

        public Marka Marka { get; set; }
        public AraclarDetay AraclarDetay { get; set; }
        public Personel Personel { get; set; }
    }
}