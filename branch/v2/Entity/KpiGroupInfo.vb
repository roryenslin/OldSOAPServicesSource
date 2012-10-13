Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class KpiGroupInfo
        Private _SupplierId As String
        Private _KpiGroupID As String
        Private _TargetTypeID As String
        Private _Description As String
        Private _PeriodType As Integer
        Private _ForAccntGrp As String
        Private _ForAccountId As String
        Private _ForProductgrp As String
        Private _ForProductID As String
        Private _LongDescription As String
        Private _KpiVersions As kpiVersionInfo()
        Private _KpiTypes As KpiTypeInfo()
        Private _HideByDefault As Boolean
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppID As String, ByVal strGroupId As String, ByVal strTargetTypId As String, ByVal strDesc As String, _
                       ByVal dPeriod As Integer, ByVal strAcctGrp As String, ByVal strAcctId As String, ByVal strProdGrp As String, _
                       ByVal strProdId As String, ByVal strLongDesc As String, ByVal bHideByDefault As Boolean, _
                       ByVal arrVersions As KpiVersionInfo(), ByVal arrTypes As KpiTypeInfo())
            _SupplierId = strSuppID
            _KpiGroupID = strGroupId
            _TargetTypeID = strTargetTypId
            _Description = strDesc
            _PeriodType = dPeriod
            _ForAccntGrp = strAcctGrp
            _ForAccountId = strAcctId
            _ForProductgrp = strProdGrp
            _ForProductID = strProdId
            _LongDescription = strLongDesc
            _HideByDefault = bHideByDefault
            _KpiVersions = arrVersions
            _KpiTypes = arrTypes
        End Sub

        Public Property SupplierId() As String
            Get
                Return _SupplierId
            End Get
            Set(ByVal value As String)
                _SupplierId = value
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
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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
        Public Property ForAccntGrp() As String
            Get
                Return _ForAccntGrp
            End Get
            Set(ByVal value As String)
                _ForAccntGrp = value
            End Set
        End Property
        Public Property ForAccountId() As String
            Get
                Return _ForAccountId
            End Get
            Set(ByVal value As String)
                _ForAccountId = value
            End Set
        End Property
        Public Property ForProductgrp() As String
            Get
                Return _ForProductgrp
            End Get
            Set(ByVal value As String)
                _ForProductgrp = value
            End Set
        End Property
        Public Property ForProductID() As String
            Get
                Return _ForProductID
            End Get
            Set(ByVal value As String)
                _ForProductID = value
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
        Public Property KpiVersions() As KpiVersionInfo()
            Get
                Return _KpiVersions
            End Get
            Set(ByVal value As KpiVersionInfo())
                _KpiVersions = value
            End Set
        End Property
        Public Property KpiTypes() As KpiTypeInfo()
            Get
                Return _KpiTypes
            End Get
            Set(ByVal value As KpiTypeInfo())
                _KpiTypes = value
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
        Public Property HideByDefault() As Boolean
            Get
                Return _HideByDefault
            End Get
            Set(ByVal value As Boolean)
                _HideByDefault = value
            End Set
        End Property
    End Class
End Namespace