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
Public Class Accounts
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Accounts))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objAccountInfo As AccountInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(objAccountInfo.ToString())
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_account_add")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objAccountInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objAccountInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objAccountInfo.Branch)
            cmdCommand.Parameters.AddWithValue("@Name", objAccountInfo.Name)
            cmdCommand.Parameters.AddWithValue("@VAT", objAccountInfo.VAT)
            cmdCommand.Parameters.AddWithValue("@Pricelist", objAccountInfo.PriceList)
            cmdCommand.Parameters.AddWithValue("@AccountGroup", objAccountInfo.AccountGroup)
            cmdCommand.Parameters.AddWithValue("@AccountType", objAccountInfo.AccountType)
            If Not objAccountInfo.UserFields Is Nothing AndAlso objAccountInfo.UserFields.Length > 0 Then
                Dim intCounter As Integer
                For intCounter = 0 To objAccountInfo.UserFields.GetUpperBound(0)
                    cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objAccountInfo.UserFields(intCounter))
                Next
            End If

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString() & " insert returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows inserted in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString(), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Change(ByVal objAccountInfo As AccountInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(objAccountInfo.ToString())
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_account_change")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objAccountInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objAccountInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objAccountInfo.Branch)
            cmdCommand.Parameters.AddWithValue("@Name", objAccountInfo.Name)
            cmdCommand.Parameters.AddWithValue("@VAT", objAccountInfo.VAT)
            cmdCommand.Parameters.AddWithValue("@Pricelist", objAccountInfo.PriceList)
            cmdCommand.Parameters.AddWithValue("@AccountGroup", objAccountInfo.AccountGroup)
            cmdCommand.Parameters.AddWithValue("@AccountType", objAccountInfo.AccountType)
            If Not objAccountInfo.UserFields Is Nothing AndAlso objAccountInfo.UserFields.Length > 0 Then
                Dim intCounter As Integer
                For intCounter = 0 To objAccountInfo.UserFields.GetUpperBound(0)
                    cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objAccountInfo.UserFields(intCounter))
                Next
            End If

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString() & " update returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows updated in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString(), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")

        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Modify(ByVal objAccountInfo As AccountInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(objAccountInfo.ToString())
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_account_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objAccountInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objAccountInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objAccountInfo.Branch)
            cmdCommand.Parameters.AddWithValue("@Name", objAccountInfo.Name)
            cmdCommand.Parameters.AddWithValue("@VAT", objAccountInfo.VAT)
            cmdCommand.Parameters.AddWithValue("@Pricelist", objAccountInfo.PriceList)
            cmdCommand.Parameters.AddWithValue("@AccountGroup", objAccountInfo.AccountGroup)
            cmdCommand.Parameters.AddWithValue("@AccountType", objAccountInfo.AccountType)
            If Not objAccountInfo.UserFields Is Nothing AndAlso objAccountInfo.UserFields.Length > 0 Then
                Dim intCounter As Integer
                For intCounter = 0 To objAccountInfo.UserFields.GetUpperBound(0)
                    cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objAccountInfo.UserFields(intCounter))
                Next
            End If

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString() & " modify returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString(), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal objAccountInfo As AccountInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(objAccountInfo.ToString())
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_account_delete")
            cmdCommand.Parameters.AddWithValue("@AccountID", objAccountInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objAccountInfo.Branch)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString() & " delete returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows deleted in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objAccountInfo.ToString(), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strAccountId As String) As AccountReadSingleResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New AccountReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("SupplierID: " & strSupplierId & " // AccountID: " & strAccountId)
            Dim cmdCommand As New SqlCommand("usp_account_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            Dim objAccounts As AccountInfo() = Nothing
            objAccounts = ReadAccounts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objAccounts Is Nothing AndAlso objAccounts.GetUpperBound(0) >= 0 Then
                objResponse.Account = objAccounts(0)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for SupplierID: " & strSupplierId & " // AccountID: " & strAccountId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function ReadList(ByVal strSupplierId As String) As AccountReadListResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New AccountReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("SupplierID: " & strSupplierId)
            Dim objAccountInfo As AccountInfo()
            Dim cmdCommand As New SqlCommand("usp_account_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            objAccountInfo = ReadAccounts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objAccountInfo Is Nothing AndAlso objAccountInfo.GetUpperBound(0) >= 0 Then
                objResponse.Accounts = objAccountInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for SupplierID: " & strSupplierId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync2(ByVal strUserId As String, ByVal intVersion As Integer) As AccountReadListResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New AccountReadListResponse
        Try

            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strUserId & " // Version: " & intVersion)
            Dim objAccountInfo As AccountInfo()
            Dim cmdCommand As New SqlCommand("usp_account_sync2")
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)

            objAccountInfo = ReadAccounts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objAccountInfo Is Nothing AndAlso objAccountInfo.GetUpperBound(0) >= 0 Then
                objResponse.Accounts = objAccountInfo
            Else
                objResponse.Accounts = New AccountInfo() {}
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for UserID: " & strUserId & " // Version: " & intVersion, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync3(ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstAccounts As List(Of AccountInfo)) As AccountSync3Response
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objTempResponse As New AccountReadListResponse
        Dim objResponse As New AccountSync3Response
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("UserID: " & strUserId & " // Version: " & intVersion)
            If _Log.IsDebugEnabled And lstAccounts IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstAccounts))

            objTempResponse = Sync2(strUserId, intVersion)

            If Not lstAccounts Is Nothing Then
                For Each objAccount As AccountInfo In lstAccounts
                    If Not objAccount Is Nothing Then
                        ProcessResponse(Modify(objAccount), objTempResponse)
                    End If
                Next
            End If

            objResponse.Accounts = objTempResponse.Accounts
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Account)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for UserID: " & strUserId & " // Version: " & intVersion, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    Private Function ReadAccounts(ByVal objReader As SqlDataReader) As AccountInfo()
        Dim objAccounts As AccountInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objAccounts(intCounter)
                    objAccounts(intCounter) = New AccountInfo
                    With objAccounts(intCounter)
                        If IsNumeric(objReader("VAT")) Then
                            .VAT = CType(objReader("VAT"), Integer)
                        End If
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .Branch = CheckString(objReader("BranchID"))
                        .Name = CheckString(objReader("Name"))
                        .PriceList = CheckString(objReader("Pricelist"))
                        .AccountGroup = CheckString(objReader("AccountGroup"))
                        ReDim .UserFields(9)
                        .UserFields(0) = CheckString(objReader("Userfield01"))
                        .UserFields(1) = CheckString(objReader("Userfield02"))
                        .UserFields(2) = CheckString(objReader("Userfield03"))
                        .UserFields(3) = CheckString(objReader("Userfield04"))
                        .UserFields(4) = CheckString(objReader("Userfield05"))
                        .UserFields(5) = CheckString(objReader("Userfield06"))
                        .UserFields(6) = CheckString(objReader("Userfield07"))
                        .UserFields(7) = CheckString(objReader("Userfield08"))
                        .UserFields(8) = CheckString(objReader("Userfield09"))
                        .UserFields(9) = CheckString(objReader("Userfield10"))
                        .AccountType = CheckString(objReader("AccountType"))

                        .Deleted = CheckDeletedField(objReader)

                    End With
                    If _Log.IsInfoEnabled Then _Log.Info("read from DB " & objAccounts(intCounter).ToString())

                    intCounter = intCounter + 1
                End While
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception", ex)
        Finally
            objReader.Close()
        End Try
        Return objAccounts
    End Function
End Class