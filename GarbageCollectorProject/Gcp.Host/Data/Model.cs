using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("Model")]
	public class Model
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ModelID { get; set; }
		public string ModelAd { get; set; }
		public int MarkaID { get; set; }

		public virtual Marka Marka { get; set; }
	}
}