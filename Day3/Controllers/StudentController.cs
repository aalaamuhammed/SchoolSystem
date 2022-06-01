using Day3.data;
using Day3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day3.Controllers
{
    public class StudentController : Controller
    {
        IStudent db;
        ITIDBContext c;
    // Index to list a group of Student depend on name of controllers
        public StudentController(IStudent Db , ITIDBContext _c )
        {
            this.db = Db;
            c= _c;
        }
        public IActionResult Index()
        {
            return View(db.GetAllStudents()) ; 
        }
      public IActionResult Details(int id) {
            Student student = db.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);

        }
      public IActionResult Create()
        {
            
            SelectList depts = new SelectList(c.Departments.ToList(),"DeptId","DeptName");
            ViewBag.departments = depts;
            return View();
        }
    [HttpPost]
    public IActionResult Create(Student student)
        {
            if (ModelState.IsValid) {
                db.AddStudent(student);
                return RedirectToAction("index", "student");
            }
            return View(student);
         }
    public IActionResult Delete(int id)
        {
           
            return View(db.GetStudentById(id));
        }
    [HttpPost]
    [ActionName("delete")]
    public IActionResult ConfirmDelete(int id)
        {
             db.DeleteStudent(id);
              return RedirectToAction("index");
        }
    public IActionResult Edit(int id)
        {
            return View(db.GetStudentById(id));
        }
    [HttpPost]
    public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.EditStudent(student);
                return RedirectToAction("index");
            }
            return View(student);
        }
    public IActionResult CheckEmailExistance(string Email) {
            Student student = db.GetAllStudents().FirstOrDefault(a => a.Email == Email);
            if (student == null)
            {
                return Json(true);
            }
            return Json("Not Valid");
        
        }
    }
   
}
