using System.Collections.Generic;

namespace ViewModels
{
    public class DatatableDataVM
    {
        /*
            https://datatables.net/manual/server-side#DataTables_Table_1_wrapper
        */

        public object data { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public int draw { get; set; }
    }
}
