using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.PL.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ParentCategory")]
        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    }
}
