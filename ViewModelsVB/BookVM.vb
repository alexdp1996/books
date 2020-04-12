Imports System.ComponentModel.DataAnnotations

Public Class BookVM
    Public Property Id As Long?
    <Required>
    <MinLength(4)>
    <RegularExpression("[^\s]+(.*[^\s]+)*", ErrorMessage:="String must be trimmed")>
    Public Property Name As String
    <Required(ErrorMessage:="Value beetween 1 and 10 is required")>
    <Range(1, 10, ErrorMessage:="Value beetween 1 and 10 is required")>
    Public Property Rate As Integer
    <Required(ErrorMessage:="Value beetween 25 and 10000 is required")>
    <Range(25, 10000, ErrorMessage:="Value beetween 25 and 10000 is required")>
    Public Property Pages As Integer
    <Required>
    <DisplayFormat(DataFormatString:="{0:MM/dd/yyyy}", ApplyFormatInEditMode:=True)>
    Public Property CreatedDate As DateTime
    Public Property Authors As List(Of AuthorVM)

    Public Sub New()
        Authors = New List(Of AuthorVM)()
    End Sub
End Class

