Namespace Entity
    Public Class ContactInfo
        Private _SupplierID As String
        Private _AccountID As String
        Private _Counter As Integer
        Private _Name As String
        Private _Position As String
        Private _Tel As String
        Private _Mobile As String
        Private _Email As String
        Private _UserField1 As String
        Private _UserField2 As String
        Private _UserField3 As String
        Private _UserField4 As String
        Private _UserField5 As String
        Private _Deleted As Boolean
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal strSuppId As String, ByVal strAcctId As String, ByVal iCounter As Integer, ByVal strName As String, _
                       ByVal strPos As String, ByVal strTel As String, ByVal strMob As String, ByVal strEmail As String, ByVal strUF1 As String, _
                       ByVal strUF2 As String, ByVal strUF3 As String, ByVal strUF4 As String, ByVal strUF5 As String, _
                       ByVal bDeleted As Boolean)
            _SupplierID = strSuppId
            _AccountID = strAcctId
            _Counter = iCounter
            _Name = strName
            _Position = strPos
            _Tel = strTel
            _Mobile = strMob
            _Email = strEmail
            _UserField1 = strUF1
            _UserField2 = strUF2
            _UserField3 = strUF3
            _UserField4 = strUF4
            _UserField5 = strUF5
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
        Public Property Counter() As Integer
            Get
                Return _Counter
            End Get
            Set(ByVal value As Integer)
                _Counter = value
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
        Public Property Position() As String
            Get
                Return _Position
            End Get
            Set(ByVal value As String)
                _Position = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Tel() As String
            Get
                Return _Tel
            End Get
            Set(ByVal value As String)
                _Tel = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Mobile() As String
            Get
                Return _Mobile
            End Get
            Set(ByVal value As String)
                _Mobile = value
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
        Public Property UserField1() As String
            Get
                Return _UserField1
            End Get
            Set(ByVal value As String)
                _UserField1 = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserField2() As String
            Get
                Return _UserField2
            End Get
            Set(ByVal value As String)
                _UserField2 = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserField3() As String
            Get
                Return _UserField3
            End Get
            Set(ByVal value As String)
                _UserField3 = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserField4() As String
            Get
                Return _UserField4
            End Get
            Set(ByVal value As String)
                _UserField4 = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserField5() As String
            Get
                Return _UserField5
            End Get
            Set(ByVal value As String)
                _UserField5 = value
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

