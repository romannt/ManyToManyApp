using ManyToManyApp.Models;
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
    }
}