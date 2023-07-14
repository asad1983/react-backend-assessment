
using System.Linq.Expressions;

namespace React_Backend.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(object id);
        T Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Save();
        T GetByGuid(Expression<Func<T, bool>> predicate);
    }
}
