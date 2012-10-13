Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ActivityTypeReadSingleResponse
        Inherits BaseResponse

        Private _ActivityType As ActivityTypeInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ActivityType() As ActivityTypeInfo
            Get
                Return _ActivityType
            End Get
            Set(ByVal value As ActivityTypeInfo)
                _ActivityType = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivityTypeReadListResponse
        Inherits BaseResponse

        Private _ActivityTypes As ActivityTypeInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ActivityTypes() As ActivityTypeInfo()
            Get
                Return _ActivityTypes
            End Get
            Set(ByVal value As ActivityTypeInfo())
                _ActivityTypes = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ActivityTypeSync3Response
        Inherits ActivityTypeReadListResponse

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

    <Serializable()> _
    Public Class ActivityTypeSync4Response
        Inherits ActivityTypeReadListResponse

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

