using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
using BLL.Services;
using DAL;
using DAL.Models;
using Moq;
using Xunit;

namespace XUnitTestProject1.ServiceTester
{
    public class StudentServiceTest
    {
        private StudentService _studentService;

        [Fact]
        public void ShouldGetStudentById()
        {
            //create mock and test data
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.StudentRepository.GetByID(1))
                .Returns(new Student {
                    LastName = "King",
                    FirstMidName = "John",
                });

            //excute test by mock
            _studentService = new StudentService(mock.Object);
            StudentDto stu = _studentService.GetStudentById(1);

            Assert.IsType(typeof(StudentDto), stu);
            Assert.Equal("John King", stu.FullName);
        }
    }
}
