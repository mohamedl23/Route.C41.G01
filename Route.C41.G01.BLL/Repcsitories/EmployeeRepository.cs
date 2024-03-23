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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDpContext _dbcontext;

        public EmployeeRepository(ApplicationDpContext dbcontext)
        {
            _dbcontext = dbcontext;

        }

        public int Add(Employee entity)
        {
            _dbcontext.Add(entity);
            return _dbcontext.SaveChanges();
        }
        public int Update(Employee entity)
        {
            _dbcontext.Update(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbcontext.Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public Employee Get(int id)
        {
            return _dbcontext.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbcontext.Employees.AsNoTracking().ToList();
        }
    }
}
