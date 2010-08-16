Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class TableUpdateInfo
        Private _TableName As String = ""
        Private _LastVersion As String = "0"
        Private _MustUpdate As Boolean = False

        Public Sub New()

        End Sub

        Public Sub New(ByVal strTableName As String, ByVal strLastVersion As String, ByVal bMustUpdate As Boolean)
            _TableName = strTableName
            _LastVersion = strLastVersion
            _MustUpdate = bMustUpdate
        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property TableName() As String
            Get
                Return _TableName
            End Get
            Set(ByVal value As String)
                _TableName = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property LastVersion() As String
            Get
                Return _LastVersion
            End Get
            Set(ByVal value As String)
                _LastVersion = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property MustUpdate() As Boolean
            Get
                Return _MustUpdate
            End Get
            Set(ByVal value As Boolean)
                _MustUpdate = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TableSyncInfo
        Inherits TableUpdateInfo
        Private _SupplierId As String = ""
        Private _UserId As String = ""
        Private _MustUpdate As Boolean = False

        Public Sub New()

        End Sub

        Public Sub New(ByVal strTableName As String, ByVal strLastVersion As String, ByVal bMustUpdate As Boolean, _
                       ByVal strSupplierId As String, ByVal strUserId As String)
            MyBase.New(strTableName, strLastVersion, bMustUpdate)
            _SupplierId = strSupplierId
            _UserId = strUserId
        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property SupplierId() As String
            Get
                Return _SupplierId
            End Get
            Set(ByVal value As String)
                _SupplierId = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Overloads Property MustUpdate() As Boolean
            Get
                Return _MustUpdate
            End Get
            Set(ByVal value As Boolean)
                _MustUpdate = value
            End Set
        End Property
    End Class
End Namespace

