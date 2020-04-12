Public Class DataTableResponseVM(Of T)
#Disable Warning IDE1006 REM Naming Styles
    Public Property data As IEnumerable(Of T)
    Public Property [error] As String
    Public Property recordsFiltered As Integer
    Public Property recordsTotal As Integer
    Public Property draw As Integer
#Enable Warning IDE1006 REM Naming Styles
End Class
