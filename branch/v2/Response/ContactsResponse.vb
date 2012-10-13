Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ContactReadSingleResponse
        Inherits BaseResponse

        Private _Contact As ContactInfo

        Public Sub New()

        End Sub

        Public Property Contact() As ContactInfo
            Get
                Return _Contact
            End Get
            Set(ByVal value As ContactInfo)
                _Contact = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ContactReadListResponse
        Inherits BaseResponse

        Private _Contacts As ContactInfo()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlArrayItem(GetType(ContactInfo))> _
        Public Property Contacts() As ContactInfo()
            Get
                Return _Contacts
            End Get
            Set(ByVal value As ContactInfo())
                _Contacts = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ContactSync3Response
        Inherits ContactReadListResponse

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
