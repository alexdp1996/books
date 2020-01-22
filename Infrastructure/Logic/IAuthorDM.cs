using System;
using System.Collections.Generic;
using ViewModels;

namespace Infrastructure.Logic
{
    public interface IAuthorDM : IDisposable
    {
        IEnumerable<AuthorBaseVM> Get(string term);
        DataTableResponseVM<AuthorBaseVM> GetList(DataTableRequestVM model);
        AuthorVM Get(long? id);
        void Delete(long id);
        void Update(AuthorVM model);
        void Create(AuthorVM model);
    }
}
