using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("Vardiya")]
	//[JsonObject(IsReference = true)]
	public class Vardiya
	{
		
		public Vardiya()
		{
			this.Personel = new List<Personel>();
		}
		[Key]
		public int VardiyaID { get; set; }
		public string VardiyaAd { get; set; }
		public string BaslamaSaati { get; set; }
		public string BitirmeSaati { get; set; }

		[ForeignKey("VardiyaID")]
		public virtual List<Personel> Personel { get; set; }
	}
}