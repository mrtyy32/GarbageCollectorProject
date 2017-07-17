using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Vergi
    {
        public int VergiID { get; set; }
        public string VergiDairesi { get; set; }
        public int? VergiNo { get; set; }
        public string VergiDairesiAdresi { get; set; }
        public int? KurumID { get; set; }

        public Kurumlar Kurumlar { get; set; }
    }
}