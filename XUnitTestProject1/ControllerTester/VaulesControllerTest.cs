using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
using BLL.Services;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using TemplateOfNetCoreWebApi.Controllers;
using Xunit;

namespace XUnitTestProject1.ControllerTester
{
    public class VaulesControllerTest
    {
        private ValuesController _valuesController;
        private Mock<SchoolContext> mock;
        private Mock<IStudentService> mockStudentService;
        private Mock<ICourseService> mockCourseService;

        public VaulesControllerTest()
        {
            mock = new Mock<SchoolContext>();
            //mock.Setup(a => a.Set<Student>()).Returns(Mock.Of<DbSet<Student>>);
            //mock.Setup(a => a.Set<Course>()).Returns(Mock.Of<DbSet<Course>>);
            //mock.Setup(a => a.Set<Enrollment>()).Returns(Mock.Of<DbSet<Enrollment>>);

            mockStudentService = new Mock<IStudentService>();
            mockCourseService = new Mock<ICourseService>();
        }

        [Fact]
        public void TestGet()
        {
            //create mock and test data
            mockStudentService.Setup(x => x.GetStudentById(1))
                .Returns(new StudentDto {
                    FullName= "Jack"
                });
            mockCourseService.Setup(x => x.GetCourseById(1045))
                .Returns(new CourseDto {
                    Title = "Course1"
                });

            //excute test by mock
            _valuesController = new ValuesController(mock.Object);
            _valuesController.studentService = mockStudentService.Object;
            _valuesController.courceService = mockCourseService.Object;

            var data = (string[])_valuesController.Get();

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Equal("Full Name:Jack Course title:Course1", data[0]);
        }
    }
}
