Imports EntityModels
Imports Infrastructure.Data
Imports Infrastructure.Logic
Imports [Shared].Interfaces
Imports [Shared].Services
Imports ViewModels

Public Class BookDM
    Inherits BaseDM
    Implements IBookDM

    Public Sub New(factory As IFactory)
        MyBase.New(factory)
    End Sub

    Public Sub Delete(id As Long) Implements IBookDM.Delete
        Using bookRepo = Factory.GetService(Of IBookRepo)()
            bookRepo.Delete(id)
        End Using
    End Sub

    Public Sub Update(model As BookVM) Implements IBookDM.Update
        Dim book = MapperService.Map(Of BookEM)(model)
        Using bookRepo = Factory.GetService(Of IBookRepo)(),
                    scope = New TransactionService()

            Dim id As Long

            id = book.Id.Value
            bookRepo.Update(book)
            Dim authorsIds = model.Authors.Select(Function(a) a.Id.Value)
            bookRepo.UpdateAuthors(id, authorsIds)

            scope.Complete()
        End Using

    End Sub

    Public Sub Create(model As BookVM) Implements IBookDM.Create
        Dim book = MapperService.Map(Of BookEM)(model)
        Using bookRepo = Factory.GetService(Of IBookRepo)(),
                    scope = New TransactionService()

            Dim id As Long

            id = bookRepo.Create(book)
            Dim authorsIds = model.Authors.Select(Function(a) a.Id.Value)
            bookRepo.UpdateAuthors(id, authorsIds)

            scope.Complete()
        End Using

    End Sub

    Public Function [Get](id As Long?) As BookVM Implements IBookDM.Get
        Using bookRepo = Factory.GetService(Of IBookRepo)()

            If (id.HasValue) Then
                Dim BookEM = bookRepo.Get(id.Value)
                Dim BookVM = MapperService.Map(Of BookVM)(BookEM)
                Return BookVM
            End If
            Return Nothing
        End Using

    End Function

    Public Function GetList(model As DataTableRequestVM) As DataTableResponseVM(Of BookVM) Implements IBookDM.GetList
        Dim dataTableEM = MapperService.Map(Of DataTableRequestEM)(model)

        Using bookRepo = Factory.GetService(Of IBookRepo)()
            Dim responseEM = bookRepo.GetList(dataTableEM)
            Dim responseVM = MapperService.Map(Of DataTableResponseVM(Of BookVM))(responseEM)
            responseVM.draw = model.Draw

            Return responseVM
        End Using
    End Function
End Class

