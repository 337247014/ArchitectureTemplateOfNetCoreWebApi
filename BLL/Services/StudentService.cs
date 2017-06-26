using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
using DAL;

namespace BLL.Services
{
    public class StudentService : GenericService, IStudentService
    {
        public StudentService(SchoolContext context) : base(context)
        {
        }

        public StudentDto GetStudentById(int id)
        {
            var stu = unitOfWork.StudentRepository.GetByID(id);
            return new StudentDto(){ FullName = stu.FirstMidName + " " + stu.LastName };
        }

    }
}
