Imports System.ComponentModel.DataAnnotations

Public Class AuthorVM
    Public Property Id As Long?

    <Required>
    <MinLength(4)>
    <RegularExpression("[A-Za-z]*", ErrorMessage:="Only letters are allowed")>
    Public Property Name As String
    <Required>
    <MinLength(4)>
    <RegularExpression("[A-Za-z]*", ErrorMessage:="Only letters are allowed")>
    Public Property Surname As String
    <Display(Name:="Amount of books")>
    Public Property CountOfBooks As Integer?
    Public Property Books As List(Of BookVM)

    Public Sub New()
        Books = New List(Of BookVM)()
    End Sub

End Class
