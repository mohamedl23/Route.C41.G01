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
    public class DepartmintRepository : IDepartmintRepository
    {
        private readonly ApplicationDpContext _dbcontext;

        public DepartmintRepository(ApplicationDpContext dbcontext)
        {
            _dbcontext = dbcontext;
            
        }

        public int Add(Department entity)
        {
            _dbcontext.Add(entity);
            return _dbcontext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbcontext.Update(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbcontext.Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public Department Get(int id)
        {
            return _dbcontext.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbcontext.Departments.AsNoTracking().ToList();
        }

       
    }
}
