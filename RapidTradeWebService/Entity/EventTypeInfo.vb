
Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class EventTypeInfo
        Private _EventTypeID As String
        Private _SupplierId As String
        Private _FollowonEventTypeID As String
        Private _KpiGroupID As String
        Private _TargetTypeID As String
        Private _FieldType As Integer
        Private _Label As String
        Private _DueDateAllowed As Boolean
        Private _DueTimeAllowed As Boolean
        Private _DefaultData As String
        Private _Size As Integer
        Private _ForAccntGrp As String
        Private _ForAccntID As String
        Private _KPITypeID As String
        Private _KPIVersionID As String
        Private _SendToCalendar As Boolean
        Private _LongDescription As String
        Private _KPIAddData As Boolean
        Private _EventGroup As String
        Private _AllowNote As Boolean
        Private _Deleted As Boolean


        Public Sub New()

        End Sub

        Public Sub New(ByVal strEventType As String, ByVal strSupplierId As String, ByVal intField As Integer, _
            ByVal strFollowonEventTypeID As String, ByVal strKpiGroupID As String, ByVal strTargetTypeID As String, _
            ByVal strLabel As String, ByVal bDueDtAllow As Boolean, ByVal bDueTimeAllow As Boolean, ByVal strDefault As String, _
            ByVal intSize As Integer, ByVal strForGrp As String, ByVal strID As String, ByVal strKpiType As String, ByVal strKpiVersion As String, _
            ByVal strLongDesc As String, ByVal bKpiAddData As Boolean, ByVal strEventGrp As String, ByVal bSend As Boolean, ByVal bAllowNote As Boolean)

            _EventTypeID = strEventType
            _SupplierId = strSupplierId
            _FollowonEventTypeID = strFollowonEventTypeID
            _KpiGroupID = strKpiGroupID
            _TargetTypeID = strTargetTypeID
            _FieldType = intField
            _Label = strLabel
            _DueDateAllowed = bDueDtAllow
            _DueTimeAllowed = bDueTimeAllow
            _DefaultData = strDefault
            _Size = intSize
            _ForAccntGrp = strForGrp
            _ForAccntID = strID
            _KPITypeID = strKpiType
            _KPIVersionID = strKpiVersion
            _SendToCalendar = bSend
            _LongDescription = strLongDesc
            _KPIAddData = bKpiAddData
            _EventGroup = strEventGrp
            _AllowNote = bAllowNote
        End Sub

        Public Property EventTypeID() As String
            Get
                Return _EventTypeID
            End Get
            Set(ByVal value As String)
                _EventTypeID = value
            End Set
        End Property
        Public Property SupplierId() As String
            Get
                Return _SupplierId
            End Get
            Set(ByVal value As String)
                _SupplierId = value
            End Set
        End Property
        Public Property FollowonEventTypeID() As String
            Get
                Return _FollowonEventTypeID
            End Get
            Set(ByVal value As String)
                _FollowonEventTypeID = value
            End Set
        End Property
        Public Property KpiGroupID() As String
            Get
                Return _KpiGroupID
            End Get
            Set(ByVal value As String)
                _KpiGroupID = value
            End Set
        End Property
        Public Property TargetTypeID() As String
            Get
                Return _TargetTypeID
            End Get
            Set(ByVal value As String)
                _TargetTypeID = value
            End Set
        End Property
        Public Property FieldType() As Integer
            Get
                Return _FieldType
            End Get
            Set(ByVal value As Integer)
                _FieldType = value
            End Set
        End Property
        Public Property Label() As String
            Get
                Return _Label
            End Get
            Set(ByVal value As String)
                _Label = value
            End Set
        End Property
        Public Property DueDateAllowed() As Boolean
            Get
                Return _DueDateAllowed
            End Get
            Set(ByVal value As Boolean)
                _DueDateAllowed = value
            End Set
        End Property
        Public Property DueTimeAllowed() As Boolean
            Get
                Return _DueTimeAllowed
            End Get
            Set(ByVal value As Boolean)
                _DueTimeAllowed = value
            End Set
        End Property
        Public Property DefaultData() As String
            Get
                Return _DefaultData
            End Get
            Set(ByVal value As String)
                _DefaultData = value
            End Set
        End Property
        Public Property Size() As Integer
            Get
                Return _Size
            End Get
            Set(ByVal value As Integer)
                _Size = value
            End Set
        End Property
        Public Property ForAccntGrp() As String
            Get
                Return _ForAccntGrp
            End Get
            Set(ByVal value As String)
                _ForAccntGrp = value
            End Set
        End Property
        Public Property ForAccntID() As String
            Get
                Return _ForAccntID
            End Get
            Set(ByVal value As String)
                _ForAccntID = value
            End Set
        End Property
        Public Property KPITypeID() As String
            Get
                Return _KPITypeID
            End Get
            Set(ByVal value As String)
                _KPITypeID = value
            End Set
        End Property
        Public Property KPIVersionID() As String
            Get
                Return _KPIVersionID
            End Get
            Set(ByVal value As String)
                _KPIVersionID = value
            End Set
        End Property
        Public Property SendToCalendar() As Boolean
            Get
                Return _SendToCalendar
            End Get
            Set(ByVal value As Boolean)
                _SendToCalendar = value
            End Set
        End Property
        Public Property LongDescription() As String
            Get
                Return _LongDescription
            End Get
            Set(ByVal value As String)
                _LongDescription = value
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
        Public Property KPIAddData() As Boolean
            Get
                Return _KPIAddData
            End Get
            Set(ByVal value As Boolean)
                _KPIAddData = value
            End Set
        End Property
        Public Property EventGroup() As String
            Get
                Return _EventGroup
            End Get
            Set(ByVal value As String)
                _EventGroup = value
            End Set
        End Property

        Public Property AllowNote() As Boolean
            Get
                Return _AllowNote
            End Get
            Set(ByVal value As Boolean)
                _AllowNote = value
            End Set
        End Property
    End Class
End Namespace