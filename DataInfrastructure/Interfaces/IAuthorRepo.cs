using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IAuthorRepo : IBaseRepo<AuthorEM>
    {
        IEnumerable<AuthorEM> Get(IEnumerable<long> Ids);
        IEnumerable<AuthorEM> Get(string term);
        IEnumerable<AuthorEM> GetByNameAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<AuthorEM> GetByNameDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<AuthorEM> GetBySurnameAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<AuthorEM> GetBySurnameDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<AuthorEM> GetByAmountOfBooksAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
        IEnumerable<AuthorEM> GetByAmountOfBooksDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
    }
}
