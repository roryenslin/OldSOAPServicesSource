Imports System.Xml

Namespace Entity
    <Serializable()> _
    Public Class RoleToIDInfo
        Private _SupplierID As String
        Private _RoleID As String
        Private _TypeID As TypeIDEnum
        Private _ID As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strRoleId As String, _
                       ByVal iTypeID As TypeIDEnum, ByVal strID As String)
            _SupplierID = strSupplierId
            _RoleID = strRoleId
            _TypeID = iTypeID
            _ID = strID
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        Public Property RoleID() As String
            Get
                Return _RoleID
            End Get
            Set(ByVal value As String)
                _RoleID = value
            End Set
        End Property
        Public Property TypeID() As TypeIDEnum
            Get
                Return _TypeID
            End Get
            Set(ByVal value As TypeIDEnum)
                _TypeID = value
            End Set
        End Property
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
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

    Public Enum TypeIDEnum
        EventType = 0
        KpiGroup = 1
        TargetType = 2
    End Enum
End Namespace

