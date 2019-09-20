using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class DataTableResponseEM<T> : DataTableResponseGeneralEM
    {
        public IEnumerable<T> Data { get; set; }
        
    }
}
