Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class UserInfo
        Private _UserID As String
        Private _Name As String
        Private _Email As String
        Private _Country As String
        Private _SupplierID As String
        Private _RepID As String
        Private _Manager As String
        Private _Password As String
        Private _AddressId As Integer
        Private _Accounts As AccountInfo()
        'Private _Staff As UserInfo()
        Private _Deleted As Boolean
        Private _IsAdmin As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strUserId As String, ByVal strName As String, ByVal strEmail As String, _
                       ByVal strCountry As String, ByVal strManager As String, _
                       ByVal strSupplierId As String, ByVal strPassword As String, ByVal intAddressId As Integer, _
                       ByVal strRepID As String, ByVal arrAccounts As AccountInfo(), Optional ByVal bIsAdmin As Boolean = False)
            _UserID = strUserId
            _Name = strName
            _Email = strEmail
            _Country = strCountry
            _SupplierID = strSupplierId
            _RepID = strRepID
            _Manager = strManager
            _Password = strPassword
            _AddressId = intAddressId
            _Accounts = arrAccounts
            _IsAdmin = bIsAdmin
        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Accounts() As AccountInfo()
            Get
                Return _Accounts
            End Get
            Set(ByVal value As AccountInfo())
                _Accounts = value
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
        Public Property Country() As String
            Get
                Return _Country
            End Get
            Set(ByVal value As String)
                _Country = value
            End Set
        End Property
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
        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal value As String)
                _Password = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AddressID() As Integer
            Get
                Return _AddressId
            End Get
            Set(ByVal value As Integer)
                _AddressId = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property RepID() As String
            Get
                Return _RepID
            End Get
            Set(ByVal value As String)
                _RepID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Manager() As String
            Get
                Return _Manager
            End Get
            Set(ByVal value As String)
                _Manager = value
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

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
       Public Property IsAdmin() As Boolean
            Get
                Return _IsAdmin
            End Get
            Set(ByVal value As Boolean)
                _IsAdmin = value
            End Set
        End Property
        'Public Property Staff() As UserInfo()
        '    Get
        '        Return _Staff
        '    End Get
        '    Set(ByVal value As UserInfo())
        '        _Staff = value
        '    End Set
        'End Property
    End Class
End Namespace