Imports AutoMapper
Imports EntityModels

Namespace Resolvers
    Public Class BookResolver
        Implements ITypeConverter(Of BookMappingObjectEM, BookEM)

        Public Function Convert(source As BookMappingObjectEM, destination As BookEM, context As ResolutionContext) As BookEM Implements ITypeConverter(Of BookMappingObjectEM, BookEM).Convert
            If source.Book Is Nothing Then
                Return Nothing
            Else
                destination = source.Book
                destination.Authors = source.Authors
                Return destination
            End If

        End Function

    End Class

End Namespace
