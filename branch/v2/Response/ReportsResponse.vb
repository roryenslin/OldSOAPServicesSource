Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ReportReadSingleResponse
        Inherits BaseResponse

        Private _Report As ReportInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Report() As ReportInfo
            Get
                Return _Report
            End Get
            Set(ByVal value As ReportInfo)
                _Report = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ReportReadListResponse
        Inherits BaseResponse

        Private _Reports As ReportInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Reports() As ReportInfo()
            Get
                Return _Reports
            End Get
            Set(ByVal value As ReportInfo())
                _Reports = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ExecuteReportResponse
        Inherits BaseResponse

        Private _Result As String()()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Result() As String()()
            Get
                Return _Result
            End Get
            Set(ByVal value As String()())
                _Result = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ReportSync3Response
        Inherits ReportReadListResponse

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

