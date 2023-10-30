using System.ComponentModel.DataAnnotations;

namespace Viettel_Solution.Models
{
    public class Solution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Image { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}
