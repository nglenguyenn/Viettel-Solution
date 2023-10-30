using System.ComponentModel.DataAnnotations;

namespace Viettel_Solution.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SolutionId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public virtual Solution Solution { get; set; }
    }
}
