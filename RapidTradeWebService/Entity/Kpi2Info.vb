Imports System.Xml

Namespace Entity
    Public Class Kpi2Info
        Private _SupplierID As String
        Private _UserID As String
        Private _PeriodKey As String
        Private _KpiTypeID As String
        Private _Data As Decimal
        Private _PeriodDay As String
        Private _PeriodWeek As String
        Private _PeriodMonth As String
        Private _PeriodQuarter As String
        Private _PeriodYear As String
        Private _Deleted As Boolean
        Private _Status As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strUserId As String, ByVal strPeriodKey As String, _
                       ByVal strKpiTypeID As String, ByVal dData As Decimal, ByVal strPeriodDay As String, _
                       ByVal strPeriodWeek As String, ByVal strPeriodMonth As String, ByVal strPeriodQuarter As String, _
                       ByVal strPeriodYear As String, ByVal strStatus As String)
            _SupplierID = strSupplierId
            _UserID = strUserId
            _PeriodKey = strPeriodKey
            _KpiTypeID = strKpiTypeID
            _Data = dData
            _PeriodDay = strPeriodDay
            _PeriodWeek = strPeriodWeek
            _PeriodMonth = strPeriodMonth
            _PeriodQuarter = strPeriodQuarter
            _PeriodYear = strPeriodYear
            _Status = strStatus
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
        Public Property PeriodKey() As String
            Get
                Return _PeriodKey
            End Get
            Set(ByVal value As String)
                _PeriodKey = value
            End Set
        End Property
        Public Property KpiTypeID() As String
            Get
                Return _KpiTypeID
            End Get
            Set(ByVal value As String)
                _KpiTypeID = value
            End Set
        End Property
        Public Property Data() As Decimal
            Get
                Return _Data
            End Get
            Set(ByVal value As Decimal)
                _Data = value
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
        Public Property Deleted() As Boolean
            Get
                Return _Deleted
            End Get
            Set(ByVal value As Boolean)
                _Deleted = value
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
    End Class

End Namespace