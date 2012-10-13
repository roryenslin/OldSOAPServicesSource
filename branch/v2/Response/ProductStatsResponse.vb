Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ProductStatReadSingleResponse
        Inherits BaseResponse

        Private _ProductStat As ProductStatInfo

        Public Sub New()

        End Sub

        Public Property ProductStat() As ProductStatInfo
            Get
                Return _ProductStat
            End Get
            Set(ByVal value As ProductStatInfo)
                _ProductStat = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductStatReadListResponse
        Inherits BaseResponse

        Private _ProductStats As ProductStatInfo()

        Public Sub New()

        End Sub

        Public Property ProductStats() As ProductStatInfo()
            Get
                Return _ProductStats
            End Get
            Set(ByVal value As ProductStatInfo())
                _ProductStats = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductStatSync3Response
        Inherits ProductStatReadListResponse

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
