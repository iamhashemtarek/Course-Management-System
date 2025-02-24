using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CourseManagement.DAL.Models;

namespace CourseManagement.DAL.Models
{
    public class CourseLesson : ModelBase
    {

        [Required]
        public string Title { get; set; }

        public int Order { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
