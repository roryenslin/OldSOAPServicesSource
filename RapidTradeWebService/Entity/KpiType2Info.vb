Imports System.Xml

Namespace Entity
    <Serializable()> _
    Public Class KpiType2Info
        Private _SupplierID As String
        Private _KpiTypeID As String
        Private _TargetTypeID As String
        Private _PeriodType As Integer
        Private _Description As String
        Private _FieldType As Integer
        Private _Deleted As Boolean
        Private _Status As String
        Private _UseUsersData As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strKpiTypeId As String, ByVal strTargetTypeId As String, _
                       ByVal iPeriodType As Integer, ByVal strDescription As String, ByVal iFieldType As Integer, _
                       ByVal strStatus As String, Optional ByVal bUseUsersData As Boolean = False)
            _SupplierID = strSupplierId
            _KpiTypeID = strKpiTypeId
            _TargetTypeID = strTargetTypeId
            _PeriodType = iPeriodType
            _Description = strDescription
            _FieldType = iFieldType
            _Status = strStatus
            _UseUsersData = bUseUsersData
        End Sub
        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
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
        Public Property TargetTypeID() As String
            Get
                Return _TargetTypeID
            End Get
            Set(ByVal value As String)
                _TargetTypeID = value
            End Set
        End Property
        Public Property PeriodType() As Integer
            Get
                Return _PeriodType
            End Get
            Set(ByVal value As Integer)
                _PeriodType = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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
        Public Property UseUsersData() As Boolean
            Get
                Return _UseUsersData
            End Get
            Set(ByVal value As Boolean)
                _UseUsersData = value
            End Set
        End Property
    End Class
End Namespace

