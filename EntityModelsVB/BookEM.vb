Public Class BookEM
    Public Property Id As Long?
    Public Property Name As String
    Public Property Rate As Integer
    Public Property Pages As Integer
    Public Property CreatedDate As DateTime
    Public Property Authors As List(Of AuthorEM)

    Public Sub New()
        Authors = New List(Of AuthorEM)()
    End Sub
End Class
