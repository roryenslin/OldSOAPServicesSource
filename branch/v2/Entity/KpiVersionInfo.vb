Namespace Entity
    Public Class KpiVersionInfo
        Private _SupplierID As String
        Private _VersionID As String
        Private _Description As String
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSupplierID As String, ByVal strVersionId As String, _
                       ByVal strDescription As String)
            _SupplierID = strSupplierID
            _VersionID = strVersionId
            _Description = strDescription
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property

        Public Property VersionID() As String
            Get
                Return _VersionID
            End Get
            Set(ByVal value As String)
                _VersionID = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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
End Namespace
