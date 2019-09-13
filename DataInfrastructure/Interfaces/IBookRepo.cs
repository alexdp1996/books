using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IBookRepo : IBaseRepo<BookEM>
    {
        long Save(UpdatableBookEM book);
        IEnumerable<BookEM> GetByNameAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByNameDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByPagesAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByPagesDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByRateAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByRateDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByDateAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<BookEM> GetByDateDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
    }
}
