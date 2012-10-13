Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class EventReadSingleResponse
        Inherits BaseResponse

        Private _Event As EventInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property EventRecord() As EventInfo
            Get
                Return _Event
            End Get
            Set(ByVal value As EventInfo)
                _Event = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class EventReadListResponse
        Inherits BaseResponse

        Private _Events As EventInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Events() As EventInfo()
            Get
                Return _Events
            End Get
            Set(ByVal value As EventInfo())
                _Events = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class EventDatesResponse
        Inherits BaseResponse

        Private _EventDates As List(Of Integer)

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property EventDates() As List(Of Integer)
            Get
                Return _EventDates
            End Get
            Set(ByVal value As List(Of Integer))
                _EventDates = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class EventSync3Response
        Inherits EventReadListResponse

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

