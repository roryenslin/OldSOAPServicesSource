Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ActivityReadSingleResponse
        Inherits BaseResponse

        Private _Activity As ActivityInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ActivityRecord() As ActivityInfo
            Get
                Return _Activity
            End Get
            Set(ByVal value As ActivityInfo)
                _Activity = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivityReadListResponse
        Inherits BaseResponse

        Private _Activitys As ActivityInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Activitys() As ActivityInfo()
            Get
                Return _Activitys
            End Get
            Set(ByVal value As ActivityInfo())
                _Activitys = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivityDatesResponse
        Inherits BaseResponse

        Private _ActivityDates As List(Of Integer)

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ActivityDates() As List(Of Integer)
            Get
                Return _ActivityDates
            End Get
            Set(ByVal value As List(Of Integer))
                _ActivityDates = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivitySync3Response
        Inherits ActivityReadListResponse

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

