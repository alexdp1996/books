using DataInfrastructure.Entities;
using System.Collections.Generic;

namespace DataInfrastructure.Interfaces
{
    public interface IBookRepo : ICRUD<BookEM, long>
    {
        void UpdateAuthors(long bookId, IEnumerable<long> authorIds);
        DataTableResponseEM<BookEM> Get(DataTableRequestEM model);
    }
}
