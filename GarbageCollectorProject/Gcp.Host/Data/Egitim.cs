using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("Egitim")]
	//[JsonObject(IsReference = true)]
	public class Egitim
	{
		public Egitim()
		{
			this.Personel = new List<Personel>();
		}
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EgitimID { get; set; }
		public string EgitimAd { get; set; }
		//public int Deger { get; set; }

		[ForeignKey("EgitimID")]
		public virtual List<Personel> Personel { get; set; }
	}
}