using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CourseManagement.DAL.Models;

namespace CourseManagement.DAL.Models
{
    public class Category : ModelBase
    {

        [Required]
        public string Name { get; set; }

        [ForeignKey("ParentCategory")]
        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    }
}
