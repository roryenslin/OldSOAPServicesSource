Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class KpiGroupReadSingleResponse
        Inherits BaseResponse

        Private _KpiGroup As KpiGroupInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property KpiGroup() As KpiGroupInfo
            Get
                Return _KpiGroup
            End Get
            Set(ByVal value As KpiGroupInfo)
                _KpiGroup = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiGroupReadListResponse
        Inherits BaseResponse

        Private _KpiGroups As KpiGroupInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property KpiGroups() As KpiGroupInfo()
            Get
                Return _KpiGroups
            End Get
            Set(ByVal value As KpiGroupInfo())
                _KpiGroups = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiGroupSync3Response
        Inherits KpiGroupReadListResponse

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

