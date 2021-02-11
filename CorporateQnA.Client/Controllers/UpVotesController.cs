using CorporateQnA.Services.Models;
using CorporateQnA.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Client.Controllers
{
	[Route("api/UpVote")]
	public class UpVotesController : Controller
	{
		private readonly IUpVoteService UpVoteService;

		public UpVotesController(IUpVoteService upVoteService)
		{
			UpVoteService = upVoteService;
		}

		// api/UpVote/Add
		[Route("Add")]
		public void AddUpVoteOnQuestion([FromBody] UpVote upVote)
		{
			if (ModelState.IsValid)
				UpVoteService.AddUpVoteOnQuestion(upVote);
		}

		[Route("{questionId}/{userId}/check")]
		public bool CheckUpVoteByUserOnQuestion(int questionId, string userId)
		{
			return UpVoteService.CheckUpVoteByUserOnQuestion(questionId, userId);
		}

		// api/UpVote/:id/Delete
		[Route("{questionId}/Delete")]
		public void DeleteUpVote(int questionId)
		{
			UpVoteService.DeleteUpVoteOnQuestionId(questionId);
		}
	}
}
