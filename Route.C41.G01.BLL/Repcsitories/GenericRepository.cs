﻿using Microsoft.EntityFrameworkCore;
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

        public async Task Add(T entity)
           => await _dbcontext.Set<T>().AddAsync(entity);

        public void Update(T entity)
         =>  _dbcontext.Set<T>().Update(entity);



        public void Delete(T entity)
          =>  _dbcontext.Set<T>().Remove(entity);

        public async Task<T> Get(int id)
           => await _dbcontext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable <T> )await _dbcontext.Employees.Include(E => E.Department).ToListAsync();
            }
            else
            {
                return _dbcontext.Set<T>().AsNoTracking().ToList();
            }
            
        }
    }
}
