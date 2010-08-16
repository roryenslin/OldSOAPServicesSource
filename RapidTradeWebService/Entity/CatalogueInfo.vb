Public Class CatalogueInfo
    Private _CatalogueID As String
    Private _CatalogueName As String
    Private _Deleted As Boolean

    Public Sub New()

    End Sub

    Public Sub New(ByVal strCatalogueId As String, ByVal strCatalogueName As String, ByVal bDeleted As Boolean)
        _CatalogueID = strCatalogueId
        _CatalogueName = strCatalogueName
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
    Public Property CatalogueName() As String
        Get
            Return _CatalogueName
        End Get
        Set(ByVal value As String)
            _CatalogueName = value
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
