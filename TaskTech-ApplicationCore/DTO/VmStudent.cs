using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTech_ApplicationCore.Entites;

namespace TaskTech_ApplicationCore.DTO
{
    public static class StudentExtensions
    {


        public static VmStudent ToModel(this Student target)
        {
            var model = new VmStudent();
            model.Id = target.Id;
            model.Name = target.Name;
            model.Grade = target.Grade;
            model.Course = target.Course;
            model.CourseId = target.Course.Id;

            return model;
        }
    }
    public class VmStudent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(40)]
        public string Grade { get; set; }
        public Course Course { get; set; }

        public int CourseId { get; set; }

        public List<Course> Courses { get; set; }

        public string valueSearch { get; set; }

    }
}
