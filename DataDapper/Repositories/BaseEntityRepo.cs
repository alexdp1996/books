using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using Dommel;

namespace DataDapper.Repositories
{
    public abstract class BaseEntityRepo<TEntity> : BaseRepo, IBaseEntityRepo<TEntity> where TEntity : BaseEM
    {
        public long Add(TEntity entity)
        {
            using (var con = Connection)
            {
                var result = con.Insert(entity);
                return long.Parse(result.ToString());
            }
        }

        public void Update(TEntity entity)
        {
            using (var con = Connection)
            {
                con.Update(entity);
            }
        }

        public abstract TEntity Get(long id);

        public abstract void Delete(long id);
    }
}
