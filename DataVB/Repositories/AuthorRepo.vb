Imports Dapper
Imports Dommel
Imports EntityModels
Imports Infrastructure.Data
Imports [Shared].Services

Namespace Repositories

    Public Class AuthorRepo
        Inherits BaseRepo
        Implements IAuthorRepo

        Public Sub Update(entity As AuthorEM) Implements ICRUD(Of AuthorEM, Long).Update
            Using con = Connection
                con.Update(entity)
            End Using
        End Sub

        Public Sub Delete(id As Long) Implements ICRUD(Of AuthorEM, Long).Delete
            Using con = Connection
                Dim SP = "USPAuthorDelete"
                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@Id", id)

                con.Query(SP, queryParameters, commandType:=CommandType.StoredProcedure)
            End Using
        End Sub

        Public Function [Get](term As String) As IEnumerable(Of AuthorEM) Implements IAuthorRepo.Get
            Using con = Connection
                Dim SP = "USPAuthorGetByTerm"
                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@Term", term)

                Dim authors = con.Query(Of AuthorEM)(SP, queryParameters, commandType:=CommandType.StoredProcedure)

                Return authors
            End Using
        End Function

        Public Function [Get](id As Long) As AuthorEM Implements ICRUD(Of AuthorEM, Long).Get
            Using con = Connection
                Dim SP = "USPAuthorGet"
                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@Id", id)

                Dim result = con.QueryMultiple(SP, queryParameters, commandType:=CommandType.StoredProcedure)

                Dim mappingObject = New AuthorMappingObjectEM()

                mappingObject.Author = result.ReadSingleOrDefault(Of AuthorEM)()
                mappingObject.Books = result.Read(Of BookEM)().AsList()

                Dim author = MapperService.Map(Of AuthorEM)(mappingObject)

                Return author
            End Using
        End Function

        Public Function GetList(model As DataTableRequestEM) As DataTableResponseEM(Of AuthorEM) Implements IAuthorRepo.GetList
            Using con = Connection
                Dim SPname = "USPAuthorGetList"

                Dim order = model.Order(0)
                Dim orderColumnName = model.Columns(order.Column).Name

                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@Start", model.Start)
                queryParameters.Add("@Lenght", model.Length)
                queryParameters.Add("@OrderColumnName", orderColumnName)
                queryParameters.Add("@IsAsc", order.IsAcs)

                Dim reader = con.QueryMultiple(SPname, queryParameters, commandType:=CommandType.StoredProcedure)
                Dim generalInfo = reader.ReadSingle(Of DataTableResponseGeneralEM)()

                Dim datatableResponse = New DataTableResponseEM(Of AuthorEM)()
                datatableResponse.RecordsFiltered = generalInfo.RecordsFiltered
                datatableResponse.RecordsTotal = generalInfo.RecordsTotal

                Dim authors = reader.Read(Of AuthorEM)().AsList()

                Dim authorBook = reader.Read(Of AuthorBookEM)().AsList()

                Dim books = reader.Read(Of BookEM)().AsList()

                For Each a In authors
                    Dim bookIds = authorBook.Where(Function(ab) ab.AuthorId = a.Id.Value).Select(Function(s) s.BookId).ToList()
                    Dim booksToAdd = books.Where(Function(b) bookIds.Contains(b.Id.Value))
                    a.Books.AddRange(booksToAdd)
                Next

                datatableResponse.Data = authors

                Return datatableResponse
            End Using
        End Function

        Public Function Create(entity As AuthorEM) As Long Implements ICRUD(Of AuthorEM, Long).Create
            Using con = Connection
                Dim result = con.Insert(entity)
                Dim converted = CLng(result)
                Return converted
            End Using
        End Function
    End Class

End Namespace
