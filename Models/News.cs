using System.ComponentModel.DataAnnotations;

namespace Viettel_Solution.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(10000)]
        public string Content { get; set; }

        [Required]
        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        public NewsCategory Category { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
