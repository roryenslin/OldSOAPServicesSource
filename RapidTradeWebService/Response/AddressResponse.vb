Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class AddressReadListResponse
        Inherits BaseResponse

        Private _Addresss As AddressInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlArrayItem(GetType(AddressInfo))> _
        Public Property Addresss() As AddressInfo()
            Get
                Return _Addresss
            End Get
            Set(ByVal value As AddressInfo())
                _Addresss = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class AddressSync4Response
        Inherits AddressReadListResponse

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
