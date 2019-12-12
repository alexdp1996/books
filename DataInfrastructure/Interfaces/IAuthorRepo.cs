using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IAuthorRepo : IBaseEntityRepo<AuthorEM>
    {
        IEnumerable<AuthorEM> Get(IEnumerable<long> Ids);
        IEnumerable<AuthorEM> Get(string term);
        DataTableResponseEM<AuthorEM> Get(DataTableRequestEM model);
    }
}
