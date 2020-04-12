Imports Data.Repositories
Imports Infrastructure.Data
Imports Infrastructure.Logic
Imports Logic
Imports [Shared].Interfaces
Imports Unity
Imports Unity.Injection

Public Class Factory
    Implements IFactory

    Private _unit As IUnityContainer

    Public Sub New()
        _unit = New UnityContainer()
        Register()
    End Sub

    Private Sub Register()
        _unit.RegisterType(Of IBookDM, BookDM)(New InjectionConstructor(Me))
        _unit.RegisterType(Of IAuthorDM, AuthorDM)(New InjectionConstructor(Me))

        _unit.RegisterType(Of IBookRepo, BookRepo)()
        _unit.RegisterType(Of IAuthorRepo, AuthorRepo)()
    End Sub

    Public Function GetService(Of T)() As T Implements IFactory.GetService
        Return _unit.Resolve(GetType(T))
    End Function


End Class