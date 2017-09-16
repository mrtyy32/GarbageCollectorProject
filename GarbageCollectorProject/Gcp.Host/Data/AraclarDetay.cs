using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("AraclarDetay")]
	//[JsonObject(IsReference = true)]
	public class AraclarDetay
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AracDetayID { get; set; }
		public DateTime? AlinisTarihi { get; set; }
		public DateTime? BakimTarihi { get; set; }
		public int AracID { get; set; }
		public DateTime? MuayeneTarihi { get; set; }

		[ForeignKey("AracID")]
		public virtual Araclar Araclar { get; set; }
	}
}