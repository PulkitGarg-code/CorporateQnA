using System;

namespace CorporateQnA.Services.Models
{
    public class ReportQuestion
    {
        public int ReportId { get; set; }

        public int QuestionId { get; set; }

        public string UserId { get; set; }

        public DateTime ReportedAt { get; set; }
    }
}
