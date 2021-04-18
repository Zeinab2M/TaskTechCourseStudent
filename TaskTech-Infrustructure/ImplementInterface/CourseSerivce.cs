using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTech_ApplicationCore.Entites;
using TaskTech_ApplicationCore.Interface;
using TaskTech_Infrustructure.DataContext;

namespace TaskTech_Infrustructure.ImplementInterface
{
    public class CourseSerivce : IStudentCourse<Course>
    {
        TaskTechDbContext db;
        public CourseSerivce(TaskTechDbContext _db)
        {
            db = _db;
        }
        public void Add(Course entiy)
        {
            db.Courses.Add(entiy);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var course = Find(Id);
            db.Courses.Remove(course);
            db.SaveChanges();
        }

        public Course Find(int Id)
        {
            var query = db.Courses.SingleOrDefault(b => b.Id == Id);
            return query;
        }

        public IList<Course> List()
        {
            return db.Courses.ToList();
        }

        public List<Course> Search(string term)
        {
            return db.Courses.Where(x => x.Name.Contains("term")).ToList();

        }

        //this is wrong way that i broke one of princible but i need this function in onther serivce 
        public List<Course> Search(int id)
        {
            throw new NotImplementedException();
        }

        public void Update( Course entity)
        {
            db.Courses.Update(entity);
            db.SaveChanges();
        }
    }
}
