using Microsoft.EntityFrameworkCore;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.DAL.Data;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BLL.Repcsitories
{
    public class DepartmintRepository : GenericRepository<Department> , IDepartmintRepository
    {
        //private readonly ApplicationDpContext _dbcontext;

        public DepartmintRepository(ApplicationDpContext dbcontext) : base(dbcontext)
        {
            //_dbcontext = dbcontext;
            
        }

       
    }
}
