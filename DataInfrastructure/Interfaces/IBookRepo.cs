using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IBookRepo : IBaseEntityRepo<BookEM>
    {
        void UpdateAuthors(long bookId, IEnumerable<long> authorIds);
        DataTableResponseEM<BookEM> GetByNameAsc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByNameDesc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByPagesAsc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByPagesDesc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByRateAsc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByRateDesc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByDateAsc(DataTableRequestEM model);
        DataTableResponseEM<BookEM> GetByDateDesc(DataTableRequestEM model);
    }
}
