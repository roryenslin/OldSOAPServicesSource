Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ProductLongTextReadSingleResponse
        Inherits BaseResponse

        Private _ProductLongText As ProductLongTextInfo

        Public Sub New()

        End Sub

        Public Property ProductLongText() As ProductLongTextInfo
            Get
                Return _ProductLongText
            End Get
            Set(ByVal value As ProductLongTextInfo)
                _ProductLongText = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductLongTextReadListResponse
        Inherits BaseResponse

        Private _ProductLongTexts As ProductLongTextInfo()

        Public Sub New()

        End Sub

        Public Property ProductLongTexts() As ProductLongTextInfo()
            Get
                Return _ProductLongTexts
            End Get
            Set(ByVal value As ProductLongTextInfo())
                _ProductLongTexts = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductLongTextSync3Response
        Inherits ProductLongTextReadListResponse

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
