using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gcp.Host.Data
{
	[Table("IslemDetay")]
	//[JsonObject(IsReference = true)]
	public class IslemDetay
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IslemDetayID { get; set; }
		public int IslemID { get; set; }
		public string IslemIcerigi { get; set; }
		public string Kullanici { get; set; }
		public DateTime IslemTarihi { get; set; }

		[ForeignKey("IslemID")]
		public virtual Islem Islem { get; set; }

	}
}