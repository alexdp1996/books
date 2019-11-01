using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;

namespace Data.Repositories
{
    public abstract class BaseEntityRepo<Entity> : IBaseEntityRepo<Entity> where Entity : BaseEM
    {
        protected DataContext DataContext { get; }

        internal BaseEntityRepo(DataContext context)
        {
            DataContext = context;
        }

        public BaseEntityRepo()
        {
            DataContext = new DataContext();
        }

        public abstract Entity Get(long id);

        public long Add(Entity entity)
        {
            DataContext.Set<Entity>().Add(entity);
            DataContext.SaveChanges();
            return entity.Id.Value;
        }

        public void Update(Entity entity)
        {
            var entry = Get(entity.Id.Value);
            DataContext.Entry(entry).CurrentValues.SetValues(entity);
            DataContext.SaveChanges();
        }

        public void Delete(long id)
        {
            DataContext.Set<Entity>().Remove(Get(id));
            DataContext.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
