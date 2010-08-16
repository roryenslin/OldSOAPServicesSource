Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class PriceListInfo
        Private _ProductID As String
        Private _Description As String
        Private _VAT As Decimal
        Private _Nett As Double
        Private _Gross As Double
        Private _Discount As Double
        Private _Unit As String
        Private _CategoryName As String
        Private _UserField01 As String
        Private _UserField02 As String
        Private _UserField03 As String
        Private _UserField04 As String
        Private _UserField05 As String
        Private _UserField06 As String
        Private _UserField07 As String
        Private _UserField08 As String
        Private _UserField09 As String
        Private _UserField10 As String
        Private _PriceList As String
        Private _Cost As Decimal
        Private _Barcode As String
        Private _OptionalProducts As String
        Private _Components As String
        Private _SimilarProducts As String
        Private _DeliveryConstraint1 As String
        Private _DeliveryConstraint2 As String
        Private _DeliveryConstraint3 As String
        Private _DeliveryConstraint4 As String
        Private _DisplayFields As String
        Private _WebSite As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal strProductID As String, ByVal strDescription As String, ByVal dVAT As Decimal, _
                       ByVal strBarcode As String, ByVal strUnit As String, ByVal strCategoryLabels As String, _
                       ByVal strOptionalProducts As String, ByVal strComponents As String, _
                       ByVal strSimilarProducts As String, ByVal strWebSite As String, _
                       ByVal strDeliveryConstraint1 As String, ByVal strDeliveryConstraint2 As String, _
                       ByVal strDeliveryConstraint3 As String, ByVal strDeliveryConstraint4 As String, _
                       ByVal strDisplayFields As String, ByVal strUserField01 As String, _
                       ByVal strUserField02 As String, ByVal strUserField03 As String, ByVal strUserField04 As String, _
                       ByVal strUserField05 As String, ByVal strUserField06 As String, ByVal strUserField07 As String, _
                       ByVal strUserField08 As String, ByVal strUserField09 As String, ByVal strUserField10 As String, _
                       ByVal strPriceList As String, ByRef dNett As Decimal, _
                       ByVal dGross As Decimal, ByVal dDiscount As Decimal, ByVal dCost As Decimal)
            _ProductID = strProductID
            _Description = strDescription
            _VAT = dVAT
            _Barcode = strBarcode
            _Unit = strUnit
            _CategoryName = strCategoryLabels
            _OptionalProducts = strOptionalProducts
            _Components = strComponents
            _SimilarProducts = strSimilarProducts
            _WebSite = strWebSite
            _DeliveryConstraint1 = strDeliveryConstraint1
            _DeliveryConstraint2 = strDeliveryConstraint2
            _DeliveryConstraint3 = strDeliveryConstraint3
            _DeliveryConstraint4 = strDeliveryConstraint4
            _DisplayFields = strDisplayFields
            _UserField01 = strUserField01
            _UserField02 = strUserField02
            _UserField03 = strUserField03
            _UserField04 = strUserField04
            _UserField05 = strUserField05
            _UserField06 = strUserField06
            _UserField07 = strUserField07
            _UserField08 = strUserField08
            _UserField09 = strUserField09
            _UserField10 = strUserField10
            _PriceList = strPriceList
            _Nett = dNett
            _Gross = dGross
            _Discount = dDiscount
            _Cost = dCost
        End Sub

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
        Public Property UserField01() As String
            Get
                Return _UserField01
            End Get
            Set(ByVal value As String)
                _UserField01 = value
            End Set
        End Property
        Public Property UserField02() As String
            Get
                Return _UserField02
            End Get
            Set(ByVal value As String)
                _UserField02 = value
            End Set
        End Property
        Public Property UserField03() As String
            Get
                Return _UserField03
            End Get
            Set(ByVal value As String)
                _UserField03 = value
            End Set
        End Property
        Public Property UserField04() As String
            Get
                Return _UserField04
            End Get
            Set(ByVal value As String)
                _UserField04 = value
            End Set
        End Property
        Public Property UserField05() As String
            Get
                Return _UserField05
            End Get
            Set(ByVal value As String)
                _UserField05 = value
            End Set
        End Property
        Public Property UserField06() As String
            Get
                Return _UserField06
            End Get
            Set(ByVal value As String)
                _UserField06 = value
            End Set
        End Property
        Public Property UserField07() As String
            Get
                Return _UserField07
            End Get
            Set(ByVal value As String)
                _UserField07 = value
            End Set
        End Property
        Public Property UserField08() As String
            Get
                Return _UserField08
            End Get
            Set(ByVal value As String)
                _UserField08 = value
            End Set
        End Property
        Public Property UserField09() As String
            Get
                Return _UserField09
            End Get
            Set(ByVal value As String)
                _UserField09 = value
            End Set
        End Property
        Public Property UserField10() As String
            Get
                Return _UserField10
            End Get
            Set(ByVal value As String)
                _UserField10 = value
            End Set
        End Property



        Public Property VAT() As Decimal
            Get
                Return _VAT
            End Get
            Set(ByVal value As Decimal)
                _VAT = value
            End Set
        End Property
        Public Property Barcode() As String
            Get
                Return _Barcode
            End Get
            Set(ByVal value As String)
                _Barcode = value
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
        Public Property CategoryName() As String
            Get
                Return _CategoryName
            End Get
            Set(ByVal value As String)
                _CategoryName = value
            End Set
        End Property
        Public Property OptionalProducts() As String
            Get
                Return _OptionalProducts
            End Get
            Set(ByVal value As String)
                _OptionalProducts = value
            End Set
        End Property
        Public Property Components() As String
            Get
                Return _Components
            End Get
            Set(ByVal value As String)
                _Components = value
            End Set
        End Property
        Public Property SimilarProducts() As String
            Get
                Return _SimilarProducts
            End Get
            Set(ByVal value As String)
                _SimilarProducts = value
            End Set
        End Property
        Public Property WebSite() As String
            Get
                Return _WebSite
            End Get
            Set(ByVal value As String)
                _WebSite = value
            End Set
        End Property
        Public Property DeliveryConstraint1() As String
            Get
                Return _DeliveryConstraint1
            End Get
            Set(ByVal value As String)
                _DeliveryConstraint1 = value
            End Set
        End Property
        Public Property DeliveryConstraint2() As String
            Get
                Return _DeliveryConstraint2
            End Get
            Set(ByVal value As String)
                _DeliveryConstraint2 = value
            End Set
        End Property
        Public Property DeliveryConstraint3() As String
            Get
                Return _DeliveryConstraint3
            End Get
            Set(ByVal value As String)
                _DeliveryConstraint3 = value
            End Set
        End Property
        Public Property DeliveryConstraint4() As String
            Get
                Return _DeliveryConstraint4
            End Get
            Set(ByVal value As String)
                _DeliveryConstraint4 = value
            End Set
        End Property
        Public Property DisplayFields() As String
            Get
                Return _DisplayFields
            End Get
            Set(ByVal value As String)
                _DisplayFields = value
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

