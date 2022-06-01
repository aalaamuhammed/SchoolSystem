using Day3.Models;
using Microsoft.EntityFrameworkCore;

namespace Day3.data
{
    public class ITIDBContext:DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<StudentCourse> StudentsCourse { get; set;}
        public ITIDBContext(DbContextOptions options):base(options) { }
        public ITIDBContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=mvcITI;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        // override on data notation 
        // that way called fluent api

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // select entity
            modelBuilder.Entity<Course>().HasKey(c=>c.CrsId);
            modelBuilder.Entity<Course>().Property(c => c.CrsName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<StudentCourse>().HasKey(a => new { a.StdId, a.CrsId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
