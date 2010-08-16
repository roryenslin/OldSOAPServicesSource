Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class FormTypeReadSingleResponse
        Inherits BaseResponse

        Private _FormType As FormTypeInfo

        Public Sub New()

        End Sub

        Public Property FormType() As FormTypeInfo
            Get
                Return _FormType
            End Get
            Set(ByVal value As FormTypeInfo)
                _FormType = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class FormTypeReadListResponse
        Inherits BaseResponse

        Private _FormTypes As FormTypeInfo()

        Public Sub New()

        End Sub

        Public Property FormTypes() As FormTypeInfo()
            Get
                Return _FormTypes
            End Get
            Set(ByVal value As FormTypeInfo())
                _FormTypes = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class FormTypeSync3Response
        Inherits FormTypeReadListResponse

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

