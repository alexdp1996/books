using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IBookRepo : IBaseRepo<BookEM>
    {
        long Save(UpdatableBookEM book);
        IEnumerable<BookEM> Get(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
    }
}
