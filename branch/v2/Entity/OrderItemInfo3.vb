Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class OrderItemInfo3
        Private _SupplierID As String
        Private _OrderID As String
        Private _ItemID As Integer
        Private _ProductID As String
        Private _Warehouse As String
        Private _Description As String
        Private _Unit As String
        Private _Quantity As Double
        Private _Nett As Double
        Private _Gross As Double
        Private _Discount As Double
        Private _ValueUnit As Double
        Private _Value As Double
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal strSupplierID As String, ByVal strOrderID As String, ByVal intItemId As Integer, ByVal strProductId As String, ByVal strWareHouse As String, ByVal description As String, _
                       ByVal strUnit As String, ByVal dQuantity As Double, ByVal dNett As Double, ByVal dGross As Double, _
                       ByVal dDiscount As Double, ByVal dValueUnit As Double, ByVal dValue As Double)
            _OrderID = strOrderID
            _SupplierID = strSupplierID
            _ItemID = intItemId
            _ProductID = strProductId
            _Warehouse = strWareHouse
            _Unit = strUnit
            _Quantity = dQuantity
            _Nett = dNett
            _Gross = dGross
            _Discount = dDiscount
            _ValueUnit = dValueUnit
            _Value = dValue
            _Description = description
        End Sub
        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        Public Property OrderID() As String
            Get
                Return _OrderID
            End Get
            Set(ByVal value As String)
                _OrderID = value
            End Set
        End Property
        Public Property ItemID() As Integer
            Get
                Return _ItemID
            End Get
            Set(ByVal value As Integer)
                _ItemID = value
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
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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
        Public Property Unit() As String
            Get
                Return _Unit
            End Get
            Set(ByVal value As String)
                _Unit = value
            End Set
        End Property
        Public Property Quantity() As Double
            Get
                Return _Quantity
            End Get
            Set(ByVal value As Double)
                _Quantity = value
            End Set
        End Property
        Public Property Nett() As Double
            Get
                Return _Nett
            End Get
            Set(ByVal value As Double)
                _Nett = value
            End Set
        End Property
        Public Property Gross() As Double
            Get
                Return _Gross
            End Get
            Set(ByVal value As Double)
                _Gross = value
            End Set
        End Property
        Public Property Discount() As Double
            Get
                Return _Discount
            End Get
            Set(ByVal value As Double)
                _Discount = value
            End Set
        End Property
        Public Property ValueUnit() As Double
            Get
                Return _ValueUnit
            End Get
            Set(ByVal value As Double)
                _ValueUnit = value
            End Set
        End Property
        Public Property Value() As Double
            Get
                Return _Value
            End Get
            Set(ByVal value As Double)
                _Value = value
            End Set
        End Property
    End Class
End Namespace