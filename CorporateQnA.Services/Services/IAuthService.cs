
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Services
{
	public interface IAuthService
	{
		public Task<bool> Login(string email, string password);
		public Task<List<string>> Register(string name, string username, string email, string password, string location, string position, string department,string profileImage);
		public Task<string> Logout(string logoutId);
	}
}
