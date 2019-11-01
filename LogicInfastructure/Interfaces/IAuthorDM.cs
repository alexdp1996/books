using System;
using System.Collections.Generic;
using ViewModels;

namespace LogicInfastructure.Interfaces
{
    public interface IAuthorDM : IDisposable
    {
        IEnumerable<AuthorBaseVM> Get(string term);
        IEnumerable<AuthorBaseVM> Get(IEnumerable<long> ids);
        DataTableResponseVM<AuthorBaseVM> Get(DataTableRequestVM model);
        AuthorVM Get(long? id);
        void Delete(long id);
        void Save(AuthorVM model);
    }
}
