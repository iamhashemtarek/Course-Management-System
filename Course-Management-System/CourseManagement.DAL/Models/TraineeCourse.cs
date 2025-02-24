using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CourseManagement.DAL.Models;

namespace CourseManagement.DAL.Models
{
    public class TraineeCourse : ModelBase
    {

        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
