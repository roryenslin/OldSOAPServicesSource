Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class PriceListInfo2
        Inherits PriceListInfo

        Private _SupplierID As String
        Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property

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
                       ByVal dGross As Decimal, ByVal dDiscount As Decimal, ByVal dCost As Decimal, _
                       ByVal supplierID As String)

            Me.SupplierID = supplierID
            ProductID = strProductID
            Description = strDescription
            VAT = dVAT
            Barcode = strBarcode
            Unit = strUnit
            CategoryName = strCategoryLabels
            OptionalProducts = strOptionalProducts
            Components = strComponents
            SimilarProducts = strSimilarProducts
            WebSite = strWebSite
            DeliveryConstraint1 = strDeliveryConstraint1
            DeliveryConstraint2 = strDeliveryConstraint2
            DeliveryConstraint3 = strDeliveryConstraint3
            DeliveryConstraint4 = strDeliveryConstraint4
            DisplayFields = strDisplayFields
            UserField01 = strUserField01
            UserField02 = strUserField02
            UserField03 = strUserField03
            UserField04 = strUserField04
            UserField05 = strUserField05
            UserField06 = strUserField06
            UserField07 = strUserField07
            UserField08 = strUserField08
            UserField09 = strUserField09
            UserField10 = strUserField10
            PriceList = strPriceList
            Nett = dNett
            Gross = dGross
            Discount = dDiscount
            Cost = dCost
        End Sub

    End Class

End Namespace

