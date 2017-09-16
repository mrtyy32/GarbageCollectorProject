using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("Islem")]
	//[JsonObject(IsReference = true)]
	public class Islem
	{
		public Islem()
		{
			this.IslemDetay = new List<IslemDetay>();
		}
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IslemID { get; set; }
		public string IslemTuru { get; set; }
		[ForeignKey("IslemID")]
		public virtual List<IslemDetay> IslemDetay { get; set; }
	}
}