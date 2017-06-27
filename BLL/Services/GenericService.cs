using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL.Services
{
    public class GenericService
    {
        public IUnitOfWork unitOfWork;
        public GenericService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}
