using EntityModels;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public interface IBookRepo : ICRUD<BookEM, long>
    {
        void UpdateAuthors(long bookId, IEnumerable<long> authorIds);
        DataTableResponseEM<BookEM> GetList(DataTableRequestEM model);
        IEnumerable<BookEM> GetALL();
    }
}
