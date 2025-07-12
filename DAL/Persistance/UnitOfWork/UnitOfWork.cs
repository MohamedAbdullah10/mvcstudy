using DAL.Entities.Employees;
using DAL.Persistance.Data;
using DAL.Persistance.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //public EmployeeRepo Repo => new EmployeeRepo(_context);

        private readonly AppDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(AppDbContext context) {

            _context = context;
        
        
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null) { 
            
                 _repositories= new Hashtable();

            }
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type)) {
                var repotype = typeof(GenericRepository<>);

                var reposinstance = Activator.CreateInstance(repotype.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, reposinstance);


            }
            return (IGenericRepository<T>)_repositories[type];

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
                _context.Dispose();
        }

       
    }
}
