Imports System.Xml
Imports RapidTradeWebService.Common

Namespace Entity

    <Serializable()> _
    Public Class ActivityInfo
        Private _ActivityID As String
        Private _SupplierID As String
        Private _ActivityTypeID As String
        Private _FollowOnActivity As String
        Private _Data As String
        Private _EndDate As String
        Private _DueDate As String
        Private _UserID As String
        Private _AccountID As String
        Private _Status As String
        Private _ContactID As String
        Private _Deleted As Boolean

        Private _Latitude As String
        Public Property Latitude() As String
            Get
                Return _Latitude
            End Get
            Set(ByVal value As String)
                _Latitude = value
            End Set
        End Property

        Private _Longitude As String
        Public Property Longitude() As String
            Get
                Return _Longitude
            End Get
            Set(ByVal value As String)
                _Longitude = value
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Sub New(ByVal strActivityID As String, ByVal strSuppID As String, ByVal strActivityTypeId As String, _
                       ByVal strFollowOnActivity As String, ByVal strData As String, ByVal dtEndDt As String, _
                       ByVal dtDueDt As String, ByVal strUserId As String, ByVal strAccountId As String, ByVal strContactId As String, _
                       ByVal strStatus As String, ByVal latitude As String, ByVal longitude As String)
            _ActivityID = strActivityID
            _SupplierID = strSuppID
            _ActivityTypeID = strActivityTypeId
            _FollowOnActivity = strFollowOnActivity
            _Data = strData
            _EndDate = dtEndDt
            _DueDate = dtDueDt
            _UserID = strUserId
            _AccountID = strAccountId
            _Status = strStatus
            _ContactID = strContactId
            _Latitude = latitude
            _Longitude = longitude
        End Sub

        Public Property ActivityID() As String
            Get
                Return _ActivityID
            End Get
            Set(ByVal value As String)
                _ActivityID = value
            End Set
        End Property
        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        Public Property ActivityTypeID() As String
            Get
                Return _ActivityTypeID
            End Get
            Set(ByVal value As String)
                _ActivityTypeID = value
            End Set
        End Property
        Public Property FollowOnActivity() As String
            Get
                Return _FollowOnActivity
            End Get
            Set(ByVal value As String)
                _FollowOnActivity = value
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
        Public Property EndDate() As String
            Get
                Return _EndDate
            End Get
            Set(ByVal value As String)
                _EndDate = value
            End Set
        End Property
        Public Property DueDate() As String
            Get
                Return _DueDate
            End Get
            Set(ByVal value As String)
                _DueDate = value
            End Set
        End Property
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
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
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
        Public Property ContactID() As String
            Get
                Return _ContactID
            End Get
            Set(ByVal value As String)
                _ContactID = value
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

        Public Overrides Function ToString() As String
            Return SerializationManager.Serialize(Me)
        End Function
    End Class
End Namespace