
using CorporateQnA.Services.ViewModels;
using CorporateQnA.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CorporateQnA.Client.Controllers
{
    [Route("api/User")]
    public class UsersController : Controller
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        // api/User/AllViews
        [Route("AllViews")]
        public IEnumerable<UsersViewModel> GetAllUsersViewModel()
        {
            return UserService.GetAllUsersViewModel();
        }


    }
}
