using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskTech_ApplicationCore.Entites;

namespace TaskTech_Infrustructure.DataContext
{
   public class TaskTechDbContext : DbContext
    {
        public TaskTechDbContext(DbContextOptions<TaskTechDbContext> options) : base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
