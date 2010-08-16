Public Class FormTypeInfo
    Private _SupplierID As String
    Private _FormTypeID As Integer
    Private _FormName As String
    Private _ForAccountID As String
    Private _ForAccountGrp As String
    Private _FormFields As FormFieldInfo()
    Private _MustSelectAccount As Boolean
    Private _Deleted As Boolean
    Public Sub New()
    End Sub
    Public Sub New(ByVal strSuppId As String, ByVal iFormType As Integer, ByVal strFormName As String, _
                   ByVal strForAcctId As String, ByVal strForAcctGrp As String, ByVal bSelect As Boolean)
        _SupplierID = strSuppId
        _FormTypeID = iFormType
        _FormName = strFormName
        _ForAccountID = strForAcctId
        _ForAccountGrp = strForAcctGrp
        _MustSelectAccount = bSelect
    End Sub
    Public Property SupplierID() As String
        Get
            Return _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
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
    Public Property FormName() As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property
    Public Property ForAccountID() As String
        Get
            Return _ForAccountID
        End Get
        Set(ByVal value As String)
            _ForAccountID = value
        End Set
    End Property
    Public Property ForAccountGrp() As String
        Get
            Return _ForAccountGrp
        End Get
        Set(ByVal value As String)
            _ForAccountGrp = value
        End Set
    End Property
    Public Property MustSelectAccount() As Boolean
        Get
            Return _MustSelectAccount
        End Get
        Set(ByVal value As Boolean)
            _MustSelectAccount = value
        End Set
    End Property
    Public Property FormFields() As FormFieldInfo()
        Get
            Return _FormFields
        End Get
        Set(ByVal value As FormFieldInfo())
            _FormFields = value
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