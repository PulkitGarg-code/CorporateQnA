using CorporateQnA.Services.Models;

namespace CorporateQnA.Services.Services
{
    public interface IBestAnswerService
    {
        void AddBestAnswerChoice(BestAnswer bestAnswer);
        void DeleteBestAnswerChoice(BestAnswer bestAnswer);
    }
}
