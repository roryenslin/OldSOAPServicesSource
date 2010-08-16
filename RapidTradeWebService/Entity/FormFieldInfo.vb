Public Class FormFieldInfo
    Private _SupplierID As String
    Private _FormTypeID As Integer
    Private _FormFieldID As Integer
    Private _FormGroup As String
    Private _SortOrder As Integer
    Private _Label As String
    Private _DefaultData As String
    Private _DefaultSQL As String
    Private _FieldType As Integer
    Private _Deleted As Boolean
    Public Sub New()
    End Sub
    Public Sub New(ByVal strSuppId As String, ByVal iFormType As Integer, ByVal iFormField As Integer, _
                   ByVal strFormgrp As String, ByVal iSort As Integer, ByVal strLabel As String, ByVal strDefault As String, _
                   ByVal strSQL As String, ByVal iField As Integer)
        _SupplierID = strSuppId
        _FormTypeID = iFormType
        _FormFieldID = iFormField
        _SortOrder = iSort
        _Label = strLabel
        _DefaultData = strDefault
        _DefaultSQL = strSQL
        _FieldType = iField
    End Sub
    Public Property SupplierID() As String
        Get
            Return _SupplierID
        End Get
        Set(ByVal value As String)
            _SupplierID = value
        End Set
    End Property
    Public Property FormTypeID() As Integer
        Get
            Return _FormTypeID
        End Get
        Set(ByVal value As Integer)
            _FormTypeID = value
        End Set
    End Property
    Public Property FormFieldID() As Integer
        Get
            Return _FormFieldID
        End Get
        Set(ByVal value As Integer)
            _FormFieldID = value
        End Set
    End Property
    Public Property FormGroup() As String
        Get
            Return _FormGroup
        End Get
        Set(ByVal value As String)
            _FormGroup = value
        End Set
    End Property
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
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
    Public Property DefaultData() As String
        Get
            Return _DefaultData
        End Get
        Set(ByVal value As String)
            _DefaultData = value
        End Set
    End Property
    Public Property DefaultSQL() As String
        Get
            Return _DefaultSQL
        End Get
        Set(ByVal value As String)
            _DefaultSQL = value
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
End Class