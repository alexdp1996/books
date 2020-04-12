using AutoMapper;
using EntityModels;
using Shared.MapperResolvers;
using ViewModels;

namespace Shared.Services
{
    public static class MapperService
    {
        private static IMapper _mapper;

        static MapperService()
        {
            _mapper = new MapperConfiguration(mapper =>
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
            }).CreateMapper();
        }

        public static T Map<T>(object source)
        {
            return _mapper.Map<T>(source);
        }
    }
}
