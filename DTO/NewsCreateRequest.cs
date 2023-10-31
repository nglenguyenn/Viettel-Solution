using Viettel_Solution.Models;

namespace Viettel_Solution.DTO
{
    public class NewsCreateRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public IFormFile ThumbnailImages { get; set; }
        public NewsCategory Category { get; set; }
        public DateTime Date { get; set; }
    }
}
