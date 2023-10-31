namespace Viettel_Solution.DTO
{
    public class SolutionCreateRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile ThumbnailImages { get; set; }
    }
}
