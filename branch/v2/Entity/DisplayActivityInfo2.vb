Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class DisplayActivityInfo2

        Private _ActivityTypeID As String
        Private _Label As String
        Private _Data As String
        Private _Time As String
        Private _AccountName As String
        Private _ActivityID As String
        Private _Latitude As String
        Private _Longitude As String
        Private _EventID As String
        Private _ContactID As String
        Private _EndTime As String
        Private _Notes As String
        Private _ContactName As String

        Property ActivityID() As String
            Get
                Return _ActivityID
            End Get
            Set(ByVal value As String)
                _ActivityID = value
            End Set
        End Property
        Property Notes() As String
            Get
                Return _Notes
            End Get
            Set(ByVal value As String)
                _Notes = value
            End Set
        End Property

        Property ContactName() As String
            Get
                Return _ContactName
            End Get
            Set(ByVal value As String)
                _ContactName = value
            End Set
        End Property

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

        Property Latitude() As String
            Get
                Return _Latitude
            End Get
            Set(ByVal value As String)
                _Latitude = value
            End Set
        End Property

        Property Longitude() As String
            Get
                Return _Longitude
            End Get
            Set(ByVal value As String)
                _Longitude = value
            End Set
        End Property

        Property EventID() As String
            Get
                Return _EventID
            End Get
            Set(ByVal value As String)
                _EventID = value
            End Set
        End Property

        Property ContactID() As String
            Get
                Return _ContactID
            End Get
            Set(ByVal value As String)
                _ContactID = value
            End Set
        End Property

        Property EndTime() As String
            Get
                Return _EndTime
            End Get
            Set(ByVal value As String)
                _EndTime = value
            End Set
        End Property

    End Class
End Namespace