using CorporateQnA.Services.Models;
using CorporateQnA.Services.ViewModels;
using System.Collections.Generic;

namespace CorporateQnA.Services.Services
{
    public interface IAnswerService
    {
        public IEnumerable<AnswersViewModel> GetAllAnswersViewModel(int questionId);
        public void AddAnswer(Answer answer);
        public void DeleteAnswer(int id);
    }
}
