using CourseManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//../CourseManagement.DAL/Data/SeedData/SeedData.json
namespace CourseManagement.DAL.Data
{
    public static class CourseManagementDbContextSeed
    {
        public static void Seed(CourseManagementDbContext context)
        {
            // Read the JSON file
            var jsonData = File.ReadAllText("../CourseManagement.DAL/Data/SeedData/SeedData.json");

            // Deserialize the JSON into a dynamic object
            var seedData = JsonConvert.DeserializeObject<dynamic>(jsonData);

            // Seed Admins
            //if (!context.Admins.Any())
            //{
            //    var admins = JsonConvert.DeserializeObject<List<Admin>>(seedData.Admins.ToString());
            //    context.Admins.AddRange(admins);
            //}

            // Seed Categories (ensure parent categories are seeded first)
            if (!context.Categories.Any())
            {
                var categories = JsonConvert.DeserializeObject<List<Category>>(seedData.Categories.ToString());
                context.Categories.AddRange(categories);
            }

            // Seed Trainers
            //if (!context.Trainers.Any())
            //{
            //    var trainers = JsonConvert.DeserializeObject<List<Trainer>>(seedData.Trainers.ToString());
            //    context.Trainers.AddRange(trainers);
            //}

            // Seed Courses
            //if (!context.Courses.Any())
            //{
            //    var courses = JsonConvert.DeserializeObject<List<Course>>(seedData.Courses.ToString());
            //    context.Courses.AddRange(courses);
            //}

            // Seed CourseLessons
            //if (!context.CourseLessons.Any())
            //{
            //    var courseLessons = JsonConvert.DeserializeObject<List<CourseLesson>>(seedData.CourseLessons.ToString());
            //    context.CourseLessons.AddRange(courseLessons);
            //}

            // Seed Trainees
            //if (!context.Trainees.Any())
            //{
            //    var trainees = JsonConvert.DeserializeObject<List<Trainee>>(seedData.Trainees.ToString());
            //    context.Trainees.AddRange(trainees);
            //}

            // Seed TraineeCourses
            //if (!context.TraineeCourses.Any())
            //{
            //    var traineeCourses = JsonConvert.DeserializeObject<List<TraineeCourse>>(seedData.TraineeCourses.ToString());
            //    context.TraineeCourses.AddRange(traineeCourses);
            //}

            // Save changes to the database
            context.SaveChanges();

        }
    }
}
    
