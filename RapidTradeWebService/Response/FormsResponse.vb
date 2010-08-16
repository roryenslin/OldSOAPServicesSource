Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class FormReadSingleResponse
        Inherits BaseResponse

        Private _Form As FormInfo

        Public Sub New()

        End Sub

        Public Property Form() As FormInfo
            Get
                Return _Form
            End Get
            Set(ByVal value As FormInfo)
                _Form = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class FormsReadListResponse
        Inherits BaseResponse

        Private _FormFields As FormInfo()

        Public Sub New()

        End Sub

        Public Property Forms() As FormInfo()
            Get
                Return _FormFields
            End Get
            Set(ByVal value As FormInfo())
                _FormFields = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class FormsSync3Response
        Inherits FormsReadListResponse

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

