using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("Araclar")]
	public class Araclar
	{
		public Araclar()
		{
			this.AraclarDetay = new List<AraclarDetay>();
			this.Personel = new List<Personel>();
		}
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AracID { get; set; }
		public int MarkaID { get; set; }
		//public int? PersonelID { get; set; }
		public bool? AktifMi { get; set; }
		public string AracPlaka { get; set; }
		public int? ModelID { get; set; }

		[ForeignKey("MarkaID")]
		public virtual Marka Marka { get; set; }
		[ForeignKey("ModelID")]
		public virtual Model Model { get; set; }

		[ForeignKey("AracID")]
		public virtual List<AraclarDetay> AraclarDetay { get; set; }
		//[ForeignKey("PersonelID")]
		public virtual List<Personel> Personel { get; set; }
	}
}