Public Class EmailInfo
    Private _From As String
    Private _To As String
    Private _Subject As String
    Private _Cc As String
    Private _IsHTML As Boolean
    Private _MailContent As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strCC As String, ByVal bHTML As Boolean, ByVal strContent As String)
        _From = strFrom
        _To = strTo
        _Subject = strSubject
        _Cc = strCC
        _IsHTML = bHTML
        _MailContent = strContent
    End Sub


    Public Property MailFrom() As String
        Get
            Return _From
        End Get
        Set(ByVal value As String)
            _From = value
        End Set
    End Property

    Public Property MailContent() As String
        Get
            Return _MailContent
        End Get
        Set(ByVal value As String)
            _MailContent = value
        End Set
    End Property

    Public Property MailTo() As String
        Get
            Return _To
        End Get
        Set(ByVal value As String)
            _To = value
        End Set
    End Property
    Public Property CC() As String
        Get
            Return _Cc
        End Get
        Set(ByVal value As String)
            _Cc = value
        End Set
    End Property
    Public Property Subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
        End Set
    End Property
    Public Property IsHTML() As Boolean
        Get
            Return _IsHTML
        End Get
        Set(ByVal value As Boolean)
            _IsHTML = value
        End Set
    End Property

End Class
