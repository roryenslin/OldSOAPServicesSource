Imports System.Xml

Namespace Entity

    <Serializable()> _
    Public Class CompanyInfo
        Private _SupplierID As String
        Private _CompanyID As String
        Private _Branch As String
        Private _Name As String
        Private _VAT As Integer
        Private _PriceList As String
        Private _CompanyGroup As String
        Private _UserFields As String()
        Private _CompanyType As String
        Private _SharedCompany As Boolean
        Private _Deleted As Boolean

        Public Sub New()

        End Sub

        Public Sub New(ByVal strSupplierId As String, ByVal strAcctId As String, ByVal strBranch As String, ByVal strName As String, _
               ByVal intVAT As Integer, ByVal strPriceList As String, ByVal strCompanyGroup As String, ByVal saUserFields As String(), _
                ByVal strAcctType As String, ByVal bSharedCompany As Boolean, ByVal bDeleted As Boolean)
            _SupplierID = strSupplierId
            _CompanyID = strAcctId
            _Branch = strBranch
            _Name = strName
            _VAT = intVAT
            _PriceList = strPriceList
            _CompanyGroup = strCompanyGroup
            _CompanyType = strAcctType
            _Deleted = bDeleted
            _SharedCompany = bSharedCompany
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
        Public Property CompanyID() As String
            Get
                Return _CompanyID
            End Get
            Set(ByVal value As String)
                _CompanyID = value
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
        Public Property CompanyGroup() As String
            Get
                Return _CompanyGroup
            End Get
            Set(ByVal value As String)
                _CompanyGroup = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property CompanyType() As String
            Get
                Return _CompanyType
            End Get
            Set(ByVal value As String)
                _CompanyType = value
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
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property SharedCompany() As Boolean
            Get
                Return _SharedCompany
            End Get
            Set(ByVal value As Boolean)
                _SharedCompany = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Dim result As New StringBuilder()
            result.AppendFormat("CompanyInfo( SupplierID: {0} // CompanyID: {1} // BranchID: {2} // Name: {3} // VAT: {4} // Pricelist: {5} // CompanyGroup: {6} // CompanyType: {7} // Deleted: {8}", SupplierID, CompanyID, Branch, Name, VAT, PriceList, CompanyGroup, CompanyType, Deleted)
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
