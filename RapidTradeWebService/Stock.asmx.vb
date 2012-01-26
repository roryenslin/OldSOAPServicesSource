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
Public Class Stock
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Stock))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
        Public Function Modify(ByVal objStockInfo As StockInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter

            Dim cmdCommand As New SqlCommand("usp_Stock_modify")

            cmdCommand.Parameters.AddWithValue("@SupplierID", objStockInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objStockInfo.ProductID)
            cmdCommand.Parameters.AddWithValue("@Warehouse", objStockInfo.Warehouse)
            cmdCommand.Parameters.AddWithValue("@Stock", objStockInfo.Stock)
            'cmdCommand.Parameters.AddWithValue("@Deleted", objStockInfo.Deleted)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            objResponse.Status = True

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objStockInfo), ex)
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
        Public Function ModifyAll(ByVal list As List(Of StockInfo)) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Dim trnTransaction As SqlTransaction = Nothing
        If _Log.IsInfoEnabled Then _Log.Info("ModifyAll Entered to update ----------->")
        If list Is Nothing Then Return objResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Update count " & list.Count)


            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            If conConnection.State <> ConnectionState.Open Then conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            For Each objStockInfo In list
                Dim cmdCommand As New SqlCommand("usp_Stock_modify", conConnection, trnTransaction)
                cmdCommand.Transaction = trnTransaction

                cmdCommand.Parameters.AddWithValue("@SupplierID", objStockInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@ProductID", objStockInfo.ProductID)
                cmdCommand.Parameters.AddWithValue("@Warehouse", objStockInfo.Warehouse)
                cmdCommand.Parameters.AddWithValue("@Stock", objStockInfo.Stock)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            Next
            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error in stock modifyall", ex)
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
        Public Function ReadList(ByVal strSupplierId As String, ByVal strProductID As String) As StockReadListResponse
        Dim objResponse As New StockReadListResponse
        Dim cmdCommand As New SqlCommand("usp_Stock_readlist")
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objStockInfo As StockInfo()

            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ProductID", strProductID)

            objStockInfo = ReadStock(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objStockInfo Is Nothing AndAlso objStockInfo.GetUpperBound(0) >= 0 Then
                objResponse.Stock = objStockInfo
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
        Finally
            Try
                cmdCommand.Connection.Close()
            Catch ex As Exception
            End Try

        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As StockReadListResponse
        Dim objResponse As New StockReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objStockInfo As StockInfo()
            Dim cmdCommand As New SqlCommand("usp_Stock_sync4")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objStockInfo = ReadStock(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objStockInfo Is Nothing AndAlso objStockInfo.GetUpperBound(0) >= 0 Then
                objResponse.Stock = objStockInfo
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
    Public Function Test(ByVal strSupplierID As String, ByVal strProductID As String, ByVal strUserId As String, ByVal intVersion As Integer) As StockSync4Response
        Dim resultarray As New Generic.List(Of String)
        Dim lstStock As New List(Of StockInfo)
        lstStock.Add(New StockInfo(strSupplierID, strProductID, "A", 10, False))
        Dim br As StockSync4Response = Sync4(strSupplierID, "", intVersion, lstStock)
        Return br
    End Function

    <WebMethod()> _
    Public Function Sync4(ByVal strSupplierId As String, ByVal userID As String, ByVal intVersion As Integer, ByVal lstStock As List(Of StockInfo)) As StockSync4Response
        Dim objResponse As New StockSync4Response
        Dim objTempResponse As New StockReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)
            'objTempResponse = Sync2(strSupplierId, intVersion)

            ProcessResponse(ModifyAll(lstStock), objTempResponse)
            If Not objTempResponse.Errors Is Nothing Then objTempResponse.Status = False
            objResponse.Stock = objTempResponse.Stock
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            'Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Stock)
            'If objTableVersionResponse.Status Then
            'objResponse.LastVersion = objTableVersionResponse.TableVersion
            'Else
            'ProcessResponse(objTableVersionResponse, objResponse)
            'End If
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

    Private Function ReadStock(ByVal objReader As SqlDataReader) As StockInfo()
        Dim objStock As StockInfo() = Nothing
        Dim intCounter As Integer = 0

        Try

            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objStock(intCounter)
                    objStock(intCounter) = New StockInfo
                    With objStock(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .Warehouse = CheckString(objReader("Warehouse"))
                        .Stock = CheckInteger(objReader("Stock"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()

        End Try
        Return objStock
    End Function

End Class