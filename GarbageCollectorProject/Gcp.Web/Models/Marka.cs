using System.Collections.Generic;

namespace Gcp.Web.Models
{
    public class Marka
    {
        public int MarkaID { get; set; }
        public string MarkaAd { get; set; }

        public List<Model> Model { get; set; }
    }
}