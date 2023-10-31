using System.ComponentModel.DataAnnotations;

namespace Viettel_Solution.Models
{
    public class News
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public NewsCategory Category { get; set; }
        public DateTime Date { get; set; }
    }
}
