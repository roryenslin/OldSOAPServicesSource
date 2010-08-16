Imports RapidTradeWebService.Entity
Namespace Response
    <Serializable()> _
    Public Class KpiVersionReadSingleResponse
        Inherits BaseResponse

        Private _KpiVersion As kpiVersionInfo

        Public Sub New()

        End Sub

        Public Property KpiVersion() As kpiVersionInfo
            Get
                Return _KpiVersion
            End Get
            Set(ByVal value As kpiVersionInfo)
                _KpiVersion = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiVersionReadListResponse
        Inherits BaseResponse

        Private _KpiVersions As kpiVersionInfo()

        Public Sub New()

        End Sub

        Public Property KpiVersions() As kpiVersionInfo()
            Get
                Return _KpiVersions
            End Get
            Set(ByVal value As kpiVersionInfo())
                _KpiVersions = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiVersionSync3Response
        Inherits KpiVersionReadListResponse

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
