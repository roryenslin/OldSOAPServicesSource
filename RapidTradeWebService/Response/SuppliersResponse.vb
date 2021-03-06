﻿Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class SupplierReadSingleResponse
        Inherits BaseResponse

        Private _Supplier As SupplierInfo

        Public Sub New()

        End Sub

        Public Property Supplier() As SupplierInfo
            Get
                Return _Supplier
            End Get
            Set(ByVal value As SupplierInfo)
                _Supplier = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class SupplierReadListResponse
        Inherits BaseResponse

        Private _Suppliers As SupplierInfo()

        Public Sub New()

        End Sub

        Public Property Suppliers() As SupplierInfo()
            Get
                Return _Suppliers
            End Get
            Set(ByVal value As SupplierInfo())
                _Suppliers = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class SupplierSync3Response
        Inherits SupplierReadListResponse

        Private _LastVersion As Integer

        Public Property LastVersion() As Integer
            Get
                Return _LastVersion
            End Get
            Set(ByVal value As Integer)
                _LastVersion = value
            End Set
        End Property

    End Class
End Namespace

