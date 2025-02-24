using Microsoft.EntityFrameworkCore;

namespace CourseManagement.PL.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TraineeCourse> TraineeCourses { get; set; }

    }
}
