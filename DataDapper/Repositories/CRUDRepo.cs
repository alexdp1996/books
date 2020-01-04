using DataInfrastructure.Interfaces;
using Dommel;
using System;

namespace DataDapper.Repositories
{
    public abstract class CRUDRepo<TEntity, TKey> : BaseRepo, ICRUD<TEntity, TKey> where TEntity : class
    {
        public TKey Add(TEntity entity)
        {
            using (var con = Connection)
            {
                var result = con.Insert(entity);
                var converted =  (TKey)Convert.ChangeType(result, typeof(TKey));
                return converted;
            }
        }

        public void Update(TEntity entity)
        {
            using (var con = Connection)
            {
                con.Update(entity);
            }
        }

        public abstract TEntity Get(TKey id);

        public abstract void Delete(TKey id);
    }
}
