Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
        Public Class ProductCategoryReadSingleResponse
        Inherits BaseResponse

        Private _ProductCategory As ProductCategoryInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ProductCategory() As ProductCategoryInfo
            Get
                Return _ProductCategory
            End Get
            Set(ByVal value As ProductCategoryInfo)
                _ProductCategory = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductCategoryReadListResponse
        Inherits BaseResponse

        Private _ProductCategories As ProductCategoryInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ProductCategories() As ProductCategoryInfo()
            Get
                Return _ProductCategories
            End Get
            Set(ByVal value As ProductCategoryInfo())
                _ProductCategories = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProductCategorySync3Response
        Inherits ProductCategoryReadListResponse

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

