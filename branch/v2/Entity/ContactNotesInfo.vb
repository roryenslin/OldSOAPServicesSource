Namespace Entity
    Public Class ContactNotesInfo
        Private _SupplierID As String
        Private _AccountID As String
        Private _ContactID As String
        Private _NotesID As String
        Private _Notes As String
        Private _Deleted As Boolean
        
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal strSuppId As String, ByVal strAcctId As String, ByVal strContactId As String, _
                       ByVal strNotesId As String, ByVal strNotes As String)
            _SupplierID = strSuppId
            _AccountID = strAcctId
            _ContactID = strContactId
            _NotesID = strNotesId
            _Notes = strNotes
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
        Public Property ContactID() As String
            Get
                Return _ContactID
            End Get
            Set(ByVal value As String)
                _ContactID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property NotesID() As String
            Get
                Return _NotesID
            End Get
            Set(ByVal value As String)
                _NotesID = value
            End Set
        End Property
        <System.Xml.Serialization.XmlElement(IsNullable:=False)> _
        Public Property Notes() As String
            Get
                Return _Notes
            End Get
            Set(ByVal value As String)
                _Notes = value
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
    End Class
End Namespace


