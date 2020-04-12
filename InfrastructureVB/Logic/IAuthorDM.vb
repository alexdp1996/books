Imports ViewModels

Namespace Logic
    Public Interface IAuthorDM
        Inherits IDisposable

        Sub Update(model As AuthorVM)
        Sub Create(model As AuthorVM)
        Function [Get](model As Long?) As AuthorVM
        Sub Delete(id As Long)

        Function GetList(model As DataTableRequestVM) As DataTableResponseVM(Of AuthorVM)
        Function [Get](term As String) As IEnumerable(Of AuthorVM)

    End Interface
End Namespace
