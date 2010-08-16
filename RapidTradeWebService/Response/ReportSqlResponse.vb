Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ReportSqlReadSingleResponse
        Inherits BaseResponse

        Private _ReportSql As ReportSqlInfo

        Public Sub New()

        End Sub

        Public Property ReportSql() As ReportSqlInfo
            Get
                Return _ReportSql
            End Get
            Set(ByVal value As ReportSqlInfo)
                _ReportSql = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ReportSqlReadListResponse
        Inherits BaseResponse

        Private _ReportSqls As ReportSqlInfo()

        Public Sub New()

        End Sub

        Public Property ReportSqls() As ReportSqlInfo()
            Get
                Return _ReportSqls
            End Get
            Set(ByVal value As ReportSqlInfo())
                _ReportSqls = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ReportSqlSync3Response
        Inherits ReportSqlReadListResponse

        Private _LastVersion As Integer

        Public Property LastVersion() As Integer
            Get
                Return _LastVersion
            End Get
            Set(ByVal value As Integer)
                _LastVersion = value
            End Set
        End Property

    End Class
End Namespace

