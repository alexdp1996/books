Imports EntityModels

Namespace Data
    Public Interface IAuthorRepo
        Inherits ICRUD(Of AuthorEM, Long)

        Overloads Function [Get](term As String) As IEnumerable(Of AuthorEM)
        Function GetList(model As DataTableRequestEM) As DataTableResponseEM(Of AuthorEM)
    End Interface
End Namespace