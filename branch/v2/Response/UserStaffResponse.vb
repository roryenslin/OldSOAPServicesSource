Imports System.Collections.Generic
Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class UserStaffResponse
        Inherits BaseResponse

        Private _Staff As List(Of String)

        Public Sub New()
            _Staff = New List(Of String)
        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Staff() As List(Of String)
            Get
                Return _Staff
            End Get
            Set(ByVal value As List(Of String))
                _Staff = value
            End Set
        End Property
    End Class

End Namespace

