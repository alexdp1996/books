using EntityModels;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public interface IAuthorRepo : ICRUD<AuthorEM, long>
    {
        IEnumerable<AuthorEM> Get(string term);
        DataTableResponseEM<AuthorEM> GetList(DataTableRequestEM model);
    }
}
