Imports RapidTradeWebService.Entity

Namespace Response

    <Serializable()> _
    Public Class GetParameterResponse
        Inherits BaseResponse

        Private _Results As String()

        Public Sub New()

        End Sub

        Public Property Results() As String()
            Get
                Return _Results
            End Get
            Set(ByVal value As String())
                _Results = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ParameterReadSingleResponse
        Inherits BaseResponse

        Private _Parameter As ParameterInfo

        Public Sub New()

        End Sub

        Public Property Parameter() As ParameterInfo
            Get
                Return _Parameter
            End Get
            Set(ByVal value As ParameterInfo)
                _Parameter = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ParameterReadListResponse
        Inherits BaseResponse

        Private _Parameters As ParameterInfo()

        Public Sub New()

        End Sub

        Public Property Parameters() As ParameterInfo()
            Get
                Return _Parameters
            End Get
            Set(ByVal value As ParameterInfo())
                _Parameters = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ParameterSync3Response
        Inherits ParameterReadListResponse

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