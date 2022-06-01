using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day3.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        
        [Remote("CheckEmailExistance","student")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [ForeignKey("Department")]
        public int? DeptNo { get; set; }
        // handle relations
        // one 

       public virtual Department Department { get; set; }
       public virtual ICollection<StudentCourse> StudentCourse { get; set; } 
    }
}
