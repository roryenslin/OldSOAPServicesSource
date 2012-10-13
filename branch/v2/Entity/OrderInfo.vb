Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class OrderInfo
        Private _OrderID As String
        Private _SupplierID As String
        Private _UserID As String
        Private _AccountID As String
        Private _BranchID As String
        Private _Status As String
        Private _OrderNumber As String
        Private _CreateDate As DateTime
        Private _RequiredByDate As datetime
        Private _Reference As String
        Private _DeliveryMethod As String
        Private _Comments As String
        Private _Type As String
        Private _DeliveryName As String
        Private _UserField01 As String
        Private _UserField02 As String
        Private _UserField03 As String
        Private _UserField04 As String
        Private _UserField05 As String
        Private _UserField06 As String
        Private _UserField07 As String
        Private _UserField08 As String
        Private _UserField09 As String
        Private _UserField10 As String
        Private _DeliveryAddress1 As String
        Private _DeliveryAddress2 As String
        Private _DeliveryAddress3 As String
        Private _DeliveryPostCode As String
        Private _PostedToERP As Boolean
        Private _ERPOrderNumber As String
        Private _ERPStatus As String
        Private _OrderItems As List(Of OrderItemInfo)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal strOrderID As String, ByVal strSupplierID As String, ByVal strUserID As String, ByVal strAccountID As String, ByVal strBranchID As String, _
                       ByVal strStatus As String, ByVal strOrderNumber As String, ByVal dtCreateDate As DateTime, ByVal dtRequiredByDate As DateTime, _
                       ByVal strReference As String, ByVal strDeliveryMethod As String, ByVal strComments As String, ByVal strType As String, _
                       ByVal strDeliveryName As String, ByVal intDeliveryAddressID As Integer, ByVal strUserField01 As String, _
                       ByVal strUserField02 As String, ByVal strUserField03 As String, ByVal strUserField04 As String, ByVal strUserField05 As String, _
                       ByVal strUserField06 As String, ByVal strUserField07 As String, ByVal strUserField08 As String, ByVal strUserField09 As String, _
                       ByVal strUserField10 As String, ByVal strDelAddress1 As String, ByVal strDelAddress2 As String, ByVal strDelAddress3 As String, ByVal strDelPostCode As String, _
                       ByVal bPostedToERP As Boolean, ByVal strERPOrderNumber As String, ByVal erpStatus As String)
            _OrderID = strOrderID
            _UserID = strUserID
            _AccountID = strAccountID
            _BranchID = strBranchID
            _Status = strStatus
            _OrderNumber = strOrderNumber
            _CreateDate = dtCreateDate
            _RequiredByDate = dtRequiredByDate
            _Reference = strReference
            _DeliveryMethod = strDeliveryMethod
            _Comments = strComments
            _Type = strType
            _DeliveryName = strDeliveryName
            _UserField01 = strUserField01
            _UserField02 = strUserField02
            _UserField03 = strUserField03
            _UserField04 = strUserField04
            _UserField05 = strUserField05
            _UserField06 = strUserField06
            _UserField07 = strUserField07
            _UserField08 = strUserField08
            _UserField09 = strUserField09
            _UserField10 = strUserField10
            _DeliveryAddress1 = strDelAddress1
            _DeliveryAddress2 = strDelAddress2
            _DeliveryAddress3 = strDelAddress3
            _DeliveryPostCode = strDelPostCode
            _ERPOrderNumber = strERPOrderNumber
            _PostedToERP = bPostedToERP
            _ERPStatus = erpStatus
        End Sub

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property

        Public Property OrderID() As String
            Get
                Return _OrderID
            End Get
            Set(ByVal value As String)
                _OrderID = value
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
        Public Property AccountID() As String
            Get
                Return _AccountID
            End Get
            Set(ByVal value As String)
                _AccountID = value
            End Set
        End Property
        Public Property BranchID() As String
            Get
                Return _BranchID
            End Get
            Set(ByVal value As String)
                _BranchID = value
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
        Public Property OrderNumber() As String
            Get
                Return _OrderNumber
            End Get
            Set(ByVal value As String)
                _OrderNumber = value
            End Set
        End Property
        Public Property CreateDate() As DateTime
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As DateTime)
                _CreateDate = value
            End Set
        End Property
        Public Property RequiredByDate() As DateTime
            Get
                Return _RequiredByDate
            End Get
            Set(ByVal value As DateTime)
                _RequiredByDate = value
            End Set
        End Property
        Public Property Reference() As String
            Get
                Return _Reference
            End Get
            Set(ByVal value As String)
                _Reference = value
            End Set
        End Property
        Public Property DeliveryMethod() As String
            Get
                Return _DeliveryMethod
            End Get
            Set(ByVal value As String)
                _DeliveryMethod = value
            End Set
        End Property
        Public Property Comments() As String
            Get
                Return _Comments
            End Get
            Set(ByVal value As String)
                _Comments = value
            End Set
        End Property
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property
        Public Property DeliveryName() As String
            Get
                Return _DeliveryName
            End Get
            Set(ByVal value As String)
                _DeliveryName = value
            End Set
        End Property

        Public Property UserField01() As String
            Get
                Return _UserField01
            End Get
            Set(ByVal value As String)
                _UserField01 = value
            End Set
        End Property
        Public Property UserField02() As String
            Get
                Return _UserField02
            End Get
            Set(ByVal value As String)
                _UserField02 = value
            End Set
        End Property
        Public Property UserField03() As String
            Get
                Return _UserField03
            End Get
            Set(ByVal value As String)
                _UserField03 = value
            End Set
        End Property
        Public Property UserField04() As String
            Get
                Return _UserField04
            End Get
            Set(ByVal value As String)
                _UserField04 = value
            End Set
        End Property
        Public Property UserField05() As String
            Get
                Return _UserField05
            End Get
            Set(ByVal value As String)
                _UserField05 = value
            End Set
        End Property
        Public Property UserField06() As String
            Get
                Return _UserField06
            End Get
            Set(ByVal value As String)
                _UserField06 = value
            End Set
        End Property
        Public Property UserField07() As String
            Get
                Return _UserField07
            End Get
            Set(ByVal value As String)
                _UserField07 = value
            End Set
        End Property
        Public Property UserField08() As String
            Get
                Return _UserField08
            End Get
            Set(ByVal value As String)
                _UserField08 = value
            End Set
        End Property
        Public Property UserField09() As String
            Get
                Return _UserField09
            End Get
            Set(ByVal value As String)
                _UserField09 = value
            End Set
        End Property
        Public Property UserField10() As String
            Get
                Return _UserField10
            End Get
            Set(ByVal value As String)
                _UserField10 = value
            End Set
        End Property
        Public Property OrderItems() As List(Of OrderItemInfo)
            Get
                Return _OrderItems
            End Get
            Set(ByVal value As List(Of OrderItemInfo))
                _OrderItems = value
            End Set
        End Property
        Public Property DeliveryAddress1() As String
            Get
                Return _DeliveryAddress1
            End Get
            Set(ByVal value As String)
                _DeliveryAddress1 = value
            End Set
        End Property
        Public Property DeliveryAddress2() As String
            Get
                Return _DeliveryAddress2
            End Get
            Set(ByVal value As String)
                _DeliveryAddress2 = value
            End Set
        End Property
        Public Property DeliveryAddress3() As String
            Get
                Return _DeliveryAddress3
            End Get
            Set(ByVal value As String)
                _DeliveryAddress3 = value
            End Set
        End Property
        Public Property DeliveryPostCode() As String
            Get
                Return _DeliveryPostCode
            End Get
            Set(ByVal value As String)
                _DeliveryPostCode = value
            End Set
        End Property
        Public Property PostedToERP() As Boolean
            Get
                Return _PostedToERP
            End Get
            Set(ByVal value As Boolean)
                _PostedToERP = value
            End Set
        End Property
        Public Property ERPOrderNumber() As String
            Get
                Return _ERPOrderNumber
            End Get
            Set(ByVal value As String)
                _ERPOrderNumber = value
            End Set
        End Property

        Public Property ERPStatus() As String
            Get
                Return _ERPStatus
            End Get
            Set(ByVal value As String)
                _ERPStatus = value
            End Set
        End Property
    End Class
End Namespace