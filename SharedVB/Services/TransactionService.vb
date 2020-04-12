Imports System.Transactions

Namespace Services

    Public Class TransactionService
        Implements IDisposable

        Private _scope As TransactionScope

        Public Sub New()
            Dim options = New TransactionOptions With {
                .IsolationLevel = IsolationLevel.ReadCommitted,
                .Timeout = TransactionManager.DefaultTimeout
            }

            _scope = New TransactionScope(TransactionScopeOption.Required, options)
        End Sub

        Public Sub Complete()
            _scope.Complete()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            _scope.Dispose()
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Namespace

