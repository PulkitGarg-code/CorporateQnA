using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CorporateQnA.Services.Models;
using CorporateQnA.Services.ViewModels;
using CorporateQnA.Services.Services;

namespace CorporateQnA.Client.Controllers
{
	[Route("api/Question")]
	public class QuestionsController : Controller
	{
		private readonly IQuestionService QuestionService;

		public QuestionsController(IQuestionService theatreService)
		{
			QuestionService = theatreService;
		}

		// api/Question/All
		[Route("All")]
		public IEnumerable<Question> GetAllQuestions()
		{
			return QuestionService.GetAllQuestions();
		}

		// api/Question/AllViews
		[Route("AllViews")]
		public IEnumerable<QuestionsViewModel> GetAllQuestionsViewModel()
		{
			return QuestionService.GetAllQuestionsViewModel();
		}
		//api/question/:id/view/
		[Route("{id}/view")]
		public QuestionsViewModel GetQuestionViewModel(int id)
		{
			return QuestionService.GetQuestionViewModel(id);
		}

		// api/Question/:id/Increment
		[Route("{id}/Increment")]
		public void IncrementTotalViewsCount(int id)
		{
			QuestionService.IncrementTotalViewsCount(id);
		}

		// api/Question/Add
		[Route("Add")]
		public void AddQuestion([FromBody] Question question)
		{
			if (ModelState.IsValid)
				QuestionService.AddQuestion(question);
		}

		// api/Question/:id/Delete
		[Route("{id}/Delete")]
		public void DeleteQuestion(int id)
		{
			QuestionService.DeleteQuestion(id);
		}
	}
}
