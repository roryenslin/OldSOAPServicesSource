﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response
'http://products.rapidtrade.com/")> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Products
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Products))
    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
        Public Function Modify(ByVal lstProductInfo As List(Of ProductInfo)) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Dim trnTransaction As SqlTransaction = Nothing
        Dim objProductInfo As New ProductInfo
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction

            For Each objProductInfo In lstProductInfo
                Dim cmdCommand As New SqlCommand("usp_product_modify", conConnection)
                cmdCommand.Transaction = trnTransaction

                cmdCommand.Parameters.AddWithValue("@SupplierID", objProductInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@ProductID", objProductInfo.ProductID)
                cmdCommand.Parameters.AddWithValue("@Description", objProductInfo.Description)
                cmdCommand.Parameters.AddWithValue("@VAT", objProductInfo.VAT)
                cmdCommand.Parameters.AddWithValue("@Barcode", objProductInfo.Barcode)
                cmdCommand.Parameters.AddWithValue("@Unit", objProductInfo.Unit)
                cmdCommand.Parameters.AddWithValue("@CategoryName", objProductInfo.CategoryName)
                cmdCommand.Parameters.AddWithValue("@OptionalProducts", objProductInfo.OptionalProducts)
                cmdCommand.Parameters.AddWithValue("@Components", objProductInfo.Components)
                cmdCommand.Parameters.AddWithValue("@SimilarProducts", objProductInfo.SimilarProducts)
                cmdCommand.Parameters.AddWithValue("@WebSite", objProductInfo.WebSite)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint1", objProductInfo.DeliveryConstraint1)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint2", objProductInfo.DeliveryConstraint2)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint3", objProductInfo.DeliveryConstraint3)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint4", objProductInfo.DeliveryConstraint4)
                cmdCommand.Parameters.AddWithValue("@DisplayFields", objProductInfo.DisplayFields)
                cmdCommand.Parameters.AddWithValue("@UserField01", objProductInfo.UserField01)
                cmdCommand.Parameters.AddWithValue("@UserField02", objProductInfo.UserField02)
                cmdCommand.Parameters.AddWithValue("@UserField03", objProductInfo.UserField03)
                cmdCommand.Parameters.AddWithValue("@UserField04", objProductInfo.UserField04)
                cmdCommand.Parameters.AddWithValue("@UserField05", objProductInfo.UserField05)
                cmdCommand.Parameters.AddWithValue("@UserField06", objProductInfo.UserField06)
                cmdCommand.Parameters.AddWithValue("@UserField07", objProductInfo.UserField07)
                cmdCommand.Parameters.AddWithValue("@UserField08", objProductInfo.UserField08)
                cmdCommand.Parameters.AddWithValue("@UserField09", objProductInfo.UserField09)
                cmdCommand.Parameters.AddWithValue("@UserField10", objProductInfo.UserField10)
                cmdCommand.Parameters.AddWithValue("@ImageUrlLarge", objProductInfo.ImageUrlLarge)
                cmdCommand.Parameters.AddWithValue("@ImageUrlSmall", objProductInfo.ImageUrlSmall)
                cmdCommand.Parameters.AddWithValue("@Deleted", objProductInfo.Deleted)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            Next

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objProductInfo), ex)
            trnTransaction.Rollback()
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing And conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal objProductInfo As ProductInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_product_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductInfo.ProductID)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows deleted in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ProductReadListResponse
        Dim objResponse As New ProductReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objProductInfo As ProductInfo()
            Dim cmdCommand As New SqlCommand("usp_product_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objProductInfo = ReadProducts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductInfo Is Nothing AndAlso objProductInfo.GetUpperBound(0) >= 0 Then
                objResponse.Products = objProductInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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
    Public Function Sync2b(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal offset As Integer, ByVal numrows As Integer) As ProductReadListResponse
        Dim objResponse As New ProductReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objProductInfo As ProductInfo()
            Dim cmdCommand As New SqlCommand("usp_product_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objProductInfo = ReadProducts2(objDBHelper.ExecuteReader(cmdCommand), offset, numrows)
            objResponse.Status = True
            If Not objProductInfo Is Nothing AndAlso objProductInfo.GetUpperBound(0) >= 0 Then
                _Log.Debug("Returning products " & objProductInfo.Length)
                objResponse.Products = objProductInfo
            Else
                _Log.Debug("No products found")
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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
    Public Function Test(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal offset As Integer, ByVal numrows As Integer) As ProductSync3Response

        'If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")

        Dim resultarray As New Generic.List(Of String)
        Dim br As ProductSync3Response = Sync5(strSupplierID, "", intVersion, Nothing, offset, numrows)
        Return br
    End Function

    <WebMethod()> _
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstProducts As List(Of ProductInfo)) As ProductSync3Response
        Dim objResponse As New ProductSync3Response
        Dim objTempResponse As New ProductReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)
            objTempResponse = Sync2(strSupplierId, intVersion)
            If Not lstProducts Is Nothing Then
                ProcessResponse(Modify(lstProducts), objTempResponse)
            End If

            objResponse.Products = objTempResponse.Products
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Product)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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
   Public Function Sync5(ByVal strSupplierId As String, ByVal userID As String, ByVal intVersion As Integer, ByVal lstProducts As List(Of ProductInfo), ByVal offset As Integer, ByVal numrows As Integer) As ProductSync3Response
        Dim objResponse As New ProductSync3Response
        Dim objTempResponse As New ProductReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)
            objTempResponse = Sync2b(strSupplierId, intVersion, offset, numrows)
            If objTempResponse Is Nothing Then
                objResponse.Status = True
                Return objResponse
            End If
            'Dim size As Integer = 0
            'If offset + numrows < objTempResponse.Products.Length Then
            '    size = numrows - 1
            'Else
            '    size = objTempResponse.Products.Length - offset - 1
            'End If
            '_Log.Debug("Size " & size)
            'Dim tmp(size) As ProductInfo
            'System.Array.Copy(objTempResponse.Products, offset, tmp, 0, size + 1)

            objTempResponse.Products = objTempResponse.Products
            If Not lstProducts Is Nothing Then
                ProcessResponse(Modify(lstProducts), objTempResponse)
            End If

            objResponse.Products = objTempResponse.Products
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Product)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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
    Private Function ReadProducts(ByVal objReader As SqlDataReader) As ProductInfo()
        Dim objProducts As ProductInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objProducts(intCounter)
                    objProducts(intCounter) = New ProductInfo
                    With objProducts(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .Description = CheckString2(objReader("Description"))
                        .VAT = CheckDecimal(objReader("VAT"))
                        .Barcode = CheckString2(objReader("Barcode"))
                        .Unit = CheckString2(objReader("Unit"))
                        .CategoryName = CheckString2(objReader("CategoryName"))
                        .OptionalProducts = CheckString2(objReader("OptionalProducts"))
                        .Components = CheckString2(objReader("Components"))
                        .SimilarProducts = CheckString2(objReader("SimilarProducts"))
                        .WebSite = CheckString2(objReader("WebSite"))
                        .DeliveryConstraint1 = CheckString2(objReader("DeliveryConstraint1"))
                        .DeliveryConstraint2 = CheckString2(objReader("DeliveryConstraint2"))
                        .DeliveryConstraint3 = CheckString2(objReader("DeliveryConstraint3"))
                        .DeliveryConstraint4 = CheckString2(objReader("DeliveryConstraint4"))
                        .DisplayFields = CheckString2(objReader("DisplayFields"))
                        .UserField01 = CheckString2(objReader("UserField01"))
                        .UserField02 = CheckString2(objReader("UserField02"))
                        .UserField03 = CheckString2(objReader("UserField03"))
                        .UserField04 = CheckString2(objReader("UserField04"))
                        .UserField05 = CheckString2(objReader("UserField05"))
                        .UserField06 = CheckString2(objReader("UserField06"))
                        .UserField07 = CheckString2(objReader("UserField07"))
                        .UserField08 = CheckString2(objReader("UserField08"))
                        .UserField09 = CheckString2(objReader("UserField09"))
                        .UserField10 = CheckString2(objReader("UserField10"))
                        .ImageUrlLarge = CheckString2(objReader("ImageUrlLarge"))
                        .ImageUrlSmall = CheckString2(objReader("ImageUrlSmall"))
                        .Deleted = CheckBoolean(objReader("Deleted"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objProducts
    End Function

    Private Function ReadProducts2(ByVal objReader As SqlDataReader, ByVal offset As Integer, ByVal numrows As Integer) As ProductInfo()
        Dim objProducts As ProductInfo() = Nothing
        Dim intCounter As Integer = 0
        Dim rowCounter As Integer = 0

        Try
            _Log.Info("Entered----------->")
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                If _Log.IsDebugEnabled Then _Log.Debug("HasRows " & objReader.HasRows)
                If _Log.IsDebugEnabled Then _Log.Debug("offset " & offset & " numrows " & numrows)
                While (objReader.Read())
                    Try
                        If rowCounter >= offset And rowCounter < numrows + offset Then
                            If intCounter = 0 And _Log.IsDebugEnabled Then _Log.Debug("In Once " & intCounter)
                            ReDim Preserve objProducts(intCounter)
                            objProducts(intCounter) = New ProductInfo
                            With objProducts(intCounter)
                                .SupplierID = CheckString(objReader("SupplierID"))
                                .ProductID = CheckString(objReader("ProductID"))
                                .Description = CheckString2(objReader("Description"))
                                .VAT = CheckDecimal(objReader("VAT"))
                                .Barcode = CheckString2(objReader("Barcode"))
                                .Unit = CheckString2(objReader("Unit"))
                                .CategoryName = CheckString2(objReader("CategoryName"))
                                .OptionalProducts = CheckString2(objReader("OptionalProducts"))
                                .Components = CheckString2(objReader("Components"))
                                .SimilarProducts = CheckString2(objReader("SimilarProducts"))
                                .WebSite = CheckString2(objReader("WebSite"))
                                .DeliveryConstraint1 = CheckString2(objReader("DeliveryConstraint1"))
                                .DeliveryConstraint2 = CheckString2(objReader("DeliveryConstraint2"))
                                .DeliveryConstraint3 = CheckString2(objReader("DeliveryConstraint3"))
                                .DeliveryConstraint4 = CheckString2(objReader("DeliveryConstraint4"))
                                .DisplayFields = CheckString2(objReader("DisplayFields"))
                                .UserField01 = CheckString2(objReader("UserField01"))
                                .UserField02 = CheckString2(objReader("UserField02"))
                                .UserField03 = CheckString2(objReader("UserField03"))
                                .UserField04 = CheckString2(objReader("UserField04"))
                                .UserField05 = CheckString2(objReader("UserField05"))
                                .UserField06 = CheckString2(objReader("UserField06"))
                                .UserField07 = CheckString2(objReader("UserField07"))
                                .UserField08 = CheckString2(objReader("UserField08"))
                                .UserField09 = CheckString2(objReader("UserField09"))
                                .UserField10 = CheckString2(objReader("UserField10"))
                                .ImageUrlLarge = CheckString2(objReader("ImageUrlLarge"))
                                .ImageUrlSmall = CheckString2(objReader("ImageUrlSmall"))
                                .Deleted = CheckBoolean(objReader("Deleted"))
                                .Deleted = CheckDeletedField(objReader)
                            End With
                            intCounter = intCounter + 1
                        End If
                        rowCounter = rowCounter + 1
                    Catch ex As Exception
                        _Log.Error("Error in product " & intCounter, ex)
                        Throw New Exception(ex.Message)
                    End Try
                End While
                If _Log.IsDebugEnabled Then _Log.Debug("size " & objProducts.Length)
            End If
        Finally
            objReader.Close()
        End Try
        Return objProducts
    End Function
End Class