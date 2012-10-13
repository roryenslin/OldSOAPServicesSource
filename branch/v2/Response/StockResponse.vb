Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class StockReadSingleResponse
        Inherits BaseResponse

        Private _Stock As StockInfo

        Public Sub New()

        End Sub

        Public Property Stock() As StockInfo
            Get
                Return _Stock
            End Get
            Set(ByVal value As StockInfo)
                _Stock = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class StockReadListResponse
        Inherits BaseResponse

        Private _Stock As StockInfo()

        Public Sub New()

        End Sub

        Public Property Stock() As StockInfo()
            Get
                Return _Stock
            End Get
            Set(ByVal value As StockInfo())
                _Stock = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class StockSync4Response
        Inherits StockReadListResponse

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
