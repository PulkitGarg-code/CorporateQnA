using Microsoft.AspNetCore.Identity;

namespace CorporateQnA.Services.Models
{
	public class AppIdentityUser : IdentityUser
	{
		public int UserID { get; set; }
	}
}
