using EntityModels;
using Shared.MapperResolvers;
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
                mapper.CreateMap<AuthorEM, AuthorVM>().ForMember(d => d.CountOfBooks, o => o.MapFrom(s => s.Books.Count)).ReverseMap();
                mapper.CreateMap<BookEM, BookVM>().ReverseMap();

                //DataTable
                mapper.CreateMap<ColumnVM, ColumnEM>();
                mapper.CreateMap<SearchVM, SearchEM>();
                mapper.CreateMap<OrderVM, OrderEM>().ForMember(d => d.IsAcs, o => o.MapFrom(s => s.Dir == "asc"));

                mapper.CreateMap<DataTableRequestVM, DataTableRequestEM>();
                mapper.CreateMap<DataTableResponseEM<BookEM>, DataTableResponseVM<BookVM>>();
                mapper.CreateMap<DataTableResponseEM<AuthorEM>, DataTableResponseVM<AuthorVM>>();

                mapper.CreateMap<BookMappingObjectEM, BookEM>().ConvertUsing<BookResolver>();
                mapper.CreateMap<AuthorMappingObjectEM, AuthorEM>().ConvertUsing<AuthorResolver>();
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public static T Map<T>(object source)
        {
            return AutoMapper.Mapper.Map<T>(source);
        }
    }
}
