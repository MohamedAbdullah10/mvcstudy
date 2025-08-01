﻿using DAL.Entities.Common.Interfaces;
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

        public void Add(T entity)
        {
           _context.Add(entity);
            //return _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity is ISoftDelete delete) {

                delete.IsDeleted = true;
                _context.Update(entity);
                


            }
            else { 
                _context.Remove(entity);
                 }
           // return _context.SaveChanges();
        }

        public async Task<T>? Get(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task <IEnumerable<T>> GetAll(bool WithAsNoTracking = true)
        {
            if(WithAsNoTracking)
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            else
                return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            //return _context.SaveChanges();  
        }
    }
}
