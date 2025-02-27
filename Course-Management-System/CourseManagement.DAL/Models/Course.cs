﻿using CourseManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourseManagement.DAL.Models
{
    public class Course : ModelBase
    {

        [Required]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public string? Description { get; set; }

        public ICollection<CourseLesson> CourseLessons { get; set; } = new List<CourseLesson>();

        public ICollection<TraineeCourse> TraineeCourses { get; set; } = new List<TraineeCourse>();
    }
    }
