using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class DbService(IFlightDbContext dbContext) : IDbService
    {
        protected readonly IFlightDbContext _dbContext = dbContext;

        public T Create<T>(T entity) where T : Entity
        {
            var createdEntity = _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return createdEntity.Entity;
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteAll<T>() where T : Entity
        {
            _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>());
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
           return _dbContext.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return _dbContext.Set<T>().SingleOrDefault(entity => entity.Id == id);
        }

        public void Update<T>(T entity) where T : Entity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
