using Day3.data;
using Day3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Day3.Controllers
{
    public class DepartmentCoursesController : Controller
    {
        ITIDBContext db;
        public DepartmentCoursesController(ITIDBContext _db) {
            db = _db;
        }
        public IActionResult GetCourses(int id)
        {
            DepartmentCoursesViewModel viewModel = new DepartmentCoursesViewModel();
            Department dept= db.Departments.Include(a => a.DepartmentCourses).FirstOrDefault(a => a.DeptId == id);
            viewModel.Department=dept;
            viewModel.CoursesInDept= dept!=null? dept.DepartmentCourses.ToList():new List<Course>();
            viewModel.CoursesOutDept = db.Courses.ToList().Except(viewModel.CoursesInDept)?.ToList();
            return PartialView(viewModel);
        }
        public IActionResult UpdateDept(int [] coursesInDept, int [] coursesOutDept, int id)
        {
            Department dept = db.Departments.Include(a => a.DepartmentCourses).FirstOrDefault(a => a.DeptId == id);
            List<Course> coursesToRemove = new List<Course>();
            List<Course> coursesToAdd = new List<Course>();
            foreach (var item in coursesInDept)
            {
                coursesToRemove.Add(db.Courses.FirstOrDefault(a=>a.CrsId==item));
            }
            foreach (var item in coursesToRemove)
            {
                dept.DepartmentCourses.Remove(item);
               
            }
          
            foreach (var item in coursesOutDept)
            {
                coursesToAdd.Add(db.Courses.FirstOrDefault(a => a.CrsId == item));
            }
            foreach (var item in coursesToAdd)
            {
                dept.DepartmentCourses.Add(item);
                
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Index()
        {
            ViewBag.depts=new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            return View();
        }
    }
}
