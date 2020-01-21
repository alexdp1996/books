using System.Collections.Generic;

namespace EntityModels
{
    public class DataTableRequestEM
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public List<ColumnEM> Columns { get; set; }
        public SearchEM Search { get; set; }

        public List<OrderEM> Order { get; set; }
    }

    public class OrderEM
    {
        public int Column { get; set; }
        public bool IsAcs { get; set; }
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
}
