using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class DataTableResponseEM<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
    }
}
