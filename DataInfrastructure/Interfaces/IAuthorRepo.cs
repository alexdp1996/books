using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IAuthorRepo : IBaseEntityRepo<AuthorEM>
    {
        IEnumerable<AuthorEM> Get(IEnumerable<long> Ids);
        IEnumerable<AuthorEM> Get(string term);
        DataTableResponseEM<AuthorEM> GetByNameAsc(DataTableRequestEM model);
        DataTableResponseEM<AuthorEM> GetByNameDesc(DataTableRequestEM model);
        DataTableResponseEM<AuthorEM> GetBySurnameAsc(DataTableRequestEM model);
        DataTableResponseEM<AuthorEM> GetBySurnameDesc(DataTableRequestEM model);
        DataTableResponseEM<AuthorEM> GetByAmountOfBooksAsc(DataTableRequestEM model);
        DataTableResponseEM<AuthorEM> GetByAmountOfBooksDesc(DataTableRequestEM model);
    }
}
