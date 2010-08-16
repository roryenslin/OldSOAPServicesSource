Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class TargetTypeInfo
        Private _SupplierID As String
        Private _TargetTypeID As String
        Private _PeriodType As String
        Private _Description As String
        Private _FieldType As Integer
        Private _Deleted As Boolean
        Private _UseUsersData As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strTargetTypeID As String, _
                       ByVal strPeriodType As String, ByVal strDescription As String, _
                       ByVal iFieldType As Integer, Optional ByVal bUseUsersData As Boolean = False)
            _SupplierID = strSupplierId
            _TargetTypeID = strTargetTypeID
            _PeriodType = strPeriodType
            _Description = strDescription
            _FieldType = iFieldType
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
        Public Property TargetTypeID() As String
            Get
                Return _TargetTypeID
            End Get
            Set(ByVal value As String)
                _TargetTypeID = value
            End Set
        End Property
        Public Property PeriodType() As String
            Get
                Return _PeriodType
            End Get
            Set(ByVal value As String)
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

