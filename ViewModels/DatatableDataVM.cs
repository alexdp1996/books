using System.Collections.Generic;

namespace ViewModels
{
    public class DatatableDataVM
    {
        /*
            https://datatables.net/manual/server-side#DataTables_Table_1_wrapper
        */

        public object Data { get; set; }
        public string Error { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public int Draw { get; set; }
    }
}
