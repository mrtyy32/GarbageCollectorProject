using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("Vardiya")]
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