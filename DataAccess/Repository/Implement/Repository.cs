using Data.Context;
using DataAccess.Repository.Contract;
using Domain.Model.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Implement
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly IRentContext _context;

        public Repository(IRentContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void AddAll(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
        }

        public void Delete(TKey key)
        {
            TEntity entity = _context.Set<TEntity>().Find(key);
            if(entity != null) _context.Remove(entity);
        }

        public void DeleteAll(IEnumerable<TKey> keys)
        {
            foreach (TKey key in keys)
            {
                Delete(key);
            }
        }

        public TEntity Get(TKey key)
        {
            return _context.Set<TEntity>().Find(key);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity, TKey key)
        {
            if(entity.Key.Equals(key))
            {
                throw new InvalidOperationException("Key in object is not match with key in argument");
            }
            TEntity findEntity = _context.Set<TEntity>().Find(key);
            if(findEntity != null) _context.Entry(findEntity).CurrentValues.SetValues(entity);
        }

        public void UpdateAll(IDictionary<TKey, TEntity> entityEntries)
        {
            foreach (TKey key in entityEntries.Keys)
            {
                Update(entityEntries[key], key);
            }
        }
    }
}
