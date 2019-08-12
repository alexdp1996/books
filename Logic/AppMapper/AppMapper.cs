using Entities;
using System.Linq;
using ViewModels;

namespace Logic.AppMapper
{
    public class MapperInit
    {
        static public void Init()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<BookBaseVM, BookEM>().ReverseMap();
                mapper.CreateMap<BookEM, BookVM>();
                mapper.CreateMap<BookEM, BookEditVM>().ForMember(d => d.AuthorIds, o => o.MapFrom(s => s.Authors.Select(a => a.Id)));
                mapper.CreateMap<AuthorEM, AuthorBaseVM>().ForMember(d => d.CountOfBooks, o => o.MapFrom(s => s.Books.Count)).ReverseMap();
                mapper.CreateMap<AuthorEM, AuthorVM>().ForMember(d => d.CountOfBooks, o => o.MapFrom(s => s.Books.Count));

                //DataTable
                mapper.CreateMap<ColumnVM, ColumnEM>();
                mapper.CreateMap<OrderVM, OrderEM>().ForMember(d => d.Asc, o => o.MapFrom(s => s.Dir == "asc"));
                mapper.CreateMap<SearchVM, SearchEM>();
                mapper.CreateMap<DataTableVM, DataTableEM>();
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
