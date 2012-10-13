Imports System.Xml
Imports RapidTradeWebService.Common

Namespace Entity

    <Serializable()> _
    Public Class EventInfo
        Private _EventID As String
        Private _SupplierID As String
        Private _EventTypeID As String
        Private _FollowOnEvent As String
        Private _Data As String
        Private _EndDate As String
        Private _DueDate As String
        Private _UserID As String
        Private _AccountID As String
        Private _Status As String
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strEventID As String, ByVal strSuppID As String, ByVal strEventTypeId As String, _
                       ByVal strFollowOnEvent As String, ByVal strData As String, ByVal dtEndDt As String, _
                       ByVal dtDueDt As String, ByVal strUserId As String, ByVal strAccountId As String, _
                       ByVal strStatus As String)
            _EventID = strEventID
            _SupplierID = strSuppID
            _EventTypeID = strEventTypeId
            _FollowOnEvent = strFollowOnEvent
            _Data = strData
            _EndDate = dtEndDt
            _DueDate = dtDueDt
            _UserID = strUserId
            _AccountID = strAccountId
            _Status = strStatus
        End Sub

        Public Property EventID() As String
            Get
                Return _EventID
            End Get
            Set(ByVal value As String)
                _EventID = value
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
        Public Property EventTypeID() As String
            Get
                Return _EventTypeID
            End Get
            Set(ByVal value As String)
                _EventTypeID = value
            End Set
        End Property
        Public Property FollowOnEvent() As String
            Get
                Return _FollowOnEvent
            End Get
            Set(ByVal value As String)
                _FollowOnEvent = value
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