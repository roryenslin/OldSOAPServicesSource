Namespace Response
    <Serializable()> _
    Public Class BaseResponse
        Private _Status As Boolean
        Private _Errors As String()

        Public Sub New()

        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Status() As Boolean
            Get
                Return _Status
            End Get
            Set(ByVal value As Boolean)
                _Status = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Errors() As String()
            Get
                Return _Errors
            End Get
            Set(ByVal value As String())
                _Errors = value
            End Set
        End Property
    End Class
End Namespace

