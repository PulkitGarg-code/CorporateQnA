using System;

namespace CorporateQnA.Services.ViewModels
{
	public class AnswersViewModel
	{
		public string UserID { get; set; }

		public int AnswerId { get; set; }

		public string FullName { get; set; }

		public string ProfileImage { get; set; }
		
		public string AnswerContent { get; set; }

		public DateTime CreatedAt { get; set; }

		public int TotalLikes { get; set; }

		public int TotalDislikes { get; set; }
	}
}
