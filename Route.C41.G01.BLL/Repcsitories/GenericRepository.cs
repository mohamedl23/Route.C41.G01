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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly ApplicationDpContext _dbcontext;

        public GenericRepository(ApplicationDpContext dbcontext) 
        {
            _dbcontext = dbcontext;

        }

        public int Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
            return _dbcontext.SaveChanges();
        }
        public int Update(T entity)
        {
            _dbcontext.Update(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbcontext.Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().AsNoTracking().ToList();
        }
    }
}
