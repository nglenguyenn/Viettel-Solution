namespace Viettel_Solution.DTO
{
    public class FeatureCreateRequest
    {
        public string Id { get; set; }
        public string SolutionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public IFormFile ThumbnailImages { get; set; }
    }
}
