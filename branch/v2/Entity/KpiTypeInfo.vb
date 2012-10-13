Imports System.Xml

Namespace Entity
    <Serializable()> _
    Public Class KpiTypeInfo
        Private _SupplierId As String
        Private _KpiGroupID As String
        Private _KpiTypeID As String
        Private _FieldType As Integer
        Private _Size As Integer
        Private _SortOrder As Integer
        Private _Label As String
        Private _DefaultData As String
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strSuppId As String, ByVal strGrpId As String, ByVal strTypeId As String, _
                       ByVal intFieldType As Integer, ByVal intSize As Integer, ByVal intSort As Integer, _
                       ByVal strLabel As String, ByVal strDefault As String)
            _SupplierId = strSuppId
            _KpiGroupID = strGrpId
            _KpiTypeID = strTypeId
            _FieldType = intFieldType
            _Size = intSize
            _SortOrder = intSort
            _Label = strLabel
            _DefaultData = strDefault
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
        Public Property KpiTypeID() As String
            Get
                Return _KpiTypeID
            End Get
            Set(ByVal value As String)
                _KpiTypeID = value
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
        Public Property Size() As Integer
            Get
                Return _Size
            End Get
            Set(ByVal value As Integer)
                _Size = value
            End Set
        End Property
        Public Property SortOrder() As Integer
            Get
                Return _SortOrder
            End Get
            Set(ByVal value As Integer)
                _SortOrder = value
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
        Public Property DefaultData() As String
            Get
                Return _DefaultData
            End Get
            Set(ByVal value As String)
                _DefaultData = value
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