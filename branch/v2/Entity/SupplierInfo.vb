Imports System.Xml

Namespace Entity
    Public Class SupplierInfo
        Private _SupplierID As String
        Private _Name As String
        Private _DefaultVAT As Decimal
        Private _DefaultPriceList As String
        Private _AddressID As Integer
        Private _CurrencyText As String
        Private _Deleted As Boolean
        Private _DontShowLogo As Boolean
        Private _UseCatalogues As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppId As String, ByVal strName As String, ByVal dVat As Decimal, ByVal strPriceList As String, _
                       ByVal intAddress As Integer, ByVal strCurrencyText As String, Optional ByVal bDontShowLogo As Boolean = False, Optional ByVal bUseCatalogues As Boolean = False)
            _SupplierID = strSuppId
            _Name = strName
            _DefaultVAT = dVat
            _DefaultPriceList = strPriceList
            _AddressID = intAddress
            _CurrencyText = strCurrencyText
            _DontShowLogo = bDontShowLogo
            _UseCatalogues = bUseCatalogues
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
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
        Public Property DefaultVAT() As Decimal
            Get
                Return _DefaultVAT
            End Get
            Set(ByVal value As Decimal)
                _DefaultVAT = value
            End Set
        End Property
        Public Property DefaultPriceList() As String
            Get
                Return _DefaultPriceList
            End Get
            Set(ByVal value As String)
                _DefaultPriceList = value
            End Set
        End Property
        Public Property AddressID() As Integer
            Get
                Return _AddressID
            End Get
            Set(ByVal value As Integer)
                _AddressID = value
            End Set
        End Property
        Public Property DontShowLogo() As Boolean
            Get
                Return _DontShowLogo
            End Get
            Set(ByVal value As Boolean)
                _DontShowLogo = value
            End Set
        End Property
        Public Property UseCatalogues() As Boolean
            Get
                Return _UseCatalogues
            End Get
            Set(ByVal value As Boolean)
                _UseCatalogues = value
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
        Public Property CurrencyText() As String
            Get
                Return _CurrencyText
            End Get
            Set(ByVal value As String)
                _CurrencyText = value
            End Set
        End Property
    End Class
End Namespace
