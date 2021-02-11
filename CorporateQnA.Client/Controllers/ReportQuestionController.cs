using Microsoft.AspNetCore.Mvc;
using CorporateQnA.Services.Models;
using CorporateQnA.Services.Services;

namespace CorporateQnA.Client.Controllers
{
    [Route("api/ReportQuestion")]
    public class ReportQuestionsController : Controller
    {
        private readonly IReportQuestionService ReportQuestionService;

        public ReportQuestionsController(IReportQuestionService reportQuestionService)
        {
            ReportQuestionService = reportQuestionService;
        }

        // api/ReportQuestion/Add
        [Route("Add")]
        public void AddReportRequestForQuestion([FromBody] ReportQuestion reportQuestion)
        {
            if (ModelState.IsValid)
                ReportQuestionService.AddReportRequestForQuestion(reportQuestion);
        }

        [Route("{questionId}/{userId}/check")]
        public bool CheckQuestionReportedByUser(int questionId, string userId)
        {
            return ReportQuestionService.CheckQuestionReportedByUser(questionId, userId);
        }

        // api/ReportQuestion/getCount
        [Route("getCount")]
        public int GetCountOfReportedQuestion([FromBody] ReportQuestion reportQuestion)
        {
            return ReportQuestionService.GetCountOfReportedQuestion(reportQuestion);
        }

        // api/ReportQuestion/Delete
        [Route("Delete")]
        public void DeleteQuestion([FromBody] ReportQuestion reportQuestion)
        {
            ReportQuestionService.DeleteReportRequestForQuestion(reportQuestion);
        }
    }
}
