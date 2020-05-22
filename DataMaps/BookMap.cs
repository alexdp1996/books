using Dapper.FluentMap.Dommel.Mapping;
using EntityModels;

namespace DataMaps
{
    public class BookMap : DommelEntityMap<BookEM>
    {
        public BookMap()
        {
            ToTable("Book");
            Map(b => b.Id).IsIdentity();
            Map(b => b.Authors).Ignore();
        }
    }
}
