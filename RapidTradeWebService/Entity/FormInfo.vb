Public Class FormInfo
    Private _SupplierID As String
    Private _FormID As String
    Private _FormTypeID As Integer
    Private _FormFieldID As Integer
    Private _AccountID As String
    Private _FormDate As DateTime
    Private _Data As String
    Private _Deleted As Boolean
    Public Sub New()
    End Sub
    Public Sub New(ByVal strSuppId As String, ByVal strFormId As String, ByVal iFormType As Integer, ByVal iFormField As Integer, _
                   ByVal strAccountId As String, ByVal dtFormDate As DateTime, ByVal strData As String)
        _SupplierID = strSuppId
        _FormID = strFormId
        _FormTypeID = iFormType
        _FormFieldID = iFormField
        _AccountID = strAccountId
        _FormDate = dtFormDate
        _Data = strData
    End Sub
    Public Property SupplierID() As String
        Get
            Return _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
        End Set
    End Property
    Public Property FormID() As String
        Get
            Return _FormID
        End Get
        Set(ByVal value As String)
            _FormID = value
        End Set
    End Property
    Public Property FormTypeID() As Integer
        Get
            Return _FormTypeID
        End Get
        Set(ByVal value As Integer)
            _FormTypeID = value
        End Set
    End Property
    Public Property FormFieldID() As Integer
        Get
            Return _FormFieldID
        End Get
        Set(ByVal value As Integer)
            _FormFieldID = value
        End Set
    End Property
    Public Property AccountID() As String
        Get
            Return _AccountID
        End Get
        Set(ByVal value As String)
            _AccountID = value
        End Set
    End Property
    Public Property FormDate() As DateTime
        Get
            Return _FormDate
        End Get
        Set(ByVal value As DateTime)
            _FormDate = value
        End Set
    End Property
    Public Property Data() As String
        Get
            Return _Data
        End Get
        Set(ByVal value As String)
            _Data = value
        End Set
    End Property
    Public Property Deleted() As Boolean
        Get
            Return _Deleted
        End Get
        Set(ByVal value As Boolean)
            _Deleted = value
        End Set
    End Property
End Class