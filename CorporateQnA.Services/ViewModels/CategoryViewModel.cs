
namespace CorporateQnA.Services.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        
        public string Description { get; set; }
        
        public int TaggedInWeek { get; set; }
        
        public int TaggedInMonth { get; set; }
        
        public int TaggedInTotal { get; set; }
    }
}
