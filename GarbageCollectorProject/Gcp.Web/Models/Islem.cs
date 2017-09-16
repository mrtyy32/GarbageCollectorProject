using System.Collections.Generic;

namespace Gcp.Web.Models
{
	public class Islem
	{
		public int IslemID { get; set; }
		public string IslemTuru { get; set; }

		public List<IslemDetay> IslemDetay { get; set; }
	}
}