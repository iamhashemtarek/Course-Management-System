using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CourseManagement.PL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CourseManagement.PL.Models
{
    public static class DbSeeder
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<AppDbContext>())
            {
                context.Database.Migrate();

                // ✅ Ensure All Tables Are Empty Before Seeding
                if (context.Categories.Any() && context.Trainers.Any() && context.Courses.Any() &&
                    context.CourseLessons.Any() && context.Trainees.Any() && context.TraineeCourses.Any() && context.Admins.Any())
                    return;

                string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "seedData.json");
                if (!File.Exists(jsonPath)) return;

                var jsonData = File.ReadAllText(jsonPath);
                var seedData = JsonConvert.DeserializeObject<SeedData>(jsonData);

                if (seedData == null) return;

                // 1️⃣ Insert Parent Categories First
                var parentCategories = seedData.Categories.Where(c => c.ParentId == null).ToList();
                context.Categories.AddRange(parentCategories);
                context.SaveChanges();

                // 2️⃣ Insert Child Categories After Parents Exist
                var childCategories = seedData.Categories.Where(c => c.ParentId != null).ToList();
                context.Categories.AddRange(childCategories);
                context.SaveChanges();

                // 3️⃣ Create Mapping Dictionaries
                var categoryMap = context.Categories.ToDictionary(c => c.Name, c => c);
                var trainerMap = context.Trainers.ToDictionary(t => t.Email, t => t);
                var courseMap = new Dictionary<string, Course>();

                // 4️⃣ Insert Trainers Before Courses
                context.Trainers.AddRange(seedData.Trainers);
                context.SaveChanges();

                // 5️⃣ Insert Courses With Correct IDs (Batch Insert)
                var courseList = new List<Course>();
                foreach (var courseSeed in seedData.Courses)
                {
                    if (!categoryMap.TryGetValue(courseSeed.CategoryName, out var category))
                    {
                        Console.WriteLine($"Category '{courseSeed.CategoryName}' not found. Skipping course '{courseSeed.Name}'.");
                        continue;
                    }

                    if (!trainerMap.TryGetValue(courseSeed.TrainerEmail, out var trainer))
                    {
                        Console.WriteLine($"Trainer '{courseSeed.TrainerEmail}' not found. Skipping course '{courseSeed.Name}'.");
                        continue;
                    }

                    var newCourse = new Course
                    {
                        Name = courseSeed.Name,
                        CreationDate = courseSeed.CreationDate,
                        CategoryId = category.Id,
                        TrainerId = trainer.Id,
                        Description = courseSeed.Description
                    };

                    courseList.Add(newCourse);
                }

                context.Courses.AddRange(courseList);
                context.SaveChanges();

                // Update courseMap after saving
                foreach (var course in courseList)
                {
                    courseMap[course.Name] = course;
                }

                // 6️⃣ Insert Course Lessons With Correct Course IDs
                var lessonList = new List<CourseLesson>();
                foreach (var lessonSeed in seedData.CourseLessons)
                {
                    if (!courseMap.TryGetValue(lessonSeed.CourseName, out var course))
                    {
                        Console.WriteLine($"Course '{lessonSeed.CourseName}' not found. Skipping lesson '{lessonSeed.Title}'.");
                        continue;
                    }

                    var newLesson = new CourseLesson
                    {
                        Title = lessonSeed.Title,
                        Order = lessonSeed.Order,
                        CourseId = course.Id
                    };

                    lessonList.Add(newLesson);
                }
                context.CourseLessons.AddRange(lessonList);
                context.SaveChanges();

                // 7️⃣ Insert Trainees
                context.Trainees.AddRange(seedData.Trainees);
                context.SaveChanges();

                // 8️⃣ Insert TraineeCourses With Validation
                var traineeCourseList = new List<TraineeCourse>();
                foreach (var tc in seedData.TraineeCourses)
                {
                    var trainee = context.Trainees.Find(tc.TraineeId);
                    var course = context.Courses.Find(tc.CourseId);

                    if (trainee == null || course == null)
                    {
                        Console.WriteLine($"Skipping TraineeCourse entry (TraineeId: {tc.TraineeId}, CourseId: {tc.CourseId}) because one or both entities are missing.");
                        continue;
                    }

                    traineeCourseList.Add(new TraineeCourse
                    {
                        TraineeId = tc.TraineeId,
                        CourseId = tc.CourseId,
                        RegistrationDate = DateTime.Now
                    });
                }

                context.TraineeCourses.AddRange(traineeCourseList);
                context.SaveChanges();

                // 9️⃣ Insert Admins
                context.Admins.AddRange(seedData.Admins);
                context.SaveChanges();
            }
        }
    }

    // 📌 SeedData Class (Matches JSON File)
    public class SeedData
    {
        public List<Category> Categories { get; set; } = new();
        public List<Trainer> Trainers { get; set; } = new();
        public List<CourseSeed> Courses { get; set; } = new();
        public List<CourseLessonSeed> CourseLessons { get; set; } = new();
        public List<Trainee> Trainees { get; set; } = new();
        public List<TraineeCourse> TraineeCourses { get; set; } = new();
        public List<Admin> Admins { get; set; } = new();
    }

    // 📌 CourseSeed Class (For JSON Mapping)
    public class CourseSeed
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string CategoryName { get; set; }
        public string TrainerEmail { get; set; }
        public string Description { get; set; }
    }

    // 📌 CourseLessonSeed Class (For JSON Mapping)
    public class CourseLessonSeed
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public string CourseName { get; set; }
    }
}
