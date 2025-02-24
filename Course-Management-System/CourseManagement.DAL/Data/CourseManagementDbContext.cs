using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.DAL.Models;


namespace CourseManagement.DAL.Data
{
    public class CourseManagementDbContext : DbContext
    {
        public CourseManagementDbContext(DbContextOptions<CourseManagementDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TraineeCourse> TraineeCourses { get; set; }
    }
}
