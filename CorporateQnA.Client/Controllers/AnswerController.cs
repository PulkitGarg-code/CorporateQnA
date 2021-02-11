using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CorporateQnA.Services.Models;
using CorporateQnA.Services.Services;
using CorporateQnA.Services.ViewModels;

namespace CorporateQnA.Client.Controllers
{
    [Route("api/Answer")]
    public class AnswerController : Controller
    {
        private readonly IAnswerService AnswerService;

        public AnswerController(IAnswerService answerService)
        {
            AnswerService = answerService;
        }

        // api/Answer/questionid/AllViews
        [Route("{questionId}/AllViews")]
        public IEnumerable<AnswersViewModel> GetAllAnswersViewModel(int questionId)
        {
            return AnswerService.GetAllAnswersViewModel(questionId);
        }

        // api/Answer/Add
        [Route("Add")]
        public void AddAnswer([FromBody] Answer answer)
        {
            if (ModelState.IsValid)
                AnswerService.AddAnswer(answer);
        }

        // api/Answer/:id/Delete
        [Route("{id}/Delete")]
        public void DeleteAnswer(int id)
        {
            AnswerService.DeleteAnswer(id);
        }
    }
}
