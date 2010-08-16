Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class ParameterInfo
        Private _SupplierID As String
        Private _ReportID As String
        Private _Counter As Integer
        Private _Label As String
        Private _FieldType As Integer
        Private _DefaultData As String
        Private _DefaultSQL As String
        Private _Hidden As Boolean
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppId As String, ByVal strRepId As String, _
                      ByVal intCount As Integer, _
                      ByVal strLabel As String, ByVal intField As Integer, _
                      ByVal strDefault As String, ByVal strSQL As String, ByVal bHidden As Boolean)
            _SupplierID = strSuppId
            _ReportID = strRepId
            _Counter = intCount
            _Label = strLabel
            _FieldType = intField
            _DefaultData = strDefault
            _DefaultSQL = strSQL
            _Hidden = bHidden
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        Public Property ReportID() As String
            Get
                Return _ReportID
            End Get
            Set(ByVal value As String)
                _ReportID = value
            End Set
        End Property
        Public Property Counter() As Integer
            Get
                Return _Counter
            End Get
            Set(ByVal value As Integer)
                _Counter = value
            End Set
        End Property
        Public Property Label() As String
            Get
                Return _Label
            End Get
            Set(ByVal value As String)
                _Label = value
            End Set
        End Property
        Public Property FieldType() As Integer
            Get
                Return _FieldType
            End Get
            Set(ByVal value As Integer)
                _FieldType = value
            End Set
        End Property
        Public Property DefaultData() As String
            Get
                Return _DefaultData
            End Get
            Set(ByVal value As String)
                _DefaultData = value
            End Set
        End Property
        Public Property DefaultSQL() As String
            Get
                Return _DefaultSQL
            End Get
            Set(ByVal value As String)
                _DefaultSQL = value
            End Set
        End Property
        Public Property Hidden() As Boolean
            Get
                Return _Hidden
            End Get
            Set(ByVal value As Boolean)
                _Hidden = value
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
    End Class
End Namespace

