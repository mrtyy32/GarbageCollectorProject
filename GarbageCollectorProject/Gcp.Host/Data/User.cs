using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gcp.Host.Data
{
	[Table("User")]
	public class User
	{
		[Key]
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string UserPassword { get; set; }
		public string UserMail { get; set; }
		public string Role { get; set; }
	}
}