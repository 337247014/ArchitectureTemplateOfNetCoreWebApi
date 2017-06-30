using System;
using System.Collections.Generic;
using System.Linq;
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
        private Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();

        [Fact]
        public void ShouldGetStudentById()
        {
            //create mock and test data
            //var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.StudentRepository.GetByID(1))
                .Returns(new Student {
                    LastName = "King",
                    FirstMidName = "John",
                });

            //excute test by mock
            _studentService = new StudentService(mock.Object);
            var stu = _studentService.GetStudentById(1);

            Assert.NotNull(stu);
            Assert.IsType(typeof(StudentDto), stu);
            Assert.Equal("John King", stu.FullName);
        }

        [Fact]
        public void ShouldGetAllStudents()
        {
            var students = GetStudentsList();
            mock.Setup(x => x.StudentRepository.GetAll()).Returns(students);

            _studentService = new StudentService(mock.Object);
            var data = _studentService.GetAllStudents();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.IsType(typeof(List<StudentDto>), data);
            Assert.Equal(3, data.Count);
        }

        private IQueryable<Student> GetStudentsList()
        {
            var students = new List<Student>
            {
                new Student
                {
                    LastName = "A",
                    FirstMidName = "student1"
                },
                new Student
                {
                    LastName ="A",
                    FirstMidName = "student2"
                },
                new Student
                {
                    LastName="B",
                    FirstMidName="student3"
                }
            };
            return students.AsQueryable();
        }

    }
}
