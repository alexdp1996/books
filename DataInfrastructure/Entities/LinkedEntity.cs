namespace DataInfrastructure.Entities
{
    public class LinkedEntity<T> 
    {
        public long LinkedId { get; set; }
        public T Entity { get; set; }
    }
}
