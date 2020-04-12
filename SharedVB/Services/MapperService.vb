Imports AutoMapper
Imports EntityModels
Imports [Shared].Resolvers
Imports ViewModels

Namespace Services
    Public Class MapperService
        Private Shared _mapper As IMapper

        Shared Sub New()
            Dim cfg = New MapperConfiguration(Sub(mapper)
                                                  mapper.CreateMap(Of AuthorEM, AuthorVM)().ForMember(Function(d) d.CountOfBooks, Sub(o) o.MapFrom(Function(s) s.Books.Count)).ReverseMap()
                                                  mapper.CreateMap(Of BookEM, BookVM)().ReverseMap()

                                                  REM DataTable
                                                  mapper.CreateMap(Of ColumnVM, ColumnEM)()
                                                  mapper.CreateMap(Of SearchVM, SearchEM)()
                                                  mapper.CreateMap(Of OrderVM, OrderEM)().ForMember(Function(d) d.IsAcs, Sub(o) o.MapFrom(Function(s) s.Dir = "asc"))

                                                  mapper.CreateMap(Of DataTableRequestVM, DataTableRequestEM)()
                                                  mapper.CreateMap(Of DataTableResponseEM(Of BookEM), DataTableResponseVM(Of BookVM))()
                                                  mapper.CreateMap(Of DataTableResponseEM(Of AuthorEM), DataTableResponseVM(Of AuthorVM))()

                                                  mapper.CreateMap(Of BookMappingObjectEM, BookEM)().ConvertUsing(Of BookResolver)()
                                                  mapper.CreateMap(Of AuthorMappingObjectEM, AuthorEM)().ConvertUsing(Of AuthorResolver)()
                                              End Sub)
            _mapper = cfg.CreateMapper()
        End Sub

        Public Shared Function Map(Of T)(source As Object) As T
            Return _mapper.Map(Of T)(source)
        End Function

    End Class
End Namespace
