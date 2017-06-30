using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
using DAL;

namespace BLL.Services
{
    public class StudentService : GenericService, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual StudentDto GetStudentById(int id)
        {
            var stu = unitOfWork.StudentRepository.GetByID(id);
            return new StudentDto(){ FullName = stu.FirstMidName + " " + stu.LastName };
        }

        public virtual List<StudentDto> GetAllStudents()
        {
            List<StudentDto> returnList = new List<StudentDto>();
            var students = unitOfWork.StudentRepository.GetAll();
            foreach(var stu in students)
            {
                returnList.Add(new StudentDto() { FullName = stu.FirstMidName + " " + stu.LastName });
            }
            return returnList;
        }

    }
}
