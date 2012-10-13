Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ProductImagesReadListResponse
        Inherits BaseResponse

        Private _ProductImagess As ProductImageInfo()

        Public Sub New()

        End Sub

        Public Property ProductImages() As ProductImageInfo()
            Get
                Return _ProductImagess
            End Get
            Set(ByVal value As ProductImageInfo())
                _ProductImagess = value
            End Set
        End Property
    End Class
End Namespace
