Imports AutoMapper
Imports EntityModels

Namespace Resolvers
    Public Class AuthorResolver
        Implements ITypeConverter(Of AuthorMappingObjectEM, AuthorEM)

        Public Function Convert(source As AuthorMappingObjectEM, destination As AuthorEM, context As ResolutionContext) As AuthorEM Implements ITypeConverter(Of AuthorMappingObjectEM, AuthorEM).Convert
            If source.Author Is Nothing Then
                Return Nothing
            Else
                destination = source.Author
                destination.Books = source.Books
                Return destination
            End If
        End Function
    End Class
End Namespace

