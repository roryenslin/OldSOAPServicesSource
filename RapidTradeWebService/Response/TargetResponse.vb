Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class TargetReadListResponse
        Inherits BaseResponse

        Private _TargetInfos As TargetInfo()

        Public Sub New()

        End Sub

        Public Property Targets() As TargetInfo()
            Get
                Return _TargetInfos
            End Get
            Set(ByVal value As TargetInfo())
                _TargetInfos = value
            End Set
        End Property
    End Class


    <Serializable()> _
    Public Class TargetSync3Response
        Inherits TargetReadListResponse

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


