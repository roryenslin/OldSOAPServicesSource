Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class CataloguesReadListResponse
        Inherits BaseResponse

        Private _Catalogues As CatalogueInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlArrayItem(GetType(CatalogueInfo))> _
        Public Property Catalogues() As CatalogueInfo()
            Get
                Return _Catalogues
            End Get
            Set(ByVal value As CatalogueInfo())
                _Catalogues = value
            End Set
        End Property
    End Class
End Namespace

