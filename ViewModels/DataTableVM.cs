using System.Collections.Generic;

namespace ViewModels
{
    ///Start - JSon class sent from Datatables

    public class DataTableVM
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<ColumnVM> columns { get; set; }
        public SearchVM search { get; set; }
        public List<OrderVM> order { get; set; }
    }

    public class ColumnVM
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchVM search { get; set; }
    }

    public class SearchVM
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class OrderVM
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
    /// End- JSon class sent from Datatables 

}