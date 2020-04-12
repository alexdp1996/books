Imports Dapper.FluentMap.Dommel.Mapping
Imports EntityModels

Namespace Maps

    Public Class BookMap
        Inherits DommelEntityMap(Of BookEM)

        Public Sub New()
            ToTable("Book")
            Map(Function(b) b.Id).IsIdentity()
            Map(Function(b) b.Authors).Ignore()
        End Sub
    End Class

End Namespace

