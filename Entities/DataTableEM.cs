using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DataTableEM
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public List<ColumnEM> Columns { get; set; }
        public SearchEM Search { get; set; }
        public List<OrderEM> Order { get; set; }
    }

    public class ColumnEM
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public SearchEM Search { get; set; }
    }

    public class SearchEM
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class OrderEM
    {
        public int Column { get; set; }
        public bool Asc { get; set; }
    }
}
