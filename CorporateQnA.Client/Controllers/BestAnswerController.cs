using CorporateQnA.Services.Models;
using CorporateQnA.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Client.Controllers
{
	[Route("api/BestAnswer")]
	public class BestAnswerController : Controller
	{
		private readonly IBestAnswerService BestAnswerService;

		public BestAnswerController(IBestAnswerService bestAnswerService)
		{
			BestAnswerService = bestAnswerService;
		}

		//api/BestAnswer/Add
		[Route("Add")]
		public void AddBestAnswerChoice([FromBody] BestAnswer bestAnswer)
		{
			if (ModelState.IsValid)
				BestAnswerService.AddBestAnswerChoice(bestAnswer);
		}

		// api/BestAnswer/Delete
		[Route("Delete")]
		public void DeleteBestAnswerChoice([FromBody] BestAnswer bestAnswer)
		{
			BestAnswerService.DeleteBestAnswerChoice(bestAnswer);
		}
	}
}
