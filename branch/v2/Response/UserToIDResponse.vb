Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class UserToIDReadListResponse
        Inherits BaseResponse

        Private _UserToID As UserToIDInfo()

        Public Sub New()

        End Sub

        Public Property UserToIDs() As UserToIDInfo()
            Get
                Return _UserToID
            End Get
            Set(ByVal value As UserToIDInfo())
                _UserToID = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class UserToIDReadListResponse2
        Inherits BaseResponse

        Private _UserToID As UserToIDInfo2()

        Public Sub New()

        End Sub

        Public Property UserToIDs() As UserToIDInfo2()
            Get
                Return _UserToID
            End Get
            Set(ByVal value As UserToIDInfo2())
                _UserToID = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class UserToIDSync3Response
        Inherits UserToIDReadListResponse

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
