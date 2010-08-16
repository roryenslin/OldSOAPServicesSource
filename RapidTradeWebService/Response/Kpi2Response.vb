Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
        Public Class Kpi2ReadSingleResponse
        Inherits BaseResponse

        Private _Kpi2 As Kpi2Info

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Kpi2() As Kpi2Info
            Get
                Return _Kpi2
            End Get
            Set(ByVal value As Kpi2Info)
                _Kpi2 = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class Kpi2ReadListResponse
        Inherits BaseResponse

        Private _Kpi2s As Kpi2Info()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Kpi2s() As Kpi2Info()
            Get
                Return _Kpi2s
            End Get
            Set(ByVal value As Kpi2Info())
                _Kpi2s = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class Kpi2Sync3Response
        Inherits Kpi2ReadListResponse

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

