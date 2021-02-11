using Microsoft.AspNetCore.Mvc;
using CorporateQnA.Services.Models;
using CorporateQnA.Services.Services;

namespace CorporateQnA.Client.Controllers
{
    [Route("api/UserReaction")]
    public class UserReactionController : Controller
    {
        private readonly IUserReactionService UserReactionService;

        public UserReactionController(IUserReactionService userReactionService)
        {
            UserReactionService = userReactionService;
        }

        // api/userReaction/Add
        [Route("Add")]
        public void AddUserReaction([FromBody] UserReaction userReaction)
        {
            if (ModelState.IsValid)
                UserReactionService.AddUserReaction(userReaction);
        }

        // api/userReaction/Delete
        [Route("Delete")]
        public void DeleteUserReaction([FromBody] UserReaction userReaction)
        {
            UserReactionService.DeleteUserReaction(userReaction);
        }

        [Route("{answerId}/{userId}/{reaction}/check")]
        public bool checkUserHasAlreadyReacted(int answerId, string userId, int reaction)
        {
            return UserReactionService.checkUserHasAlreadyReacted(answerId, userId ,reaction);
        }
        
    }
}
