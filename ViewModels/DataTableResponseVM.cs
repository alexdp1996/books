using System.Collections.Generic;

namespace ViewModels
{
#pragma warning disable IDE1006 // Naming Styles
    public class DataTableResponseVM<T>
    {
        public IEnumerable<T> data { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public int draw { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
