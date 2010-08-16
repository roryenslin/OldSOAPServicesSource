Public Class SupplierCatalogueInfo
    Private _CatalogueID As String
    Private _SupplierID As String
    Private _Deleted As Boolean

    Public Sub New()

    End Sub

    Public Sub New(ByVal strCatalogueId As String, ByVal strSupplierId As String, ByVal bDeleted As Boolean)
        _CatalogueID = strCatalogueId
        _SupplierID = strSupplierId
        _Deleted = bDeleted
    End Sub

    Public Property CatalogueID() As String
        Get
            Return _CatalogueID

        End Get
        Set(ByVal value As String)
            _CatalogueID = value
        End Set
    End Property
    Public Property SupplierID() As String
        Get
            Return _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
        End Set
    End Property
    Public Property Deleted() As Boolean
        Get
            Return _Deleted
        End Get
        Set(ByVal value As Boolean)
            _Deleted = value
        End Set
    End Property
End Class
