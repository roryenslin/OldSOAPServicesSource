Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class UserCompanySync4Response
        Inherits BaseResponse

        Private _UsersCompanies As UserCompanyInfo()
        Private _LastVersion As Integer

        Public Property LastVersion() As Integer
            Get
                Return _LastVersion
            End Get
            Set(ByVal value As Integer)
                _LastVersion = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserCompanies() As UserCompanyInfo()
            Get
                Return _UsersCompanies
            End Get
            Set(ByVal value As UserCompanyInfo())
                _UsersCompanies = value
            End Set
        End Property

    End Class
End Namespace


