Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class CompanyReadSingleResponse
        Inherits BaseResponse

        Private _Company As CompanyInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=True)> _
        Public Property Company() As CompanyInfo
            Get
                Return _Company
            End Get
            Set(ByVal value As CompanyInfo)
                _Company = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class CompanyReadListResponse
        Inherits BaseResponse

        Private _Companies As CompanyInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlArrayItem(GetType(CompanyInfo))> _
        Public Property Companies() As CompanyInfo()
            Get
                Return _Companies
            End Get
            Set(ByVal value As CompanyInfo())
                _Companies = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class Companiesync3Response
        Inherits CompanyReadListResponse

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

