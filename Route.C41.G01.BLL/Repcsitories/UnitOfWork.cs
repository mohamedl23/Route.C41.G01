using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.BLL.Repcsitories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDpContext _dbContext;
        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmintRepository DepartmintRepository { get ; set ; }

        public UnitOfWork(ApplicationDpContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmintRepository = new DepartmintRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task< int> Complite()
            =>_dbContext.SaveChanges();
        public void Dispose() 
            => _dbContext.Dispose();
    }
}
