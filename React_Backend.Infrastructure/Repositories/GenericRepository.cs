using Microsoft.EntityFrameworkCore;
using React_Backend.Domain.Interfaces;
using React_Backend.Infrastructure.Context;
using System.Linq.Expressions;

namespace React_Backend.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

     
        public T GetByGuid(Expression<Func<T, bool>> predicate)
        {
            return _applicationDbContext.Set<T>().FirstOrDefault(predicate);

        }


        public List<T> GetAll()
        {
            return _applicationDbContext.Set<T>().ToList();
        }

        public T GetById(object id)
        {
            return _applicationDbContext.Set<T>().Find(id);
        }


        public T Create(T obj)
        {
            _applicationDbContext.Set<T>().Add(obj);
            _applicationDbContext.SaveChanges();
            return obj;
        }

        public void Update(T obj)
        {
            _applicationDbContext.Set<T>().Attach(obj);
            _applicationDbContext.Entry(obj).State = EntityState.Modified;
            _applicationDbContext.SaveChanges();
        }

        public void Delete(T obj)
        {
            _applicationDbContext.Set<T>().Remove(obj);
            _applicationDbContext.SaveChanges();
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
        


    }
}
