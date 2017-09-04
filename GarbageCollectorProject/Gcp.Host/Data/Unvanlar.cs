using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("Unvanlar")]
	public class Unvanlar
	{
		public Unvanlar()
		{
			this.Personel = new List<Personel>();
		}
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UnvanID { get; set; }
		public string UnvanAd { get; set; }

		[ForeignKey("UnvanID")]
		public virtual List<Personel> Personel { get; set; }
	}
}