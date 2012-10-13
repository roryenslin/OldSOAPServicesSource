Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class ProductCategoryInfo
        Private _SupplierID As String
        Private _CategoryID As Integer
        Private _ParentCategoryID As Integer
        Private _Path As String
        Private _Name As String
        Private _Label As String
        Private _Description As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierID As String, ByVal iCategoryId As Integer, ByVal iParentCategory As Integer, _
                       ByVal strPath As String, ByVal strName As String, ByVal strLabel As String, ByVal strDescription As String)
            _SupplierID = strSupplierID
            _CategoryID = iCategoryId
            _ParentCategoryID = iParentCategory
            _Path = strPath
            _Name = strName
            _Label = strLabel
            _Description = strDescription
            _Deleted = False
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property
        Public Property CategoryID() As Integer
            Get
                Return _CategoryID
            End Get
            Set(ByVal value As Integer)
                _CategoryID = value
            End Set
        End Property
        Public Property ParentCategoryID() As Integer
            Get
                Return _ParentCategoryID
            End Get
            Set(ByVal value As Integer)
                _ParentCategoryID = value
            End Set
        End Property
        Public Property Path() As String
            Get
                Return _Path
            End Get
            Set(ByVal value As String)
                _Path = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
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
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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

