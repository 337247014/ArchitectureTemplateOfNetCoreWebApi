using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Student> StudentRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<Enrollment> EnrollmentRepository { get; }

        void Save();
    }
}
