using System.Collections.Generic;

namespace EntityModels
{
    public class DataTableResponseEM<T> : DataTableResponseGeneralEM
    {
        public IEnumerable<T> Data { get; set; }
        
    }
}
