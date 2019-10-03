using DataInfrastructure.Entities;
using Dommel;

namespace DataDapper.Repositories
{
    public class BaseEntityRepo<TEntity> : BaseRepo where TEntity : BaseEM
    {
        private long Insert(TEntity entity)
        {
            using (var con = Connection)
            {
                var result = con.Insert(entity);
                return long.Parse(result.ToString());
            }
        }

        private void Update(TEntity entity)
        {
            using (var con = Connection)
            {
                con.Update(entity);
            }
        }

        public long Save(TEntity entity)
        {
            if (entity.Id != 0)
            {
                Update(entity);
                return entity.Id;
            }
            else
            {
                var id = Insert(entity);
                return id;
            }
        }
    }
}
