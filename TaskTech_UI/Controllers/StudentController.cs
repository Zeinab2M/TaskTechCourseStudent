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
    public class StudentController : Controller
    {
        private readonly IStudentCourse<Student> studentSerivce;
        private readonly IStudentCourse<Course> courseSerivce;

        public StudentController(IStudentCourse<Student> _studentSerivce, IStudentCourse<Course> _courseSerivce)
        {
            this.studentSerivce = _studentSerivce;
            this.courseSerivce = _courseSerivce;
        }
       
        public ActionResult Index()
        {
           
            List<VmStudent> model = null;
            try
            {
                ViewBag.CoursesList = FillCourseList();
                  model = studentSerivce.List().Select(x => x.ToModel()).ToList();
                return View(model);
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
           
            return View(model);
        }

        #region Details
        public ActionResult Details(int id)
        {

            var model = new VmStudent();
            try
            {
                var target = studentSerivce.Find(id);

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

        #region CreateEdit
        public ActionResult CreateEdit(int id=0)
        {
          
            VmStudent model = new VmStudent();
            try
            {
                if (id == 0)
                {
                    model.Courses = FillCourseList();
                   
                }
                else
                {
                    var target = studentSerivce.Find(id);

                    if (target == null) return Index();

                    model = target.ToModel();
                    model.Courses = FillCourseList();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(int id,VmStudent model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0) 
                    {
                        if (model.CourseId == -1)
                        {
                            ViewBag.Msg = "Select value About Course";
                            model.Courses = FillCourseList();
                            return View(model);
                        }
                        Student target = new Student() { Name = model.Name, Grade = model.Grade, Course = courseSerivce.Find(model.CourseId) };
                        studentSerivce.Add(target);
                    }
                    else
                    {
                        if (model.CourseId == -1)
                        {
                            ViewBag.Msg = "Select value About Course";
                            model.Courses = FillCourseList();
                            return View(model);
                        }

                        var target = studentSerivce.Find(model.Id);
                        target.Name = model.Name;
                        target.Grade = model.Grade;
                        target.Course = courseSerivce.Find(model.CourseId);
                        studentSerivce.Update(target);

                    }
                    return Json(new { isValid = true,html=Helper.RenderRazorViewToString(this, "_IndexAll", studentSerivce.List().Select(x => x.ToModel()).ToList()) });
                }
              
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateEdit", model) });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "_IndexAll", studentSerivce.List().Select(x => x.ToModel()).ToList()) });


        }

        #endregion

        #region Create
        public ActionResult Create()
        {

            VmStudent model = new VmStudent();
            try
            {
                model.Courses = FillCourseList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VmStudent model)
        {
            try
            {
                if (model.CourseId == -1)
                {
                    ViewBag.Msg = "Select value About Course";
                    model.Courses = FillCourseList();
                    return View(model);
                }
                Student target = new Student() { Name = model.Name, Grade = model.Grade, Course = courseSerivce.Find(model.CourseId) };
                studentSerivce.Add(target);
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
            var model = new VmStudent();
            try
            {
                var target = studentSerivce.Find(id);

                if (target == null) return Index();
               
                model = target.ToModel();
                model.Courses = FillCourseList();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( VmStudent model)
        {

            try
            {
                if (model.CourseId == -1)
                {
                    ViewBag.Msg = "Select value About Course";
                    model.Courses = FillCourseList();
                    return View(model);
                }

                var target = studentSerivce.Find(model.Id);
                target.Name = model.Name;
                target.Grade = model.Grade;
                target.Course = courseSerivce.Find(model.CourseId);
                studentSerivce.Update(target);

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
        //public ActionResult Delete(int id)
        //{
        //    var model = new VmStudent();
        //    try
        //    {
        //        var target = studentSerivce.Find(id);

        //        if (target == null) return Index();

        //        model = target.ToModel();
        //    }

        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View(model);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)//, VmStudent model)
        {
            try
            {
                studentSerivce.Delete(id);

                return Json(new { html = Helper.RenderRazorViewToString(this, "_IndexAll", studentSerivce.List().Select(x => x.ToModel()).ToList()) });

            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region FillCourseList
        List<Course> FillCourseList()
        {
            var authors = courseSerivce.List().ToList();
            authors.Insert(0, new Course { Id = -1, Name = " Please Select Course" });
            return authors;
        }
        #endregion

        #region Search
        public ActionResult Search(string term)
        {
            if (term == null) 
            {
                return Json(new { html = Helper.RenderRazorViewToString(this, "_IndexAll", studentSerivce.List().Select(x => x.ToModel()).ToList()) });
            }
            else
            {
                var result = studentSerivce.Search(term).Select(x => x.ToModel());
                return Json(new { html = Helper.RenderRazorViewToString(this, "_IndexAll", result) });

            }

        }
        #endregion


        #region SearchCourse
        public ActionResult SearchCourse(int id)
        {
            if (id == null)
            {
                return Json(new { html = Helper.RenderRazorViewToString(this, "_IndexAll", studentSerivce.List().Select(x => x.ToModel()).ToList()) });
            }
            else
            {
                var result = studentSerivce.Search(id).Select(x => x.ToModel());
                return Json(new { html = Helper.RenderRazorViewToString(this, "_IndexAll", result) });

            }

        }
        #endregion

    }
}