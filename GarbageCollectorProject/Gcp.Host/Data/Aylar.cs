using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("Aylar")]
	public class Aylar
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AyID { get; set; }
		public string AyAd { get; set; }

		public virtual List<AylikGider> AylikGider { get; set; }
	}
}