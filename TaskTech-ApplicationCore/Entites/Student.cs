using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTech_ApplicationCore.Entites
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public Course Course { get; set; }

      


    }
}
