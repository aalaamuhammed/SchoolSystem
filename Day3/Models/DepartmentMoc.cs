using Day3.data;

namespace Day3.Models
{
    public interface IDepartment
    {
        public List<Department> GetAllDepartments();
        public Department GetDepartmentById(int id);
        public Department AddDepartment(Department department);
        public void DeleteDepartment(int id);
        public void EditDepartment(Department department);
    }
    public class DepartmentDB : IDepartment
    {
        ITIDBContext db;
        public DepartmentDB(ITIDBContext _db) {
        db = _db;
        }
        public Department AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
            return department;
        }

        public void DeleteDepartment(int id)
        {
           db.Departments.Remove(db.Departments.FirstOrDefault(a=>a.DeptId==id));
           db.SaveChanges();
        }

        public void EditDepartment(Department department)
        {
            Department OldDepartment = db.Departments.FirstOrDefault(a => a.DeptId == department.DeptId);
            OldDepartment.DeptName = department.DeptName;
            db.SaveChanges();
        }

        public List<Department> GetAllDepartments()
        {
           return db.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return db.Departments.FirstOrDefault(a => a.DeptId == id);
        }
    }
}
