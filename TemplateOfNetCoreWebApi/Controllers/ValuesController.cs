using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace TemplateOfNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly SchoolContext _context;
        private UnitOfWork unitOfWork;
        /// <summary>
        /// default get DBContext by constructor because of using DI DBContext in startup.cs configure 
        /// </summary>
        /// <param name="context"></param>
        public ValuesController(SchoolContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(context);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var student = unitOfWork.StudentRepository.GetByID(1);
            return new string[] { "value1", "value2","Name:" + student.FirstMidName };
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
