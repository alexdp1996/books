using System.Data.Entity;

namespace Data
{
    internal class DataContextInitializer : CreateDatabaseIfNotExists<DataContext>
    {
    }
}
