namespace ViewModels
{
#pragma warning disable IDE1006 // Naming Styles
    public class DataTableResponseVM
    {
        public object data { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public int draw { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
