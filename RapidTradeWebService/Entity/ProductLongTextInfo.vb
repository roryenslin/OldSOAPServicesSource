Imports System.Xml
Namespace Entity

    <Serializable()> _
    Public Class ProductLongTextInfo

#Region "Properties"
        Private _SupplierID As String
        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property

        Private _ProductID As String
        Public Property ProductID() As String
            Get
                Return _ProductID
            End Get
            Set(ByVal value As String)
                _ProductID = value
            End Set
        End Property

        Private _TabID As Integer
        Public Property TabID() As Integer
            Get
                Return _TabID
            End Get
            Set(ByVal value As Integer)
                _TabID = value
            End Set
        End Property


        Private _LongText As String
        Public Property LongText() As String
            Get
                Return _LongText
            End Get
            Set(ByVal value As String)
                _LongText = value
            End Set
        End Property
#End Region
#Region "Methods"
        Public Sub New()

        End Sub

        Public Sub New(ByVal supplierid As String, ByVal productid As String, ByVal tabid As Integer, ByVal longtext As String)
            Me.SupplierID = supplierid
            Me.ProductID = productid
            Me.TabID = tabid
            Me.LongText = longtext
        End Sub
#End Region

    End Class
End Namespace