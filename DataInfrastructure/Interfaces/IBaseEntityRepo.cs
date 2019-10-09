using DataInfrastructure.Entities;
using System;

namespace DataInfrastructure.Interfaces
{
    public interface IBaseEntityRepo<Entity> : IDisposable where Entity : BaseEM
    {
        Entity Get(long id);
        long Add(Entity entity);
        void Update(Entity entity);
        void Delete(long id);
    }
}
