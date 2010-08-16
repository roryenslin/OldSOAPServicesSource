Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class OrderReadListResponse
        Inherits BaseResponse

        Private _Orders As OrderInfo()

        Public Sub New()

        End Sub

        Public Property Orders() As OrderInfo()
            Get
                Return _Orders
            End Get
            Set(ByVal value As OrderInfo())
                _Orders = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class OrderSync3Response
        Inherits OrderReadListResponse

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