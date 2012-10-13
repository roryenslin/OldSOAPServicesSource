Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class OrderReadListResponse2
        Inherits BaseResponse

        Private _Orders As OrderInfo2()

        Public Sub New()

        End Sub

        Public Property Orders() As OrderInfo2()
            Get
                Return _Orders
            End Get
            Set(ByVal value As OrderInfo2())
                _Orders = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class OrderSync3Response2
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