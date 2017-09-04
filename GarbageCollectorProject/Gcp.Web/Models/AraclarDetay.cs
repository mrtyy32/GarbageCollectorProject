using System;


namespace Gcp.Web.Models
{
    public class AraclarDetay
    {
        public int AracDetayID { get; set; }
        public DateTime? AlinisTarihi { get; set; }
        public DateTime? BakimTarihi { get; set; }
        public DateTime? MuayeneTarihi { get; set; }
		public int AracID { get; set; }

		public Araclar Araclar { get; set; }
    }
}