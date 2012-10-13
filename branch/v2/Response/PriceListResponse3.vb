Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class PriceListReadListResponse3
        Inherits BaseResponse

        Private _PriceLists As PriceListInfo3()

        Public Sub New()

        End Sub

        Public Property PriceLists() As PriceListInfo3()
            Get
                Return _PriceLists
            End Get
            Set(ByVal value As PriceListInfo3())
                _PriceLists = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class PriceListSync3Response3
        Inherits PriceListReadListResponse3

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
