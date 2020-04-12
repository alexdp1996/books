Namespace Data
    Public Interface ICRUD(Of TEntity, TKey)
        Inherits IDisposable

        Function [Get](id As TKey) As TEntity

        Function Create(entity As TEntity) As TKey

        Sub Update(entity As TEntity)

        Sub Delete(id As TKey)
    End Interface
End Namespace