using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcp.Web.Models
{
    public class Model
    {
        public int ModelID { get; set; }
        public string ModelAd { get; set; }
        public int? MarkaID { get; set; }

        public Marka Marka { get; set; }
    }
}