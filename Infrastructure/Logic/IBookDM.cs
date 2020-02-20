using System;
using ViewModels;

namespace Infrastructure.Logic
{
    public interface IBookDM : IDisposable
    {
        void Delete(long id);
        void Update(BookVM model);
        void Create(BookVM model);
        BookVM Get(long? id);
        DataTableResponseVM<BookVM> GetList(DataTableRequestVM model);
    }
}
