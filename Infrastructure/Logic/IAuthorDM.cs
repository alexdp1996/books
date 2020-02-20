using System;
using System.Collections.Generic;
using ViewModels;

namespace Infrastructure.Logic
{
    public interface IAuthorDM : IDisposable
    {
        IEnumerable<AuthorVM> Get(string term);
        DataTableResponseVM<AuthorVM> GetList(DataTableRequestVM model);
        AuthorVM Get(long? id);
        void Delete(long id);
        void Update(AuthorVM model);
        void Create(AuthorVM model);
    }
}
