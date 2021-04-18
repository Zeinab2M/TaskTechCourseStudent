using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTech_ApplicationCore.Entites;
using TaskTech_ApplicationCore.Interface;

namespace BookStore.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent<Student> studentSerivce;
        private readonly ICourse<Course> courseSerivce;

        public StudentController(IStudent<Student> _studentSerivce, ICourse<Course> _courseSerivce)
        {
            this.studentSerivce = _studentSerivce;
            this.courseSerivce = _courseSerivce;
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = studentSerivce.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = studentSerivce.Find(id);

            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}