using System.Data.Entity;

namespace Data
{
    public class DataContextInitializer : CreateDatabaseIfNotExists<DataContext>
    {
    }
}
