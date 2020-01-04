﻿using DataInfrastructure.Entities;
using System;

namespace DataInfrastructure.Interfaces
{
    public interface ICRUD<TEntity, TKey> : IDisposable
    {
        TEntity Get(TKey id);
        TKey Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}