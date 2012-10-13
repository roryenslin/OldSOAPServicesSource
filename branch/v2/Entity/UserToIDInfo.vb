Imports System.Xml

Namespace Entity
    <Serializable()> _
Public Class UserToIDInfo
        Private _SupplierID As String
        Private _UserID As String
        Private _TypeID As Integer
        Private _ID As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strUserId As String, _
                       ByVal iTypeID As Integer, ByVal strID As String)
            _SupplierID = strSupplierId
            _UserID = strUserId
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
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property
        Public Property TypeID() As Integer
            Get
                Return _TypeID
            End Get
            Set(ByVal value As Integer)
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
End Namespace