Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class PriceInfo
        Private _SupplierID As String
        Private _ProductID As String
        Private _PriceList As String
        Private _Nett As Double
        Private _Gross As Double
        Private _Discount As Double
        Private _Cost As Decimal

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppID As String, ByVal strProductId As String, ByVal strPriceList As String, ByRef dNett As Decimal, _
            ByVal dGross As Decimal, ByVal dDiscount As Decimal, ByVal dCost As Decimal)
            _SupplierID = strSuppID
            _ProductID = strProductId
            _PriceList = strPriceList
            _Nett = dNett
            _Gross = dGross
            _Discount = dDiscount
            _Cost = dCost
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
        Public Property PriceList() As String
            Get
                Return _PriceList
            End Get
            Set(ByVal value As String)
                _PriceList = value
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
        Public Property Cost() As Decimal
            Get
                Return _Cost
            End Get
            Set(ByVal value As Decimal)
                _Cost = value
            End Set
        End Property

    End Class
End Namespace
