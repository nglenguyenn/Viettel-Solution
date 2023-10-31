using System.ComponentModel.DataAnnotations;

namespace Viettel_Solution.Models
{
    public class Feature
    {
        public string Id { get; set; }
        public string SolutionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public virtual Solution Solution { get; set; }
    }
}
