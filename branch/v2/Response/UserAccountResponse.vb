Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class UserAccountSync4Response
        Inherits BaseResponse

        Private _UsersAccounts As UserAccountInfo()
        Private _LastVersion As Integer

        Public Property LastVersion() As Integer
            Get
                Return _LastVersion
            End Get
            Set(ByVal value As Integer)
                _LastVersion = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserAccounts() As UserAccountInfo()
            Get
                Return _UsersAccounts
            End Get
            Set(ByVal value As UserAccountInfo())
                _UsersAccounts = value
            End Set
        End Property

    End Class
End Namespace


