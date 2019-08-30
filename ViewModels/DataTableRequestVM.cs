using System.Collections.Generic;

namespace ViewModels
{
    ///Start - JSon class sent from Datatables
    
    public class DataTableRequestVM
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<ColumnVM> Columns { get; set; }
        public SearchVM Search { get; set; }
        public List<OrderVM> Order { get; set; }
    }

    public class ColumnVM
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public SearchVM Search { get; set; }
    }

    public class SearchVM
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class OrderVM
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    /// End- JSon class sent from Datatables 
}