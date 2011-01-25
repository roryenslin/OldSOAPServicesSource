Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class OptionReadSingleResponse
        Inherits BaseResponse
        Private _Option As OptionInfo

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property OptionData() As OptionInfo
            Get
                Return _Option
            End Get
            Set(ByVal value As OptionInfo)
                _Option = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class OptionFindSupplierResponse
        Inherits BaseResponse
        Public Sub New()

        End Sub

        Private _supplierid As String
        Public Property SupplierID() As String
            Get
                Return _supplierid
            End Get
            Set(ByVal value As String)
                _supplierid = value
            End Set
        End Property

    End Class


    <Serializable()> _
    Public Class OptionReadListResponse
        Inherits BaseResponse

        Private _Options As OptionInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Options() As OptionInfo()
            Get
                Return _Options
            End Get
            Set(ByVal value As OptionInfo())
                _Options = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class OptionSync3Response
        Inherits OptionReadListResponse

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
