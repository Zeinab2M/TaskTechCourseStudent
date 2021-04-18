using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTech_ApplicationCore.Entites;
using TaskTech_ApplicationCore.Interface;

namespace TaskTechUserInterface.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourse<Course> courseSerivce;
        public CourseController(ICourse<Course> _courseSerivce)
        {
            this.courseSerivce = _courseSerivce;
        }
        // GET: Authors
        public ActionResult Index()
        {
            var courses = courseSerivce.List();
            return View(courses);
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            var course = courseSerivce.Find(id);
            return View(course);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            try
            {

                courseSerivce.Add(course);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Edit(int id)
        {
            var author = courseSerivce.Find(id);
            return View(author);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Course course)
        {
            try
            {
                courseSerivce.Update(id, course);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int id)
        {
            var author = courseSerivce.Find(id);
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Course course)
        {
            try
            {
                courseSerivce.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}