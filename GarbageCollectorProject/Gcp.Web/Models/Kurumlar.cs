using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Kurumlar
    {
        public int KurumID { get; set; }
        public string KurumIsmi { get; set; }
        public string KurumAdresi { get; set; }
        public string TemsilciKisiNo { get; set; }
        public string TemsilciKisiEmail { get; set; }
        public bool? CalismaDurumu { get; set; }
        public string VergiDairesi { get; set; }
        public int? VergiNo { get; set; }
        public string VergiDairesiAdresi { get; set; }
    }
}