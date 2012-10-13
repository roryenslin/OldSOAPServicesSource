Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ActivityReadSingleResponse2
        Inherits BaseResponse

        Private _Activity As ActivityInfo2

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ActivityRecord() As ActivityInfo2
            Get
                Return _Activity
            End Get
            Set(ByVal value As ActivityInfo2)
                _Activity = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivityReadListResponse2
        Inherits BaseResponse

        Private _Activitys As ActivityInfo2()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Activitys() As ActivityInfo2()
            Get
                Return _Activitys
            End Get
            Set(ByVal value As ActivityInfo2())
                _Activitys = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivityDatesResponse2
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
    Public Class ActivitySync3Response2
        Inherits ActivityReadListResponse2

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

