Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class RoleInfo
        Private _SupplierID As String
        Private _RoleId As String
        Private _Name As String
        Private _Description As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strRoleId As String, _
                       ByVal strName As String, ByVal strDescription As String)
            _SupplierID = strSupplierId
            _RoleId = strRoleId
            _Name = strName
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
        Public Property RoleId() As String
            Get
                Return _RoleId
            End Get
            Set(ByVal value As String)
                _RoleId = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
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