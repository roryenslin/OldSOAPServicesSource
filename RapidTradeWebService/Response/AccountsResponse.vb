Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class AccountReadSingleResponse
        Inherits BaseResponse

        Private _Account As AccountInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=True)> _
        Public Property Account() As AccountInfo
            Get
                Return _Account
            End Get
            Set(ByVal value As AccountInfo)
                _Account = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class AccountReadListResponse
        Inherits BaseResponse

        Private _Accounts As AccountInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlArrayItem(GetType(AccountInfo))> _
        Public Property Accounts() As AccountInfo()
            Get
                Return _Accounts
            End Get
            Set(ByVal value As AccountInfo())
                _Accounts = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class AccountSync3Response
        Inherits AccountReadListResponse

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

