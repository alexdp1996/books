Imports System.Configuration
Imports System.Data.SqlClient
Imports Dapper.FluentMap
Imports Dapper.FluentMap.Dommel
Imports Data.Maps

Namespace Repositories

    Public Class BaseRepo
        Implements IDisposable

        Private _connectionString As String
        Private _Connection As SqlConnection

        Shared Sub New()
            FluentMapper.Initialize(Sub(config)
                                        config.AddMap(New AuthorMap())
                                        config.AddMap(New BookMap())
                                        config.ForDommel()
                                    End Sub
            )
        End Sub

        Public Sub New()
            _connectionString = ConfigurationManager.
                ConnectionStrings("DataContext").ConnectionString
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub

        Protected ReadOnly Property Connection As SqlConnection
            Get
                Return New SqlConnection(_connectionString)
            End Get
        End Property

    End Class

End Namespace
