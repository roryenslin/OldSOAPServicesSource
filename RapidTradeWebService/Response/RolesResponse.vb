Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class RoleReadSingleResponse
        Inherits BaseResponse

        Private _Role As RoleInfo

        Public Sub New()

        End Sub

        Public Property Role() As RoleInfo
            Get
                Return _Role
            End Get
            Set(ByVal value As RoleInfo)
                _Role = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class RoleReadListResponse
        Inherits BaseResponse

        Private _Roles As RoleInfo()

        Public Sub New()

        End Sub

        Public Property Roles() As RoleInfo()
            Get
                Return _Roles
            End Get
            Set(ByVal value As RoleInfo())
                _Roles = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class RoleSync3Response
        Inherits RoleReadListResponse

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

