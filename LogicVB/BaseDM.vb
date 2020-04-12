Imports [Shared].Interfaces

Public Class BaseDM
    Implements IDisposable

    Protected Property Factory As IFactory

    Public Sub New(factory As IFactory)
        Me.Factory = factory
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub
End Class

