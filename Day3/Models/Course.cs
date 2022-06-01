using System.ComponentModel.DataAnnotations.Schema;

namespace Day3.Models
{   // poco classs : ENTITY WITHOUT ANY DATA NOTATION
    public class Course
    {
        public int CrsId { get; set; }
        public string CrsName { get; set; }
        public ICollection<Department> CourseDepartments { get; set; }
        public Course()
        {
            CourseDepartments= new List<Department>();
        }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
