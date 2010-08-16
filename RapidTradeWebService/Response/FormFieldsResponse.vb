Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class FormFieldReadSingleResponse
        Inherits BaseResponse

        Private _Formfield As FormFieldInfo

        Public Sub New()

        End Sub

        Public Property FormField() As FormFieldInfo
            Get
                Return _Formfield
            End Get
            Set(ByVal value As FormFieldInfo)
                _Formfield = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class FormFieldsReadListResponse
        Inherits BaseResponse

        Private _FormFields As FormFieldInfo()

        Public Sub New()

        End Sub

        Public Property FormFields() As FormFieldInfo()
            Get
                Return _FormFields
            End Get
            Set(ByVal value As FormFieldInfo())
                _FormFields = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class FormFieldsSync3Response
        Inherits FormFieldsReadListResponse

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

