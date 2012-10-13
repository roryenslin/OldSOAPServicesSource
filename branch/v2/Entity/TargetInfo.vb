Imports System.Xml

Namespace Entity

    Public Class TargetInfo
        Private _SupplierID As String
        Private _UserID As String
        Private _TargetTypeID As String
        Private _PeriodKey As String
        Private _PeriodDay As String
        Private _PeriodWeek As String
        Private _PeriodMonth As String
        Private _PeriodQuarter As String
        Private _PeriodYear As String
        Private _Data As String
        Private _Deleted As Boolean

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal strSuppID As String, ByVal strUserID As String, ByVal strTarget As String, ByVal strPeriodKey As String, _
                       ByVal strPeriodDay As String, ByVal strPeriodWeek As String, ByVal strPeriodMonth As String, _
                       ByVal strPeriodQuarter As String, ByVal strPeriodYear As String, ByVal dData As String)
            _SupplierID = strSuppID
            _UserID = strUserID
            _TargetTypeID = strTarget
            _PeriodKey = strPeriodKey
            _PeriodDay = strPeriodDay
            _PeriodWeek = strPeriodWeek
            _PeriodMonth = strPeriodMonth
            _PeriodQuarter = strPeriodQuarter
            _PeriodYear = strPeriodYear
            _Data = dData
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
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

        Public Property TargetTypeID() As String
            Get
                Return _TargetTypeID
            End Get
            Set(ByVal value As String)
                _TargetTypeID = value
            End Set
        End Property

        Public Property PeriodKey() As String
            Get
                Return _PeriodKey
            End Get
            Set(ByVal value As String)
                _PeriodKey = value
            End Set
        End Property

        Public Property PeriodDay() As String
            Get
                Return _PeriodDay
            End Get
            Set(ByVal value As String)
                _PeriodDay = value
            End Set
        End Property

        Public Property PeriodWeek() As String
            Get
                Return _PeriodWeek
            End Get
            Set(ByVal value As String)
                _PeriodWeek = value
            End Set
        End Property

        Public Property PeriodMonth() As String
            Get
                Return _PeriodMonth
            End Get
            Set(ByVal value As String)
                _PeriodMonth = value
            End Set
        End Property

        Public Property PeriodQuarter() As String
            Get
                Return _PeriodQuarter
            End Get
            Set(ByVal value As String)
                _PeriodQuarter = value
            End Set
        End Property

        Public Property PeriodYear() As String
            Get
                Return _PeriodYear
            End Get
            Set(ByVal value As String)
                _PeriodYear = value
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
End Namespace

