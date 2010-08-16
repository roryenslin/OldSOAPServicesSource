Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class UserReadSingleResponse
        Inherits BaseResponse

        Private _User As UserInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property User() As UserInfo
            Get
                Return _User
            End Get
            Set(ByVal value As UserInfo)
                _User = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class UserReadListResponse
        Inherits BaseResponse

        Private _Users As UserInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Users() As UserInfo()
            Get
                Return _Users
            End Get
            Set(ByVal value As UserInfo())
                _Users = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class UserUsersResponse
        Inherits BaseResponse

        Private _Users As UserInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Users() As UserInfo()
            Get
                Return _Users
            End Get
            Set(ByVal value As UserInfo())
                _Users = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class UserSync3Response
        Inherits UserReadListResponse

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

