Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class TargetTypeResponse

    End Class

    <Serializable()> _
    Public Class TargetTypeReadListResponse
        Inherits BaseResponse

        Private _TargetTypes As TargetTypeInfo()

        Public Sub New()

        End Sub

        Public Property TargetTypes() As TargetTypeInfo()
            Get
                Return _TargetTypes
            End Get
            Set(ByVal value As TargetTypeInfo())
                _TargetTypes = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TargetTypeSync3Response
        Inherits TargetTypeReadListResponse

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

