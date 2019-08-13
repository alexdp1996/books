using Entities;
using System;

namespace Data.Repositories
{
    public abstract class BaseRepo<Entity> : IDisposable where Entity : BaseEM
    {
        protected DataContext DataContext { get; }

        public BaseRepo(DataContext context)
        {
            DataContext = context;
        }

        public abstract Entity Get(long id);

        public void Update(Entity entity)
        {
            var entry = Get(entity.Id);
            DataContext.Entry(entry).CurrentValues.SetValues(entity);
            DataContext.SaveChanges();
        }

        public void Delete(long id)
        {
            DataContext.Set<Entity>().Remove(Get(id));
            DataContext.SaveChanges();
        }

        public void Add(Entity entity)
        {
            DataContext.Set<Entity>().Add(entity);
            DataContext.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
