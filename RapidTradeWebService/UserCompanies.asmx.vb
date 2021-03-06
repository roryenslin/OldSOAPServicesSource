﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class UserCompanies
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(UserCompanies))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objUserCompanyInfo As UserCompanyInfo) As BaseResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_userAccounts_modify")
            cmdCommand.Parameters.AddWithValue("@UserID", objUserCompanyInfo.UserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", objUserCompanyInfo.CompanyId)
            cmdCommand.Parameters.AddWithValue("@BranchID", objUserCompanyInfo.BranchId)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserCompanyInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@Deleted", objUserCompanyInfo.Deleted)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function Test(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal strCompanyID As String, ByVal strBranch As String) As UserCompanySync4Response
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim rslt As New List(Of Object)
        Dim ob As New UserCompanyInfo
        ob.SupplierID = strSupplierID
        ob.UserId = strUserId
        ob.BranchId = strBranch
        ob.CompanyId = strCompanyID
        Dim lstUserCompanys As New List(Of UserCompanyInfo)
        lstUserCompanys.Add(ob)
        Dim result As UserCompanySync4Response = Sync4(strSupplierID, strUserId, intVersion, lstUserCompanys)

        Return result
    End Function

    <WebMethod()> _
    Public Function Sync4(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstUserCompanys As List(Of UserCompanyInfo)) As UserCompanySync4Response
        Dim objResponse As New UserCompanySync4Response
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->" & strUserId)
            If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstUserCompanys))
            'Sync4 Implementation
            Dim objUserCompanyInfo As UserCompanyInfo()
            Dim cmdCommand As New SqlCommand("usp_userAccounts_sync4")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objUserCompanyInfo = ReadUserCompanys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserCompanyInfo Is Nothing AndAlso objUserCompanyInfo.GetUpperBound(0) >= 0 Then
                objResponse.UserCompanies = objUserCompanyInfo
            End If

            Dim objTempResponse As New UserCompanySync4Response
            If Not lstUserCompanys Is Nothing Then
                For Each objUserCompany As UserCompanyInfo In lstUserCompanys
                    If Not objUserCompany Is Nothing Then
                        ProcessResponse(Modify(objUserCompany), objTempResponse)
                    End If
                Next
                If objTempResponse.Errors IsNot Nothing Then
                    If objTempResponse.Errors.Length > 0 Then
                        objResponse.Errors = objTempResponse.Errors
                        objResponse.Status = objTempResponse.Status
                    End If
                End If
            End If

            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.UserAccounts)
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
        '***If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    Private Function ReadUserCompanys(ByVal objReader As SqlDataReader) As UserCompanyInfo()
        Dim objUserCompanys As UserCompanyInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objUserCompanys(intCounter)
                    objUserCompanys(intCounter) = New UserCompanyInfo
                    With objUserCompanys(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .UserId = CheckString(objReader("UserID"))
                        .BranchId = CheckString(objReader("BranchId"))
                        .CompanyId = CheckString(objReader("AccountId"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objUserCompanys
    End Function
End Class