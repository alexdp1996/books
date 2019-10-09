using AutoMapper;
using DataInfrastructure.Entities;

namespace Shared.MapperResolvers
{
    public class AuthorResolver : ITypeConverter<AuthorMappingObjectEM, AuthorEM>
    {
        public AuthorEM Convert(AuthorMappingObjectEM source, AuthorEM destination, ResolutionContext context)
        {
            if (source.Author == null)
            {
                return null;
            }
            else
            {
                destination = source.Author;
                destination.Books = source.Books;
                return destination;
            }
        }
    }
}
