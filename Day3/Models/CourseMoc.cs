using Day3.data;
using Microsoft.EntityFrameworkCore;

namespace Day3.Models
{
    public interface ICourse
    {
        public List<Course> GetAllCourses();
        public Course GetCourseById(int id);
        public Course AddCourse(Course crs);
        public void DeleteCourse(int id);
        public void EditCourse(Course crs);
    }

    public class CourseDb : ICourse
    {
        ITIDBContext db;
        public Course AddCourse(Course crs)
        {
            db.Courses.Add(crs);
            db.SaveChanges();
            return crs;
        }
        public CourseDb(ITIDBContext _db)
        {
            this.db = _db;
        }

        public void DeleteCourse(int id)
        {
            db.Courses.Remove(db.Courses.FirstOrDefault(a => a.CrsId == id));
            db.SaveChanges();
        }

        public void EditCourse(Course crs)
        {
            Course OldCourse = db.Courses.FirstOrDefault(a => a.CrsId == crs.CrsId);
            OldCourse.CrsName = crs.CrsName;
            db.SaveChanges();
        }

        public List<Course> GetAllCourses()
        {
           // return db.Courses.Include(a => a.Department).ToList();
           return db.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            //  return db.Courses.Include(a => a.Department).FirstOrDefault(a => a.CrsId == id);
            return db.Courses.FirstOrDefault(a => a.CrsId == id);

        }
    }
}
