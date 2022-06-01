using Day3.data;
using Microsoft.EntityFrameworkCore;

namespace Day3.Models
{
    public interface IStudent
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(int id);
        public Student AddStudent(Student std);
        public void DeleteStudent(int id);
        public void EditStudent(Student std);
    }
    public class StudentMoc:IStudent
    {
        static List <Student> Students = new List <Student>()
        {
            new Student(){ 
                Name = "aalaa",
                Age =25,
                Id=1,
                Email="aalaa@gmail.com"
            },
            new Student(){Name = "Kareem",
                Age =25,
                Id=1,
                Email="Kareem@gmail.com"},
            new Student(){ Name = "Ahmed",
                Age =25,
                Id=1,
                Email="Ahmed@gmail.com"},

        };
        public List<Student> GetAllStudents() { 
            return Students;
        }
        public Student GetStudentById(int id)
        {
            return Students.FirstOrDefault(a=>id==a.Id);
        }
        public Student AddStudent(Student std)
        {
            Students.Add(std);
            return std;
        }
        public void DeleteStudent(int id)
        {
            Students.Remove(Students.FirstOrDefault(a => a.Id == id));
        } 
        public void EditStudent(Student std)
        {
            Student oldOne = Students.FirstOrDefault(x => x.Id == std.Id);
            oldOne.Name = std.Name;
            oldOne.Age = std.Age;

        }
    }

    public class StudentDb : IStudent
    {
        ITIDBContext db;
        public Student AddStudent(Student std)
        {
            db.Students.Add(std);
            db.SaveChanges();
            return std;
        }
        public StudentDb(ITIDBContext _db)
        {
            this.db = _db;
        }

        public void DeleteStudent(int id)
        {
            db.Students.Remove(db.Students.FirstOrDefault(a=>a.Id==id));
            db.SaveChanges();
        }

        public void EditStudent(Student std)
        {
           Student OldStudent = db.Students.FirstOrDefault(a=>a.Id==std.Id);
            OldStudent.Name = std.Name;
            OldStudent.Age = std.Age;
            OldStudent.Email= std.Email;
            OldStudent.DeptNo = std.DeptNo;
            db.SaveChanges();
        }

        public List<Student> GetAllStudents()
        {
           return db.Students.Include(a => a.Department).ToList();
        }

        public Student GetStudentById(int id)
        {
            return db.Students.Include(a => a.Department).FirstOrDefault(a=>a.Id==id);
        }
    }
}
