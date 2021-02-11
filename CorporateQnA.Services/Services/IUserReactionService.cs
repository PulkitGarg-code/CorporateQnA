using CorporateQnA.Services.Models;
using CorporateQnA.Services.ViewModels;


namespace CorporateQnA.Services.Services
{
    public interface IUserReactionService
    {
        public void AddUserReaction(UserReaction userReaction);
        public void DeleteUserReaction(UserReaction userReaction);
        bool checkUserHasAlreadyReacted(int questionId, string userId, int reaction);
       
    }
}
