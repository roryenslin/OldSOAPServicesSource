Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class DisplayActivityInfo

        Private _ActivityTypeID As String
        Private _Label As String
        Private _Data As String
        Private _Time As String
        Private _AccountName As String


        Public Sub New()
        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ActivityTypeID() As String
            Get
                Return _ActivityTypeID
            End Get
            Set(ByVal value As String)
                _ActivityTypeID = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Data() As String
            Get
                Return _Data
            End Get
            Set(ByVal value As String)
                _Data = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Label() As String
            Get
                Return _Label
            End Get
            Set(ByVal value As String)
                _Label = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Time() As String
            Get
                Return _Time
            End Get
            Set(ByVal value As String)
                _Time = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AccountName() As String
            Get
                Return _AccountName
            End Get
            Set(ByVal value As String)
                _AccountName = value
            End Set
        End Property

    End Class
End Namespace