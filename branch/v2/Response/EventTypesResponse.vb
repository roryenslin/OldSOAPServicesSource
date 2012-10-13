Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class EventTypeReadSingleResponse
        Inherits BaseResponse

        Private _EventType As EventTypeInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property EventType() As EventTypeInfo
            Get
                Return _EventType
            End Get
            Set(ByVal value As EventTypeInfo)
                _EventType = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class EventTypeReadListResponse
        Inherits BaseResponse

        Private _EventTypes As EventTypeInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property EventTypes() As EventTypeInfo()
            Get
                Return _EventTypes
            End Get
            Set(ByVal value As EventTypeInfo())
                _EventTypes = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class EventTypeSync3Response
        Inherits EventTypeReadListResponse

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

