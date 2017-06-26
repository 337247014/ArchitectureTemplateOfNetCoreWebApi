using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL.Services
{
    public class GenericService
    {
        public UnitOfWork unitOfWork;
        public GenericService(SchoolContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

    }
}
