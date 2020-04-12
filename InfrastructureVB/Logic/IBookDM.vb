Imports ViewModels

Namespace Logic
    Public Interface IBookDM
        Inherits IDisposable

        Sub Update(model As BookVM)
        Sub Create(model As BookVM)
        Function [Get](model As Long?) As BookVM
        Sub Delete(id As Long)

        Function GetList(model As DataTableRequestVM) As DataTableResponseVM(Of BookVM)

    End Interface
End Namespace