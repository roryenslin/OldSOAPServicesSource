Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
        Public Class KpiTypeReadSingleResponse
        Inherits BaseResponse

        Private _KpiType As KpiTypeInfo

        Public Sub New()

        End Sub

        Public Property KpiType() As KpiTypeInfo
            Get
                Return _KpiType
            End Get
            Set(ByVal value As KpiTypeInfo)
                _KpiType = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiTypeReadListResponse
        Inherits BaseResponse

        Private _KpiTypes As KpiTypeInfo()

        Public Sub New()

        End Sub

        Public Property KpiTypes() As KpiTypeInfo()
            Get
                Return _KpiTypes
            End Get
            Set(ByVal value As KpiTypeInfo())
                _KpiTypes = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiTypeSync3Response
        Inherits KpiTypeReadListResponse

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

