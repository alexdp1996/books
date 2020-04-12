Imports Dapper
Imports Data.Extensions
Imports Dommel
Imports EntityModels
Imports Infrastructure.Data
Imports [Shared].Services

Namespace Repositories

    Public Class BookRepo
        Inherits BaseRepo
        Implements IBookRepo

        Public Sub UpdateAuthors(bookId As Long, authorIds As IEnumerable(Of Long)) Implements IBookRepo.UpdateAuthors
            Using con = Connection
                Dim SP = "USPBookUpdateAuthors"
                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@BookId", bookId)
                queryParameters.Add("@AuthorIds", authorIds.AsEnumerableParameter())

                con.Query(SP, queryParameters, commandType:=CommandType.StoredProcedure)
            End Using
        End Sub

        Public Sub Update(entity As BookEM) Implements ICRUD(Of BookEM, Long).Update
            Using con = Connection
                con.Update(entity)
            End Using
        End Sub

        Public Sub Delete(id As Long) Implements ICRUD(Of BookEM, Long).Delete
            Using con = Connection
                Dim SP = "USPBookDelete"
                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@Id", id)

                con.Query(SP, queryParameters, commandType:=CommandType.StoredProcedure)
            End Using
        End Sub

        Public Function GetList(model As DataTableRequestEM) As DataTableResponseEM(Of BookEM) Implements IBookRepo.GetList
            Using con = Connection
                Dim SPname = "USPBookGetList"

                Dim order = model.Order(0)
                Dim orderColumnName = model.Columns(order.Column).Name

                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@Start", model.Start)
                queryParameters.Add("@Lenght", model.Length)
                queryParameters.Add("@OrderColumnName", orderColumnName)
                queryParameters.Add("@IsAsc", order.IsAcs)

                Dim reader = con.QueryMultiple(SPname, queryParameters, commandType:=CommandType.StoredProcedure)
                Dim generalInfo = reader.ReadSingle(Of DataTableResponseGeneralEM)()

                Dim datatableResponse = New DataTableResponseEM(Of BookEM)()
                datatableResponse.RecordsFiltered = generalInfo.RecordsFiltered
                datatableResponse.RecordsTotal = generalInfo.RecordsTotal


                Dim books = reader.Read(Of BookEM)().AsList()

                Dim authorBook = reader.Read(Of AuthorBookEM)().AsList()

                Dim authors = reader.Read(Of AuthorEM)().AsList()

                For Each b In books
                    Dim authorIds = authorBook.Where(Function(ab) ab.BookId = b.Id.Value).Select(Function(s) s.AuthorId).ToList()
                    Dim authorsToBook = authors.Where(Function(a) authorIds.Contains(a.Id.Value))
                    b.Authors.AddRange(authorsToBook)
                Next

                datatableResponse.Data = books

                Return datatableResponse
            End Using
        End Function

        Public Function [Get](id As Long) As BookEM Implements ICRUD(Of BookEM, Long).Get
            Using con = Connection
                Dim SP = "USPBookGet"
                Dim queryParameters = New DynamicParameters()
                queryParameters.Add("@id", id)

                Dim result = con.QueryMultiple(SP, queryParameters, commandType:=CommandType.StoredProcedure)

                Dim mappingObject = New BookMappingObjectEM()

                mappingObject.Book = result.ReadSingleOrDefault(Of BookEM)()
                mappingObject.Authors = result.Read(Of AuthorEM)().AsList()

                Dim book = MapperService.Map(Of BookEM)(mappingObject)

                Return book
            End Using
        End Function

        Public Function Create(entity As BookEM) As Long Implements ICRUD(Of BookEM, Long).Create
            Using con = Connection
                Dim result = con.Insert(entity)
                Dim converted = CLng(result)
                Return converted
            End Using
        End Function
    End Class

End Namespace
