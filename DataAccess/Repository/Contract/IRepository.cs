using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Contract
{
    internal interface IRepository<TEntity, TKey> where TEntity : class
    {
        TEntity Get(TKey key);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddAll(IEnumerable<TEntity> entities);
        void Update(TEntity entity, TKey key);
        void UpdateAll(IEnumerable<TEntity> entities);
        void Delete(TKey key);
        void DeleteAll(IEnumerable<TKey> entities);
    }
}
