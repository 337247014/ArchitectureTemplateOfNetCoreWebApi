using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Services
{
    public interface IStudentService
    {
        StudentDto GetStudentById(int id);
    }
}
