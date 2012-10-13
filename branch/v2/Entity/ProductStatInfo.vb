Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class ProductStatInfo
        Private _SupplierID As String
        Private _AccountID As String
        Private _ProductID As String
        Private _Year As String
        Private _Month As String
        Private _Day As DateTime
        Private _Value As Decimal
        Private _Value2 As Decimal
        Private _Value3 As Decimal
        Private _Value4 As Decimal
        Private _Value5 As Decimal
        Private _UserField1 As String
        Private _UserField2 As String
        Private _UserField3 As String
        Private _UserField4 As String
        Private _UserField5 As String
        Private _Deleted As Boolean

        Public Sub New()
        End Sub
        Public Sub New(ByVal strSupplierID As String, ByVal strAccountID As String, ByVal strProductID As String, ByVal strYear As String, _
                       ByVal strMonth As String, ByVal strDay As DateTime, ByVal dValue As Decimal, _
                       ByVal dValue2 As Decimal, _
                       ByVal dValue3 As Decimal, ByVal dValue4 As Decimal, ByVal dValue5 As Decimal, _
                       ByVal strUserField1 As String, ByVal strUserField2 As String, ByVal strUserField3 As String, _
                       ByVal strUserField4 As String, ByVal strUserField5 As String)
            _SupplierID = strSupplierID
            _AccountID = strAccountID
            _ProductID = strProductID
            _Year = strYear
            _Month = strMonth
            _Day = strDay
            _Value = dValue
            _Value2 = dValue2
            _Value3 = dValue3
            _Value4 = dValue4
            _Value5 = dValue5
            _UserField1 = strUserField1
            _UserField2 = strUserField2
            _UserField3 = strUserField3
            _UserField4 = strUserField4
            _UserField5 = strUserField5
        End Sub

        Public Property AccountID() As String
            Get
                Return _AccountID
            End Get
            Set(ByVal value As String)
                _AccountID = value
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
        Public Property ProductID() As String
            Get
                Return _ProductID
            End Get
            Set(ByVal value As String)
                _ProductID = value
            End Set
        End Property
        Public Property Year() As String
            Get
                Return _Year
            End Get
            Set(ByVal value As String)
                _Year = value
            End Set
        End Property
        Public Property Month() As String
            Get
                Return _Month
            End Get
            Set(ByVal value As String)
                _Month = value
            End Set
        End Property
        Public Property Day() As DateTime
            Get
                Return _Day
            End Get
            Set(ByVal value As DateTime)
                _Day = value
            End Set
        End Property
        Public Property Value() As Decimal
            Get
                Return _Value
            End Get
            Set(ByVal value As Decimal)
                _Value = value
            End Set
        End Property
        Public Property Value2() As Decimal
            Get
                Return _Value2
            End Get
            Set(ByVal value As Decimal)
                _Value2 = value
            End Set
        End Property
        
        Public Property Value3() As Decimal
            Get
                Return _Value3
            End Get
            Set(ByVal value As Decimal)
                _Value3 = value
            End Set
        End Property
        Public Property Value4() As Decimal
            Get
                Return _Value4
            End Get
            Set(ByVal value As Decimal)
                _Value4 = value
            End Set
        End Property
        Public Property Value5() As Decimal
            Get
                Return _Value5
            End Get
            Set(ByVal value As Decimal)
                _Value5 = value
            End Set
        End Property
        Public Property UserField1() As String
            Get
                Return _UserField1
            End Get
            Set(ByVal value As String)
                _UserField1 = value
            End Set
        End Property
        Public Property UserField2() As String
            Get
                Return _UserField2
            End Get
            Set(ByVal value As String)
                _UserField2 = value
            End Set
        End Property
        Public Property UserField3() As String
            Get
                Return _UserField3
            End Get
            Set(ByVal value As String)
                _UserField3 = value
            End Set
        End Property
        Public Property UserField4() As String
            Get
                Return _UserField4
            End Get
            Set(ByVal value As String)
                _UserField4 = value
            End Set
        End Property
        Public Property UserField5() As String
            Get
                Return _UserField5
            End Get
            Set(ByVal value As String)
                _UserField5 = value
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
