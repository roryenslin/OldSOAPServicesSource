Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PriceLists
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(PriceLists))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function ReadList(ByVal strSupplierId As String, ByVal strAccountId As String, _
                    ByVal strSearchString As String, ByVal strCategory As String, _
                    ByVal iOffset As Integer, ByVal iNoRows As Integer, ByVal bMyRange As Boolean) As PriceListReadListResponse

        Return ReadListInclCatalogues(strSupplierId, strAccountId, strSearchString, strCategory, iOffset, iNoRows, bMyRange, True)

    End Function

    <WebMethod()> _
    Public Function ReadListInclCatalogues(ByVal strSupplierId As String, ByVal strAccountId As String, _
                    ByVal strSearchString As String, ByVal strCategory As String, _
                    ByVal iOffset As Integer, ByVal iNoRows As Integer, ByVal bMyRange As Boolean, _
                    ByVal bIncludeCatalogues As Boolean) As PriceListReadListResponse
        Dim objResponse As New PriceListReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objPriceListInfo As PriceListInfo()
            Dim cmdCommand As New SqlCommand("usp_pricelist_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@SearchString", strSearchString)
            cmdCommand.Parameters.AddWithValue("@Category", strCategory)
            cmdCommand.Parameters.AddWithValue("@Offset", iOffset)
            cmdCommand.Parameters.AddWithValue("@NoRows", iNoRows)
            cmdCommand.Parameters.AddWithValue("@MyRange", bMyRange)
            cmdCommand.Parameters.AddWithValue("@IncludeCatalogues", bIncludeCatalogues)

            objPriceListInfo = ReadPriceLists(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objPriceListInfo Is Nothing AndAlso objPriceListInfo.GetUpperBound(0) >= 0 Then
                objResponse.PriceLists = objPriceListInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strAccountId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function ReadList2(ByVal strSupplierId As String, ByVal strAccountId As String, _
                ByVal strSearchString As String, ByVal strCategory As String, _
                ByVal iOffset As Integer, ByVal iNoRows As Integer, ByVal bMyRange As Boolean, _
                ByVal bIncludeCatalogues As Boolean) As PriceListReadListResponse2
        Dim objResponse As New PriceListReadListResponse2
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objPriceListInfo As PriceListInfo2()
            Dim cmdCommand As New SqlCommand("usp_pricelist_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@SearchString", strSearchString)
            cmdCommand.Parameters.AddWithValue("@Category", strCategory)
            cmdCommand.Parameters.AddWithValue("@Offset", iOffset)
            cmdCommand.Parameters.AddWithValue("@NoRows", iNoRows)
            cmdCommand.Parameters.AddWithValue("@MyRange", bMyRange)
            cmdCommand.Parameters.AddWithValue("@IncludeCatalogues", bIncludeCatalogues)

            objPriceListInfo = ReadPriceLists2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objPriceListInfo Is Nothing AndAlso objPriceListInfo.GetUpperBound(0) >= 0 Then
                objResponse.PriceLists = objPriceListInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strAccountId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse
    End Function

    Private Function ReadPriceLists(ByVal objReader As SqlDataReader) As PriceListInfo()
        Dim objPriceLists As PriceListInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objPriceLists(intCounter)
                    objPriceLists(intCounter) = New PriceListInfo2()
                    With objPriceLists(intCounter)
                        .ProductID = CheckString(objReader("ProductID"))
                        .Description = CheckString(objReader("Description"))
                        .VAT = CheckDecimal(objReader("VAT"))
                        .Barcode = CheckString(objReader("Barcode"))
                        .Unit = CheckString(objReader("Unit"))
                        .CategoryName = CheckString(objReader("CategoryName"))
                        .OptionalProducts = CheckString(objReader("OptionalProducts"))
                        .Components = CheckString(objReader("Components"))
                        .SimilarProducts = CheckString(objReader("SimilarProducts"))
                        .DeliveryConstraint1 = CheckString(objReader("DeliveryConstraint1"))
                        .DeliveryConstraint2 = CheckString(objReader("DeliveryConstraint2"))
                        .DeliveryConstraint3 = CheckString(objReader("DeliveryConstraint3"))
                        .DeliveryConstraint4 = CheckString(objReader("DeliveryConstraint4"))
                        .DisplayFields = CheckString(objReader("DisplayFields"))
                        .UserField01 = CheckString(objReader("UserField01"))
                        .UserField02 = CheckString(objReader("UserField02"))
                        .UserField03 = CheckString(objReader("UserField03"))
                        .UserField04 = CheckString(objReader("UserField04"))
                        .UserField05 = CheckString(objReader("UserField05"))
                        .UserField06 = CheckString(objReader("UserField06"))
                        .UserField07 = CheckString(objReader("UserField07"))
                        .UserField08 = CheckString(objReader("UserField08"))
                        .UserField09 = CheckString(objReader("UserField09"))
                        .UserField10 = CheckString(objReader("UserField10"))
                        .Nett = CheckDouble(objReader("Nett"))
                        .Gross = CheckDouble(objReader("Gross"))
                        .Discount = CheckDouble(objReader("Discount"))
                        .Cost = CheckDecimal(objReader("Cost"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objPriceLists
    End Function
    Private Function ReadPriceLists2(ByVal objReader As SqlDataReader) As PriceListInfo2()
        Dim objPriceLists As PriceListInfo2() = Nothing
        Dim intCounter As Integer = 0

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objPriceLists(intCounter)
                    objPriceLists(intCounter) = New PriceListInfo2()
                    With objPriceLists(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .Description = CheckString(objReader("Description"))
                        .VAT = CheckDecimal(objReader("VAT"))
                        .Barcode = CheckString(objReader("Barcode"))
                        .Unit = CheckString(objReader("Unit"))
                        .CategoryName = CheckString(objReader("CategoryName"))
                        .OptionalProducts = CheckString(objReader("OptionalProducts"))
                        .Components = CheckString(objReader("Components"))
                        .SimilarProducts = CheckString(objReader("SimilarProducts"))
                        .DeliveryConstraint1 = CheckString(objReader("DeliveryConstraint1"))
                        .DeliveryConstraint2 = CheckString(objReader("DeliveryConstraint2"))
                        .DeliveryConstraint3 = CheckString(objReader("DeliveryConstraint3"))
                        .DeliveryConstraint4 = CheckString(objReader("DeliveryConstraint4"))
                        .DisplayFields = CheckString(objReader("DisplayFields"))
                        .UserField01 = CheckString(objReader("UserField01"))
                        .UserField02 = CheckString(objReader("UserField02"))
                        .UserField03 = CheckString(objReader("UserField03"))
                        .UserField04 = CheckString(objReader("UserField04"))
                        .UserField05 = CheckString(objReader("UserField05"))
                        .UserField06 = CheckString(objReader("UserField06"))
                        .UserField07 = CheckString(objReader("UserField07"))
                        .UserField08 = CheckString(objReader("UserField08"))
                        .UserField09 = CheckString(objReader("UserField09"))
                        .UserField10 = CheckString(objReader("UserField10"))
                        .Nett = CheckDouble(objReader("Nett"))
                        .Gross = CheckDouble(objReader("Gross"))
                        .Discount = CheckDouble(objReader("Discount"))
                        .Cost = CheckDecimal(objReader("Cost"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objPriceLists
    End Function

End Class