Imports Dapper.FluentMap.Dommel.Mapping
Imports EntityModels

Namespace Maps

    Public Class AuthorMap
        Inherits DommelEntityMap(Of AuthorEM)

        Public Sub New()
            ToTable("Author")
            Map(Function(a) a.Id).IsIdentity()
            Map(Function(a) a.Books).Ignore()
        End Sub

    End Class

End Namespace
