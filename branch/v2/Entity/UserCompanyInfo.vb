Imports System.Xml

Namespace Entity
    Public Class UserCompanyInfo
        Private _UserId As String
        Private _CompanyId As String
        Private _BranchId As String
        Private _SupplierID As String
        Private _Deleted As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal strUserId As String, ByVal strCompanyId As String, ByVal strBranchId As String, _
            ByVal strSuppId As String, ByVal bDeleted As Boolean)
            _UserId = strUserId
            _CompanyId = strCompanyId
            _BranchId = strBranchId
            _SupplierID = strSuppId
            _Deleted = bDeleted
        End Sub

        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property
       
        Public Property CompanyId() As String
            Get
                Return _CompanyId
            End Get
            Set(ByVal value As String)
                _CompanyId = value
            End Set
        End Property

        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
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
