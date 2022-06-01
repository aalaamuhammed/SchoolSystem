namespace Day3.Models
{
    public class DepartmentCoursesViewModel
    {
        public List<Course> CoursesInDept { get; set; }
        public List<Course> CoursesOutDept { get; set; }
        public Department Department { get; set; }
    }
}
