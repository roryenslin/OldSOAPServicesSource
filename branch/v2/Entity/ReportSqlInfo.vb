Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class ReportSqlInfo
        Private _SupplierID As String
        Private _ReportID As String
        Private _Counter As Integer
        Private _SQLStatement As String
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppId As String, ByVal strRepId As String, ByVal strSQL As String, _
                      ByVal intCount As Integer)
            _SupplierID = strSuppId
            _ReportID = strRepId
            _SQLStatement = strSQL
            _Counter = intCount
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
        Public Property SQLStatement() As String
            Get
                Return _SQLStatement
            End Get
            Set(ByVal value As String)
                _SQLStatement = value
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