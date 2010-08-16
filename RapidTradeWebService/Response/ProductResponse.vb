Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ProductReadSingleResponse
        Inherits BaseResponse

        Private _Product As ProductInfo

        Public Sub New()

        End Sub

        Public Property Product() As ProductInfo
            Get
                Return _Product
            End Get
            Set(ByVal value As ProductInfo)
                _Product = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductReadListResponse
        Inherits BaseResponse

        Private _Products As ProductInfo()

        Public Sub New()

        End Sub

        Public Property Products() As ProductInfo()
            Get
                Return _Products
            End Get
            Set(ByVal value As ProductInfo())
                _Products = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductSync3Response
        Inherits ProductReadListResponse

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
