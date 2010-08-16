Namespace Entity

    Public Class OptionInfo
        Private _SupplierID As String
        Private _Name As String
        Private _Group As String
        Private _Type As String
        Private _Value As String
        Private _Platform As Short
        Private _Deleted As Boolean

        Public Sub New()

        End Sub
        Public Sub New(ByVal strSuppId As String, ByVal strName As String, ByVal strGrp As String, _
                       ByVal strType As String, ByVal strValue As String, ByVal shPlatform As Short)
            _SupplierID = strSuppId
            _Name = strName
            _Group = strGrp
            _Type = strType
            _Value = strValue
            _Platform = shPlatform
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
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Group() As String
            Get
                Return _Group
            End Get
            Set(ByVal value As String)
                _Group = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(ByVal value As String)
                _Value = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Platform() As Short
            Get
                Return _Platform
            End Get
            Set(ByVal value As Short)
                _Platform = value
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