using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace TemplateOfNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private UnitOfWork unitOfWork;
        private readonly SchoolContext _context;
        private IStudentService studentService;
        private ICourseService courceService;

        /// <summary>
        /// default get DBContext by constructor because of using DI DBContext in startup.cs configure 
        /// </summary>
        /// <param name="context"></param>
        public ValuesController(SchoolContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(context);
            studentService = new StudentService(unitOfWork);
            courceService = new CourseService(unitOfWork);

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var student = studentService.GetStudentById(1);
            var course = courceService.GetCourseById(1045);
            return new string[] { "Full Name:" + student.FullName + " Course title:" + course.Title};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //var student = unitOfWork.StudentRepository.GetByID(1);
            var student = studentService.GetStudentById(id);
            var course = courceService.GetCourseById(1045);
            return "Full Name:" + student.FullName + " Course title:" + course.Title;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
