namespace ViewModels
{
    public class DataTableDataVM
    {
        public object data { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public int draw { get; set; }
    }
}
