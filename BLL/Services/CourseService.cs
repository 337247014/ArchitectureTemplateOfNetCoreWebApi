using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
using DAL;

namespace BLL.Services
{
    public class CourseService : GenericService, ICourseService
    {
        public CourseService(SchoolContext context) : base(context)
        {
        }

        public CourseDto GetCourseById(int id)
        {
            var course = unitOfWork.CourseRepository.GetByID(id);
            return new CourseDto() { Title = course.Title };
        }

    }
}
