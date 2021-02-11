using CorporateQnA.Services.Models;
using CorporateQnA.Services.ViewModels;
using System.Collections.Generic;

namespace CorporateQnA.Services.Services
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAllQuestions();
        IEnumerable<QuestionsViewModel> GetAllQuestionsViewModel();
        QuestionsViewModel GetQuestionViewModel(int id);
        void IncrementTotalViewsCount(int id);
        void AddQuestion(Question question);
        void DeleteQuestion(int id);
    }
}
