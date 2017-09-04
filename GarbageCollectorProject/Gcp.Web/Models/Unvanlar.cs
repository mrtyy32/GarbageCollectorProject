using System.Collections.Generic;

namespace Gcp.Web.Models
{
    public class Unvanlar
    {
        public int UnvanID { get; set; }
        public string UnvanAd { get; set; }
        public List<Personel> Personel { get; set; }

    }
}