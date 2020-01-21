using EntityModels;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public interface IBookRepo : ICRUD<BookEM, long>
    {
        void UpdateAuthors(long bookId, IEnumerable<long> authorIds);
        DataTableResponseEM<BookEM> Get(DataTableRequestEM model);
    }
}
