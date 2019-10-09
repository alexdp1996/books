using DataInfrastructure.Entities;
using ViewModels;

namespace Shared.Services
{
    public static class MapperService
    {
        static MapperService()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<BookEditVM, UpdatableBookEM>().ReverseMap();
                mapper.CreateMap<BookEditVM, BookVM>();
                mapper.CreateMap<BookEM, BookBaseVM>();
                mapper.CreateMap<BookEM, BookVM>();
                mapper.CreateMap<AuthorEM, AuthorBaseVM>().ForMember(d => d.CountOfBooks, o => o.MapFrom(s => s.Books.Count)).ReverseMap();
                mapper.CreateMap<AuthorEM, AuthorVM>().ForMember(d => d.CountOfBooks, o => o.MapFrom(s => s.Books.Count));

                //DataTable
                mapper.CreateMap<ColumnVM, ColumnEM>();
                mapper.CreateMap<SearchVM, SearchEM>();
                mapper.CreateMap<DataTableRequestVM, DataTableRequestEM>();
                mapper.CreateMap<DataTableResponseEM<BookEM>, DataTableResponseVM<BookVM>>();
                mapper.CreateMap<DataTableResponseEM<AuthorEM>, DataTableResponseVM<AuthorBaseVM>>();
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public static T Map<T>(object source)
        {
            return AutoMapper.Mapper.Map<T>(source);
        }
    }
}
