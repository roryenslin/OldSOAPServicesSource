Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class StockInfo
        Private _SupplierID As String
        Private _ProductID As String
        Private _Warehouse As String
        Private _Stock As Integer
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierID As String, ByVal strProductID As String, ByVal strWarehouse As String, ByVal stock As Integer, ByVal deleted As Boolean)
            _SupplierID = strSupplierID
            _ProductID = strProductID
            _Warehouse = strWarehouse
            _Stock = stock
            _Deleted = deleted
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        Public Property ProductID() As String
            Get
                Return _ProductID
            End Get
            Set(ByVal value As String)
                _ProductID = value
            End Set
        End Property
        Public Property Warehouse() As String
            Get
                Return _Warehouse
            End Get
            Set(ByVal value As String)
                _Warehouse = value
            End Set
        End Property
        Public Property Stock() As Integer
            Get
                Return _Stock
            End Get
            Set(ByVal value As Integer)
                _Stock = value
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
