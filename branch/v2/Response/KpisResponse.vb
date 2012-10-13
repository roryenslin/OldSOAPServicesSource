Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
        Public Class KpiReadSingleResponse
        Inherits BaseResponse

        Private _Kpi As KpiInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Kpi() As KpiInfo
            Get
                Return _Kpi
            End Get
            Set(ByVal value As KpiInfo)
                _Kpi = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiReadListResponse
        Inherits BaseResponse

        Private _Kpis As KpiInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Kpis() As KpiInfo()
            Get
                Return _Kpis
            End Get
            Set(ByVal value As KpiInfo())
                _Kpis = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiSync3Response
        Inherits KpiReadListResponse

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

