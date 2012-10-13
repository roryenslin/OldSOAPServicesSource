Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class KpiType2ReadSingleResponse
        Inherits BaseResponse

        Private _KpiType2 As KpiType2Info

        Public Sub New()

        End Sub

        Public Property KpiType2() As KpiType2Info
            Get
                Return _KpiType2
            End Get
            Set(ByVal value As KpiType2Info)
                _KpiType2 = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiType2ReadListResponse
        Inherits BaseResponse

        Private _KpiTypes2 As KpiType2Info()

        Public Sub New()

        End Sub

        Public Property KpiTypes2() As KpiType2Info()
            Get
                Return _KpiTypes2
            End Get
            Set(ByVal value As KpiType2Info())
                _KpiTypes2 = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class KpiType2Sync3Response
        Inherits KpiType2ReadListResponse

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

