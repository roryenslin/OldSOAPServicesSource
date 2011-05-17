Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class OrderReadListResponse3
        Inherits BaseResponse

        Private _Orders As OrderInfo3()

        Public Sub New()

        End Sub

        Public Property Orders() As OrderInfo3()
            Get
                Return _Orders
            End Get
            Set(ByVal value As OrderInfo3())
                _Orders = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class OrderSync3Response3
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