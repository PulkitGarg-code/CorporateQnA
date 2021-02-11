using CorporateQnA.Services.Models;
using CorporateQnA.Services.ViewModels;
using System.Collections.Generic;


namespace CorporateQnA.Services.Services
{
    public interface IUserService
    {
        IEnumerable<UsersViewModel> GetAllUsersViewModel();
        public int Create(AppUser appUser);
        public AppUser GetUser(int userid);
    }
}
