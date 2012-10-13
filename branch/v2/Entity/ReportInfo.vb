Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class ReportInfo
        Private _SupplierID As String
        Private _ReportID As String
        Private _Description As String
        Private _HideRepeatValues As Boolean
        Private _LongDescription As String
        Private _Deleted As Boolean
        Private _ReportParameters As ParameterInfo()
        Private _ReportSQLs As ReportSqlInfo()

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppId As String, ByVal strRepId As String, ByVal strDesc As String, _
                       ByVal bHide As Boolean, ByVal strLongDesc As String)
            _SupplierID = strSuppId
            _ReportID = strRepId
            _Description = strDesc
            _HideRepeatValues = bHide
            _LongDescription = strLongDesc
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
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property
        Public Property HideRepeatValues() As Boolean
            Get
                Return _HideRepeatValues
            End Get
            Set(ByVal value As Boolean)
                _HideRepeatValues = value
            End Set
        End Property
        Public Property LongDescription() As String
            Get
                Return _LongDescription
            End Get
            Set(ByVal value As String)
                _LongDescription = value
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

        Public Property ReportParameters() As ParameterInfo()
            Get
                Return _ReportParameters
            End Get
            Set(ByVal value As ParameterInfo())
                _ReportParameters = value
            End Set
        End Property
        Public Property ReportSQLs() As ReportSqlInfo()
            Get
                Return _ReportSQLs
            End Get
            Set(ByVal value As ReportSqlInfo())
                _ReportSQLs = value
            End Set
        End Property
    End Class
End Namespace


