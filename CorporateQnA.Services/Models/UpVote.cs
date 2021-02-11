using System;

namespace CorporateQnA.Services.Models
{
	public class UpVote
	{
		public int UpVoteId { get; set; }

		public int QuestionId { get; set; }

		public string UserID { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
