using AutoMapper;
using DataInfrastructure.Entities;

namespace Shared.MapperResolvers
{
    public class BookResolver : ITypeConverter<BookMappingObjectEM, BookEM>
    {
        public BookEM Convert(BookMappingObjectEM source, BookEM destination, ResolutionContext context)
        {
            if (source.Book == null)
            {
                return null;
            }
            else
            {
                destination = source.Book;
                destination.Authors = source.Authors;
                return destination;
            }
        }
    }
}
