Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class PriceReadSingleResponse
        Inherits BaseResponse

        Private _Price As PriceInfo

        Public Sub New()

        End Sub

        Public Property Price() As PriceInfo
            Get
                Return _Price
            End Get
            Set(ByVal value As PriceInfo)
                _Price = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class PriceReadListResponse
        Inherits BaseResponse

        Private _Prices As PriceInfo()

        Public Sub New()

        End Sub

        Public Property Prices() As PriceInfo()
            Get
                Return _Prices
            End Get
            Set(ByVal value As PriceInfo())
                _Prices = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class PriceSync3Response
        Inherits PriceReadListResponse

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
