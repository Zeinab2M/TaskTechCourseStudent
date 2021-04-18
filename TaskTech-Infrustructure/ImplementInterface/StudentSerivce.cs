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
    public class StudentSerivce : IStudentCourse<Student>
    {
        TaskTechDbContext db;
        public StudentSerivce(TaskTechDbContext _db)
        {
            db = _db;
        }
        public void Add(Student entiy)
        {
            db.Students.Add(entiy);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var student = Find(Id);
            db.Students.Remove(student);
            db.SaveChanges();
        }

        public Student Find(int Id)
        {
            var query = db.Students.Include(x => x.Course).SingleOrDefault(b => b.Id == Id);
            return query;
        }

        public IList<Student> List()
        {
            return db.Students.Include(x => x.Course).ToList();
        }

        public List<Student> Search(string term)
        {
            var query = db.Students.Include(x => x.Course).Where(x => x.Name.Contains(term)
              || x.Grade.Contains(term) || x.Course.Name.Contains(term)).ToList();

            return query;
        }
        public List<Student> Search(int id)
        {
            var query = db.Students.Include(x => x.Course).Where(x => x.Course.Id==id).ToList();

            return query;
        }

        public void Update( Student entity)
        {
            db.Students.Update(entity);
            db.SaveChanges();
        }
    }
}
