using CourseManagement.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.DAL.Models
{
    public class Trainee : ModelBase
    {

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public ICollection<TraineeCourse> TraineeCourses { get; set; } = new List<TraineeCourse>();
    }
}
