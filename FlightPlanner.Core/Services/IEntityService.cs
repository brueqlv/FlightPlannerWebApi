using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        T Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Clear();
    }
}
