﻿using ManyToManyApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace ManyToManyApp.Controllers
{
    public class HomeController : Controller
    {
        private StudentContext db = new StudentContext();

        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Student student = db.Students.Find(id);
            // Если бы в модели не было ключевого свойства Virtual, нужно было бы использовать Include
            // Если не подключено пространство имён System.Data.Entity студия считает параметр "s => s.Courses" ошибочным
            // student = db.Students.Include(s => s.Courses).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
                return HttpNotFound();
            ViewBag.Courses = db.Courses.ToList();
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student, int[] selectedCourses)
        {
            Student newStudent = db.Students.Find(student.Id);
            newStudent.Name = student.Name;
            newStudent.Surname = student.Surname;
            newStudent.Courses.Clear();
            if (selectedCourses != null)
            {
                // получаем выбранные курсы
                foreach (var c in db.Courses.Where(co => selectedCourses.Contains(co.Id)))
                {
                    newStudent.Courses.Add(c);
                }
            }
            db.Entry(newStudent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {
            ViewBag.Courses = db.Courses.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student, int[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                // получаем выбранные курсы
                foreach (var c in db.Courses.Where(co => selectedCourses.Contains(co.Id)))
                {
                    student.Courses.Add(c);
                }
            }
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}