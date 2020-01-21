using EntityModels;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public interface IAuthorRepo : ICRUD<AuthorEM, long>
    {
        IEnumerable<AuthorEM> Get(IEnumerable<long> Ids);
        IEnumerable<AuthorEM> Get(string term);
        DataTableResponseEM<AuthorEM> Get(DataTableRequestEM model);
    }
}
