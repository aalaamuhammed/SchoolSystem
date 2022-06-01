using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day3.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeptId { get; set; }
        [Required]
        [StringLength(15,MinimumLength =3)]
        public string DeptName { get; set; }
        // handle relations
        // many
        public ICollection<Student> Students { get; set; }
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentCourses = new HashSet<Course>();

        }
        public ICollection<Course> DepartmentCourses { get; set; }
    }
}
