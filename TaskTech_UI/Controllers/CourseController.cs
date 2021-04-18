using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTech_ApplicationCore.DTO;
using TaskTech_ApplicationCore.Entites;
using TaskTech_ApplicationCore.Interface;

namespace TaskTech_UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IStudentCourse<Course> courseSerivce;
        public CourseController(IStudentCourse<Course> _courseSerivce)
        {
            this.courseSerivce = _courseSerivce;
        }

        #region Index
        public ActionResult Index()
        {

            List<VmCourse> model = null;
            try
            {
                var Data = courseSerivce.List();

                model = Data.Select(x => x.ToModel()).ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        #endregion

        #region Details
        public ActionResult Details(int id)
        {
            var model = new VmCourse();
            try
            {
                var target = courseSerivce.Find(id);

                if (target == null) return Index();

                model = target.ToModel();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }
        #endregion


        #region Create
        public ActionResult Create()
        {
            VmCourse model = new VmCourse();
            try
            {
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VmCourse course)
        {
            try
            {
                Course target = new Course() { Name = course.Name, StartDate = course.StartDate, EndDate = course.EndDate, CreateDate = DateTime.Now };
                courseSerivce.Add(target);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var model = new VmCourse();
            try
            {
                var target = courseSerivce.Find(id);

                if (target == null) return Index();

                model = target.ToModel();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( VmCourse model)
        {
            try
            {
                var target = courseSerivce.Find(model.Id);
                target.Name = model.Name;
                target.StartDate = model.StartDate;
                target.EndDate = model.EndDate;
                courseSerivce.Update(target);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            var model = new VmCourse();
            try
            {
                var target = courseSerivce.Find(id);

                if (target == null) return Index();

                model = target.ToModel();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


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
        #endregion

    }
}