using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    /// <summary>
    /// The unit of work class serves one purpose: to make sure that when you use multiple repositories, 
    /// they share a single database context. That way, when a unit of work is complete you can call the 
    /// SaveChanges method on that instance of the context and be assured that all related changes will be coordinated.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private SchoolContext context;
        private IRepository<Student> studentRepository;
        private IRepository<Course> courseRepository;
        private IRepository<Enrollment> enrollmentRepository;

        public UnitOfWork(SchoolContext context)
        {
            this.context = context;
        }
        public IRepository<Student> StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                {
                    this.studentRepository = new GenericRepository<Student>(context);
                }
                return studentRepository;
            }
        }

        public IRepository<Course> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new GenericRepository<Course>(context);
                }
                return courseRepository;
            }
        }

        public IRepository<Enrollment> EnrollmentRepository
        {
            get
            {
                if (this.enrollmentRepository == null)
                {
                    this.enrollmentRepository = new GenericRepository<Enrollment>(context);
                }
                return enrollmentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
