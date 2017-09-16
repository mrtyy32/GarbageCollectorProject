using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("Marka")]
	//[JsonObject(IsReference = true)]
	public class Marka
	{
		public Marka()
		{
			this.Araclar = new List<Araclar>();
			this.Model = new List<Model>();
		}
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MarkaID { get; set; }
		public string MarkaAd { get; set; }

		public virtual List<Araclar> Araclar { get; set; }
		public virtual List<Model> Model { get; set; }
	}
}