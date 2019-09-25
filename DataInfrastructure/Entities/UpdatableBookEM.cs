using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class UpdatableBookEM : BookEM
    {
        public IEnumerable<long> AuthorIds { get; set; }
    }
}
