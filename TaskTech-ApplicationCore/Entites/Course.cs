using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTech_ApplicationCore.Entites
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


    }
}
