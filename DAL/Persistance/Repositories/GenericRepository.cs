using DAL.Entities.Common.Interfaces;
using DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistance.Repositories
{
 
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(T entity)
        {
           _context.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            if (entity is ISoftDelete delete) {

                delete.IsDeleted = true;
                _context.Update(entity);
                


            }
            else { 
                _context.Remove(entity);
                 }
            return _context.SaveChanges();
        }

        public T? Get(int id)
        {

            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if(WithAsNoTracking)
                return _context.Set<T>().AsNoTracking().ToList();
            else
                return _context.Set<T>().ToList();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _context.Set<T>();
        }

        public int Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();  
        }
    }
}
