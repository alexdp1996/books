using DataInfrastructure.Entities;
using System;

namespace DataInfrastructure.Interfaces
{
    public interface IBaseRepo<Entity> : IDisposable where Entity : BaseEM
    {
        Entity Get(long id);
        long Save(Entity entity);
        void Delete(long id);
    }
}
