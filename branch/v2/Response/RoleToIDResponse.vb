Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class RoleToIDReadListResponse
        Inherits BaseResponse

        Private _RoleToID As RoleToIDInfo()

        Public Sub New()

        End Sub

        Public Property RoleToIDs() As RoleToIDInfo()
            Get
                Return _RoleToID
            End Get
            Set(ByVal value As RoleToIDInfo())
                _RoleToID = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class RoleToIDSync3Response
        Inherits RoleToIDReadListResponse

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

