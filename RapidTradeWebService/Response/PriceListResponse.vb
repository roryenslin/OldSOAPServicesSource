Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class PriceListReadListResponse
        Inherits BaseResponse

        Private _PriceLists As PriceListInfo()

        Public Sub New()

        End Sub

        Public Property PriceLists() As PriceListInfo()
            Get
                Return _PriceLists
            End Get
            Set(ByVal value As PriceListInfo())
                _PriceLists = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class PriceListReadListResponse2
        Inherits BaseResponse

        Private _PriceLists As PriceListInfo2()

        Public Sub New()

        End Sub

        Public Property PriceLists() As PriceListInfo2()
            Get
                Return _PriceLists
            End Get
            Set(ByVal value As PriceListInfo2())
                _PriceLists = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class PriceListSync3Response
        Inherits PriceListReadListResponse

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
