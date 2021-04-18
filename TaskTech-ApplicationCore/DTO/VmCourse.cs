using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTech_ApplicationCore.Entites;

namespace TaskTech_ApplicationCore.DTO
{
    public static class CourseExtensions
    {


        public static VmCourse ToModel(this Course target)
        {
            var model = new VmCourse();
            model.Id = target.Id;
            model.Name = target.Name;
            model.StartDate = target.StartDate;
            model.EndDate = target.EndDate;
            model.CreateDate = target.CreateDate;

            return model;
        }
    }
    public class VmCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }


    }
}
