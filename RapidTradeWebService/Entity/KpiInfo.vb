Imports System.Xml

Namespace Entity
    Public Class KpiInfo
        Private _KpiInfoID As String
        Private _SupplierID As String
        Private _UserID As String
        Private _KpiTypeID As String
        Private _KpiGroupId As String
        Private _KpiVersionID As String
        Private _PeriodKey As String
        Private _Data As Double
        Private _PeriodDay As String
        Private _PeriodWeek As String
        Private _PeriodMonth As String
        Private _AccntGroup As String
        Private _AccountID As String
        Private _ProductGroup As String
        Private _ProductID As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strKpiInfoID As String, ByVal strSuppId As String, ByVal strUserId As String, ByVal strTypeId As String, _
                       ByVal strGroupId As String, ByVal strVersionId As String, ByVal strPeriod As String, ByVal dData As Double, _
                       ByVal strPeriodDay As String, ByVal strPeriodWeek As String, ByVal strPeriodMonth As String, _
                       ByVal strAcctGrp As String, ByVal strAcctId As String, ByVal strProductGrp As String, _
                       ByVal strProdId As String)
            _KpiInfoID = strKpiInfoID
            _SupplierID = strSuppId
            _UserID = strUserId
            _KpiTypeID = strTypeId
            _KpiGroupId = strGroupId
            _KpiVersionID = strVersionId
            _PeriodKey = strPeriod
            _Data = dData
            _PeriodDay = strPeriodDay
            _PeriodWeek = strPeriodWeek
            _PeriodMonth = strPeriodMonth
            _AccntGroup = strAcctGrp
            _AccountID = strAcctId
            _ProductGroup = strProductGrp
            _ProductID = strProdId
        End Sub

        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property KpiInfoID() As String
            Get
                Return _KpiInfoID
            End Get
            Set(ByVal value As String)
                _KpiInfoID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property KpiTypeID() As String
            Get
                Return _KpiTypeID
            End Get
            Set(ByVal value As String)
                _KpiTypeID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property KpiGroupId() As String
            Get
                Return _KpiGroupId
            End Get
            Set(ByVal value As String)
                _KpiGroupId = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property KpiVersionID() As String
            Get
                Return _KpiVersionID
            End Get
            Set(ByVal value As String)
                _KpiVersionID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property PeriodKey() As String
            Get
                Return _PeriodKey
            End Get
            Set(ByVal value As String)
                _PeriodKey = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Data() As Double
            Get
                Return _Data
            End Get
            Set(ByVal value As Double)
                _Data = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property PeriodDay() As String
            Get
                Return _PeriodDay
            End Get
            Set(ByVal value As String)
                _PeriodDay = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property PeriodWeek() As String
            Get
                Return _PeriodWeek
            End Get
            Set(ByVal value As String)
                _PeriodWeek = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property PeriodMonth() As String
            Get
                Return _PeriodMonth
            End Get
            Set(ByVal value As String)
                _PeriodMonth = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AccntGroup() As String
            Get
                Return _AccntGroup
            End Get
            Set(ByVal value As String)
                _AccntGroup = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AccountID() As String
            Get
                Return _AccountID
            End Get
            Set(ByVal value As String)
                _AccountID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ProductGroup() As String
            Get
                Return _ProductGroup
            End Get
            Set(ByVal value As String)
                _ProductGroup = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property ProductID() As String
            Get
                Return _ProductID
            End Get
            Set(ByVal value As String)
                _ProductID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
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