using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IAuthorRepo : IBaseRepo<AuthorEM>
    {
        IEnumerable<AuthorEM> Get(IEnumerable<long> Ids);
        IEnumerable<AuthorEM> Get(string term);
        IEnumerable<AuthorEM> Get(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered);
    }
}
