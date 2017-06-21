using System;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }
        //默认映射成为对应表名为Courses的数据库表
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        //修改默认映射对应的数据库表名
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //将Cource的实体映射成Cource表而非Courses
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
