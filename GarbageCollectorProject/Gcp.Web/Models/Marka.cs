using System.Collections.Generic;

namespace Gcp.Web.Models
{
    public class Marka
    {
        public int MarkaID { get; set; }
        public string MarkaAd { get; set; }

        public IEnumerable<Araclar> Araclar { get; set; }
        public IEnumerable<Model> Model { get; set; }
    }
}