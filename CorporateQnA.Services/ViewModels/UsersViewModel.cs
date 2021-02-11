
namespace CorporateQnA.Services.ViewModels
{
	public class UsersViewModel
	{
		public string UserID{ get; set; }

		public string FullName{ get; set; }

		public string ProfileImage{ get; set; }

		public string Department{ get; set; }

		public string Location{ get; set; }

		public string Designation{ get; set; }

		public int TotalLikes{ get; set; }

		public int TotalDislikes{ get; set; }

		public int TotalQuestionsAsked{ get; set; }

		public int TotalQuestionsAnswered{ get; set; }

		public int TotalQuestionsSolved{ get; set; }
	}
}
