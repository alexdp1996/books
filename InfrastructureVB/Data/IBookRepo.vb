Imports EntityModels

Namespace Data
    Public Interface IBookRepo
        Inherits ICRUD(Of BookEM, Long)

        Sub UpdateAuthors(bookId As Long, authorIds As IEnumerable(Of Long))
        Function GetList(model As DataTableRequestEM) As DataTableResponseEM(Of BookEM)

    End Interface
End Namespace