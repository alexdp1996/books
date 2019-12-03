using System;
using ViewModels;

namespace LogicInfastructure.Interfaces
{
    public interface IBookDM : IDisposable
    {
        void Delete(long id);
        void Save(BookEditVM model);
        BookVM Get(long? id);
        DataTableResponseVM<BookVM> Get(DataTableRequestVM model);
    }
}
