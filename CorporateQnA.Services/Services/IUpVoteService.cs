using CorporateQnA.Services.Models;

namespace CorporateQnA.Services.Services
{
    public interface IUpVoteService
    {
        void AddUpVoteOnQuestion(UpVote upVote);
        bool CheckUpVoteByUserOnQuestion(int questionId, string userId);
        void DeleteUpVoteOnQuestionId(int questionId);
    }
}
