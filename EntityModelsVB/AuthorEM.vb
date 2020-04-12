Public Class AuthorEM
    Public Property Id As Long?
    Public Property Name As String
    Public Property Surname As String
    Public Property Books As List(Of BookEM)

    Public Sub New()
        Books = New List(Of BookEM)()
    End Sub
End Class
