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
Public Class Companies
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Companies))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objCompanyInfo As CompanyInfo) As BaseResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug(objCompanyInfo.ToString())
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_account_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objCompanyInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objCompanyInfo.CompanyID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objCompanyInfo.Branch)
            cmdCommand.Parameters.AddWithValue("@Name", objCompanyInfo.Name)
            cmdCommand.Parameters.AddWithValue("@VAT", objCompanyInfo.VAT)
            cmdCommand.Parameters.AddWithValue("@Pricelist", objCompanyInfo.PriceList)
            cmdCommand.Parameters.AddWithValue("@AccountGroup", objCompanyInfo.CompanyGroup)
            cmdCommand.Parameters.AddWithValue("@AccountType", objCompanyInfo.CompanyType)
            cmdCommand.Parameters.AddWithValue("@SharedCompany", objCompanyInfo.SharedCompany)
            cmdCommand.Parameters.AddWithValue("@Deleted", objCompanyInfo.Deleted)
            If Not objCompanyInfo.UserFields Is Nothing AndAlso objCompanyInfo.UserFields.Length > 0 Then
                Dim intCounter As Integer
                For intCounter = 0 To objCompanyInfo.UserFields.GetUpperBound(0)
                    cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objCompanyInfo.UserFields(intCounter))
                Next
            End If

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objCompanyInfo.ToString() & " modify returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objCompanyInfo.ToString(), ex)
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
    Public Function Delete(ByVal objCompanyInfo As CompanyInfo) As BaseResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(objCompanyInfo.ToString())
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_account_delete")
            cmdCommand.Parameters.AddWithValue("@AccountID", objCompanyInfo.CompanyID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objCompanyInfo.Branch)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objCompanyInfo.ToString() & " delete returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows deleted in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & objCompanyInfo.ToString(), ex)
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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strCompanyId As String) As CompanyReadSingleResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New CompanyReadSingleResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("SupplierID: " & strSupplierId & " // CompanyID: " & strCompanyId)
            Dim cmdCommand As New SqlCommand("usp_account_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strCompanyId)
            Dim objCompanies As CompanyInfo() = Nothing
            objCompanies = ReadCompanies(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objCompanies Is Nothing AndAlso objCompanies.GetUpperBound(0) >= 0 Then
                objResponse.Company = objCompanies(0)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for SupplierID: " & strSupplierId & " // CompanyID: " & strCompanyId, ex)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As CompanyReadListResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New CompanyReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("SupplierID: " & strSupplierId)
            Dim objCompanyInfo As CompanyInfo()
            Dim cmdCommand As New SqlCommand("usp_account_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            objCompanyInfo = ReadCompanies(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objCompanyInfo Is Nothing AndAlso objCompanyInfo.GetUpperBound(0) >= 0 Then
                objResponse.Companies = objCompanyInfo
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
    Public Function Sync2(ByVal strUserId As String, ByVal intVersion As Integer) As CompanyReadListResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New CompanyReadListResponse
        Try

            If _Log.IsDebugEnabled Then _Log.Debug("UserID: " & strUserId & " // Version: " & intVersion)
            Dim objCompanyInfo As CompanyInfo()
            Dim cmdCommand As New SqlCommand("usp_account_sync2")
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)

            objCompanyInfo = ReadCompanies(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objCompanyInfo Is Nothing AndAlso objCompanyInfo.GetUpperBound(0) >= 0 Then
                objResponse.Companies = objCompanyInfo
            Else
                objResponse.Companies = New CompanyInfo() {}
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
    Public Function Sync3(ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstCompanies As List(Of CompanyInfo)) As Companiesync3Response
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->" & strUserId)
        Dim objTempResponse As New CompanyReadListResponse
        Dim objResponse As New Companiesync3Response
        Try
            If _Log.IsDebugEnabled And lstCompanies IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstCompanies))

            objTempResponse = Sync2(strUserId, intVersion)

            If Not lstCompanies Is Nothing Then
                For Each objCompany As CompanyInfo In lstCompanies
                    If Not objCompany Is Nothing Then
                        ProcessResponse(Modify(objCompany), objTempResponse)
                    End If
                Next
            End If

            objResponse.Companies = objTempResponse.Companies
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
        'If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))

        Return objResponse
    End Function

    Private Function ReadCompanies(ByVal objReader As SqlDataReader) As CompanyInfo()
        Dim objCompanies As CompanyInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objCompanies(intCounter)
                    objCompanies(intCounter) = New CompanyInfo
                    With objCompanies(intCounter)
                        If IsNumeric(objReader("VAT")) Then
                            .VAT = CType(objReader("VAT"), Integer)
                        End If
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .CompanyID = CheckString(objReader("AccountID"))
                        .Branch = CheckString(objReader("BranchID"))
                        .Name = CheckString(objReader("Name"))
                        .PriceList = CheckString(objReader("Pricelist"))
                        .CompanyGroup = CheckString(objReader("AccountGroup"))
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
                        .CompanyType = CheckString(objReader("AccountType"))

                        .SharedCompany = CheckBoolean(objReader("SharedCompany"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    If _Log.IsDebugEnabled Then _Log.Debug("read from DB " & objCompanies(intCounter).ToString())

                    intCounter = intCounter + 1
                End While
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception", ex)
        Finally
            objReader.Close()
        End Try
        Return objCompanies
    End Function
End Class