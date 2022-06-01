using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day3.Models;
using Day3.data;

namespace Day3.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ITIDBContext _context;

        public DepartmentController(ITIDBContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
              return View(await _context.Departments.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptId,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }
            DepartmentCoursesViewModel DepartmentCourse = new DepartmentCoursesViewModel();
            Department department = _context.Departments.Include(a => a.DepartmentCourses).FirstOrDefault(a => a.DeptId == id);
            DepartmentCourse.Department = department;
            DepartmentCourse.CoursesInDept = department != null ? department.DepartmentCourses.ToList() : new List<Course>();
            DepartmentCourse.CoursesOutDept = _context.Courses.ToList().Except(DepartmentCourse.CoursesInDept)?.ToList();
            if (department == null)
            {
                return NotFound();
            }
            return View(DepartmentCourse);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int[] coursesInDept, int[] coursesOutDept, [Bind("Department,CoursesInDept,CoursesOutDept")] DepartmentCoursesViewModel departmentCoursesViewModel)
        {
            Department dept = _context.Departments.Include(a => a.DepartmentCourses).FirstOrDefault(a => a.DeptId == id);
            List<Course> coursesToRemove = new List<Course>();
            List<Course> coursesToAdd = new List<Course>();
            foreach (var item in coursesInDept)
            {
                coursesToRemove.Add(_context.Courses.FirstOrDefault(a => a.CrsId == item));
            }
            foreach (var item in coursesToRemove)
            {
                dept.DepartmentCourses.Remove(item);

            }

            foreach (var item in coursesOutDept)
            {
                coursesToAdd.Add(_context.Courses.FirstOrDefault(a => a.CrsId == item));
            }
            foreach (var item in coursesToAdd)
            {
                dept.DepartmentCourses.Add(item);

            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'ITIDBContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return _context.Departments.Any(e => e.DeptId == id);
        }
    }
}
