Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class ProductImageInfo
        Private _SupplierID As String
        Private _ProductID As String
        Private _ProductImage As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierID As String, ByVal strProductID As String, ByVal strProductImage As String)
            _SupplierID = strSupplierID
            _ProductID = strProductID
            _ProductImage = strProductImage
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
        Public Property ProductID() As String
            Get
                Return _ProductID
            End Get
            Set(ByVal value As String)
                _ProductID = value
            End Set
        End Property
        Public Property ImageName() As String
            Get
                Return _ProductImage
            End Get
            Set(ByVal value As String)
                _ProductImage = value
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
