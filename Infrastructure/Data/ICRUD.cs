﻿using System;

namespace Infrastructure.Data
{
    public interface ICRUD<TEntity, TKey> : IDisposable
    {
        TEntity Get(TKey id);
        TKey Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}
