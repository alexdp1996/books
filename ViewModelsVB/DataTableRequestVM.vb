REM Start - JSon class sent from Datatables

Public Class DataTableRequestVM
    Public Property Draw As Integer
    Public Property Start As Integer
    Public Property Length As Integer
    Public Property Columns As List(Of ColumnVM)
    Public Property Search As SearchVM
    Public Property Order As List(Of OrderVM)
End Class

Public Class ColumnVM
    Public Property Data As String
    Public Property Name As String
    Public Property Searchable As Boolean
    Public Property Orderable As Boolean
    Public Property Search As SearchVM
End Class

Public Class SearchVM
    Public Property Value As String
    Public Property Regex As String
End Class


Public Class OrderVM
    Public Property Column As Integer
    Public Property Dir As String
End Class

REM End- JSon class sent from Datatables