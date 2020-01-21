using System;
using ViewModels;

namespace Infrastructure.Logic
{
    public interface IBookDM : IDisposable
    {
        void Delete(long id);
        void Update(BookEditVM model);
        void Create(BookEditVM model);
        BookVM Get(long? id);
        DataTableResponseVM<BookVM> Get(DataTableRequestVM model);
    }
}
