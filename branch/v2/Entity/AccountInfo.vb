Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class AccountInfo
        Private _SupplierID As String
        Private _AccountID As String
        Private _Branch As String
        Private _Name As String
        Private _VAT As Integer
        Private _PriceList As String
        Private _AccountGroup As String
        Private _UserFields As String()
        Private _AccountType As String
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strAcctId As String, ByVal strBranch As String, ByVal strName As String, _
               ByVal intVAT As Integer, ByVal strPriceList As String, ByVal strAccountGroup As String, ByVal saUserFields As String(), _
                ByVal strAcctType As String, ByVal bDeleted As Boolean)
            _SupplierID = strSupplierId
            _AccountID = strAcctId
            _Branch = strBranch
            _Name = strName
            _VAT = intVAT
            _PriceList = strPriceList
            _AccountGroup = strAccountGroup
            _AccountType = strAcctType
            _Deleted = bDeleted
            ReDim _UserFields(9)
            If Not saUserFields Is Nothing Then
                Array.Copy(saUserFields, _UserFields, saUserFields.Length)
            End If
        End Sub

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
        Public Property AccountID() As String
            Get
                Return _AccountID
            End Get
            Set(ByVal value As String)
                _AccountID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Branch() As String
            Get
                Return _Branch
            End Get
            Set(ByVal value As String)
                _Branch = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property VAT() As Integer
            Get
                Return _VAT
            End Get
            Set(ByVal value As Integer)
                _VAT = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property PriceList() As String
            Get
                Return _PriceList
            End Get
            Set(ByVal value As String)
                _PriceList = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AccountGroup() As String
            Get
                Return _AccountGroup
            End Get
            Set(ByVal value As String)
                _AccountGroup = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property AccountType() As String
            Get
                Return _AccountType
            End Get
            Set(ByVal value As String)
                _AccountType = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property UserFields() As String()
            Get
                Return _UserFields
            End Get
            Set(ByVal value As String())
                _UserFields = value
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

        Public Overrides Function ToString() As String
            Dim result As New StringBuilder()
            result.AppendFormat("AccountInfo( SupplierID: {0} // AccountID: {1} // BranchID: {2} // Name: {3} // VAT: {4} // Pricelist: {5} // AccountGroup: {6} // AccountType: {7} // Deleted: {8}", SupplierID, AccountID, Branch, Name, VAT, PriceList, AccountGroup, AccountType, Deleted)
            If UserFields IsNot Nothing Then
                For x As Integer = 0 To UserFields.Length - 1
                    If UserFields(x) IsNot Nothing Then result.AppendFormat(" // Userfield{0}: {1}", x, UserFields(x))
                Next
            End If
            result.Append(" )")
            Return result.ToString()
        End Function

    End Class
End Namespace
