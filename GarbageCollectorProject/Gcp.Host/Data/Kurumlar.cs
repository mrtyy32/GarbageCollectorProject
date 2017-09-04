using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("Kurumlar")]
	public class Kurumlar
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int KurumID { get; set; }
		public string KurumIsmi { get; set; }
		public string KurumAdresi { get; set; }
		public string TemsilciKisi { get; set; }
		public string TemsilciKisiNo { get; set; }
		public string TemsilciKisiEmail { get; set; }
		public bool? CalismaDurumu { get; set; }
		public string VergiDairesi { get; set; }
		public int? VergiNo { get; set; }
		public string VergiDairesiAdresi { get; set; }
	}
}