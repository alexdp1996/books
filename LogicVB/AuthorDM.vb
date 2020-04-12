Imports EntityModels
Imports Infrastructure.Data
Imports Infrastructure.Logic
Imports [Shared].Interfaces
Imports [Shared].Services
Imports ViewModels

Public Class AuthorDM
        Inherits BaseDM
        Implements IAuthorDM

        Public Sub New(factory As IFactory)
            MyBase.New(factory)
        End Sub

        Public Sub Delete(id As Long) Implements IAuthorDM.Delete
            Using authorRepo = Factory.GetService(Of IAuthorRepo)()
                authorRepo.Delete(id)
            End Using
        End Sub

        Public Sub Update(model As AuthorVM) Implements IAuthorDM.Update
            Dim author = MapperService.Map(Of AuthorEM)(model)

            Using authorRepo = Factory.GetService(Of IAuthorRepo)()
                authorRepo.Update(author)
            End Using
        End Sub

        Public Sub Create(model As AuthorVM) Implements IAuthorDM.Create
            Dim author = MapperService.Map(Of AuthorEM)(model)

            Using authorRepo = Factory.GetService(Of IAuthorRepo)()
                authorRepo.Create(author)
            End Using

        End Sub

        Public Function [Get](term As String) As IEnumerable(Of AuthorVM) Implements IAuthorDM.Get
            Using authorRepo = Factory.GetService(Of IAuthorRepo)()
                Dim authorsEM = authorRepo.Get(term)
                Dim authorsVM = MapperService.Map(Of IEnumerable(Of AuthorVM))(authorsEM)
                Return authorsVM
            End Using
        End Function

        Public Function [Get](id As Long?) As AuthorVM Implements IAuthorDM.Get
            Using authorRepo = Factory.GetService(Of IAuthorRepo)()
                If (id.HasValue) Then
                    Dim AuthorEM = authorRepo.Get(id.Value)
                    Dim AuthorVM = MapperService.Map(Of AuthorVM)(AuthorEM)
                    Return AuthorVM
                End If
                Return Nothing
            End Using
        End Function

    Public Function GetList(model As DataTableRequestVM) As DataTableResponseVM(Of AuthorVM) Implements IAuthorDM.GetList
        Dim dataTableEM = MapperService.Map(Of DataTableRequestEM)(model)

        Using authorRepo = Factory.GetService(Of IAuthorRepo)()
            Dim responseEM = authorRepo.GetList(dataTableEM)
            Dim responseVM = MapperService.Map(Of DataTableResponseVM(Of AuthorVM))(responseEM)
            responseVM.draw = model.Draw

            Return responseVM
        End Using

    End Function
End Class