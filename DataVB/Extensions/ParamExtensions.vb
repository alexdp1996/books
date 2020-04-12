Imports System.Runtime.CompilerServices
Imports Dapper.SqlMapper

Namespace Extensions

    Module ParamExtensions

        <Extension()>
        Public Function AsEnumerableParameter(Of T)(elements As IEnumerable(Of T)) As ICustomQueryParameter
            Dim DataTable = New DataTable()

            DataTable.Columns.Add("Element")

            For Each elem In elements
                DataTable.Rows.Add(elem)
            Next

            Return DataTable.AsTableValuedParameter()
        End Function

    End Module

End Namespace