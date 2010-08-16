Namespace Entity
    Public Class AddressInfo
        Private _SupplierID As String
        Private _AccountID As String
        Private _AddressID As String
        Private _unit As String
        Private _Street As String
        Private _PostalCode As String
        Private _City As String
        Private _Region As String
        Private _Country As String
        Private _Telephone As String
        Private _Cell As String
        Private _Fax As String
        Private _WebSite As String
        Private _Email As String
        Private _Deleted As Boolean

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal strSuppId As String, ByVal strAcctId As String, ByVal strAddressID As String, ByVal strunit As String, _
                       ByVal strPos As String, ByVal strPostalCode As String, ByVal strMob As String, ByVal strRegion As String, ByVal strCountry As String, _
                       ByVal strTelephone As String, ByVal strCell As String, ByVal strFax As String, ByVal strWebSite As String, _
                       ByVal strEmail As String, ByVal bDeleted As Boolean)
            _SupplierID = strSuppId
            _AccountID = strAcctId
            _AddressID = strAddressID
            _unit = strunit
            _Street = strPos
            _PostalCode = strPostalCode
            _City = strMob
            _Region = strRegion
            _Country = strCountry
            _Telephone = strTelephone
            _Cell = strCell
            _Fax = strFax
            _WebSite = strWebSite
            _Email = strEmail
            _Deleted = bDeleted
        End Sub
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AccountID() As String
            Get
                Return _AccountID
            End Get
            Set(ByVal value As String)
                _AccountID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AddressID() As String
            Get
                Return _AddressID
            End Get
            Set(ByVal value As String)
                _AddressID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property unit() As String
            Get
                Return _unit
            End Get
            Set(ByVal value As String)
                _unit = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Street() As String
            Get
                Return _Street
            End Get
            Set(ByVal value As String)
                _Street = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property PostalCode() As String
            Get
                Return _PostalCode
            End Get
            Set(ByVal value As String)
                _PostalCode = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property City() As String
            Get
                Return _City
            End Get
            Set(ByVal value As String)
                _City = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Region() As String
            Get
                Return _Region
            End Get
            Set(ByVal value As String)
                _Region = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Country() As String
            Get
                Return _Country
            End Get
            Set(ByVal value As String)
                _Country = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Telephone() As String
            Get
                Return _Telephone
            End Get
            Set(ByVal value As String)
                _Telephone = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Cell() As String
            Get
                Return _Cell
            End Get
            Set(ByVal value As String)
                _Cell = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Fax() As String
            Get
                Return _Fax
            End Get
            Set(ByVal value As String)
                _Fax = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property WebSite() As String
            Get
                Return _WebSite
            End Get
            Set(ByVal value As String)
                _WebSite = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(ByVal value As String)
                _Email = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
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

