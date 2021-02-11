using CorporateQnA.Services.Models;

namespace CorporateQnA.Services.Services
{
    public interface IReportQuestionService
    {
        void AddReportRequestForQuestion(ReportQuestion reportQuestion);
        bool CheckQuestionReportedByUser(int questionId, string userId);
        public int GetCountOfReportedQuestion(ReportQuestion reportQuestion);
        void DeleteReportRequestForQuestion(ReportQuestion reportQuestion);
    }
}
