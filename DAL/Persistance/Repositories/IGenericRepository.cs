    using DAL.Persistance.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace DAL.Persistance.Repositories
    {
        public interface IGenericRepository<T>where T:class
        {
       

            Task<IEnumerable<T>> GetAll(bool WithAsNoTracking=true);
            IQueryable<T> GetAllQueryable();
            Task<T>? Get(int id);
            void Add(T entity);
            void Update(T entity);
            void Delete(T entity);

        }
    }
