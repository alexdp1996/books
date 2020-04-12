Public Class DataTableRequestEM
    Public Property Start As Integer
    Public Property Length As Integer
    Public Property Columns As List(Of ColumnEM)
    Public Property Search As SearchEM
    Public Property Order As List(Of OrderEM)
End Class

Public Class ColumnEM
    Public Property Data As String
    Public Property Name As String
    Public Property Searchable As Boolean
    Public Property Orderable As Boolean
    Public Property Search As SearchEM
End Class

Public Class SearchEM
    Public Property Value As String
    Public Property Regex As String
End Class


Public Class OrderEM
    Public Property Column As Integer
    Public Property IsAcs As Boolean
End Class