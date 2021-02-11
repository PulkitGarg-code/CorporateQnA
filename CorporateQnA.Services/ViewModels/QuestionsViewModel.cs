using System;

namespace CorporateQnA.Services.ViewModels
{
	public class QuestionsViewModel
	{	
		public int QuestionId { get; set; }

		public string UserID { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Title { get; set; }

		public  string Description { get; set; }

		public int ViewsCount { get; set; }

		public bool Resolved { get; set; }

		public DateTime CreatedAt { get; set; }

		public string FullName { get; set; }

		public string ProfileImage { get; set; }

		public int TotalUpVotes { get; set; }

		public int TotalAnswers { get; set; }

	}
}
