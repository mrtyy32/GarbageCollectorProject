using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("AylikGider")]
	public class AylikGider
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AylikGiderID { get; set; }
		public int AyID { get; set; }
		public int Yil { get; set; }
		public decimal AylikToplam { get; set; }

		[ForeignKey("AyID")]
		public virtual Aylar Aylar { get; set; }
	}
}