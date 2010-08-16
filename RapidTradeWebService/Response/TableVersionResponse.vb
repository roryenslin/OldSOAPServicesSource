Imports RapidTradeWebService.Entity

Namespace Response
    Public Class TableVersionResponse
        Inherits BaseResponse

        Private _TableVersion As Integer

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property TableVersion() As Integer
            Get
                Return _TableVersion
            End Get
            Set(ByVal value As Integer)
                _TableVersion = value
            End Set
        End Property
    End Class

    Public Class TableVersionsResponse
        Inherits BaseResponse

        Private _TableVersions() As Integer

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property TableVersions() As Integer()
            Get
                Return _TableVersions
            End Get
            Set(ByVal value As Integer())
                _TableVersions = value
            End Set
        End Property
    End Class

    Public Class TableUpdateResponse
        Inherits BaseResponse

        Private _TableInfo() As TableUpdateInfo

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property TableVersions() As TableUpdateInfo()
            Get
                Return _TableInfo
            End Get
            Set(ByVal value As TableUpdateInfo())
                _TableInfo = value
            End Set
        End Property
    End Class

    Public Class TableSyncResponse
        Inherits BaseResponse

        Private _TableSync As New TableSyncInfo

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property TableSync() As TableSyncInfo
            Get
                Return _TableSync
            End Get
            Set(ByVal value As TableSyncInfo)
                _TableSync = value
            End Set
        End Property
    End Class
End Namespace

