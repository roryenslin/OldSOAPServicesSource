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
Public Class Contacts
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Contacts))
    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objContactInfo As ContactInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_contacts_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objContactInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objContactInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Counter", objContactInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@Name", objContactInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Position", objContactInfo.Position)
            cmdCommand.Parameters.AddWithValue("@Tel", objContactInfo.Tel)
            cmdCommand.Parameters.AddWithValue("@Mobile", objContactInfo.Mobile)
            cmdCommand.Parameters.AddWithValue("@Email", objContactInfo.Email)
            cmdCommand.Parameters.AddWithValue("@UserField1", objContactInfo.UserField1)
            cmdCommand.Parameters.AddWithValue("@UserField2", objContactInfo.UserField2)
            cmdCommand.Parameters.AddWithValue("@UserField3", objContactInfo.UserField3)
            cmdCommand.Parameters.AddWithValue("@UserField4", objContactInfo.UserField4)
            cmdCommand.Parameters.AddWithValue("@UserField5", objContactInfo.UserField5)

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objContactInfo), ex)
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
    Public Function Delete(ByVal objContactInfo As ContactInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_contacts_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objContactInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objContactInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Counter", objContactInfo.Counter)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strAccountId As String, ByVal iCounter As Integer) As ContactReadSingleResponse
        Dim objResponse As New ContactReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_contacts_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@Counter", iCounter)

            Dim objContacts As ContactInfo() = Nothing
            objContacts = ReadContacts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objContacts Is Nothing AndAlso objContacts.GetUpperBound(0) >= 0 Then
                objResponse.Contact = objContacts(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As ContactReadListResponse
        Dim objResponse As New ContactReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objContactInfo As ContactInfo()
            Dim cmdCommand As New SqlCommand("usp_contacts_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objContactInfo = ReadContacts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objContactInfo Is Nothing AndAlso objContactInfo.GetUpperBound(0) >= 0 Then
                objResponse.Contacts = objContactInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ContactReadListResponse
        Dim objResponse As New ContactReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objContactInfo As ContactInfo()
            Dim cmdCommand As New SqlCommand("usp_contacts_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objContactInfo = ReadContacts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objContactInfo Is Nothing AndAlso objContactInfo.GetUpperBound(0) >= 0 Then
                objResponse.Contacts = objContactInfo
            Else
                objResponse.Contacts = New ContactInfo() {}
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstContacts As List(Of ContactInfo)) As ContactSync3Response
        Dim objResponse As New ContactSync3Response
        Dim objTempResponse As New ContactReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strUserId & " // Version: " & intVersion)
            If _Log.IsDebugEnabled And lstContacts IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstContacts))

            objTempResponse = Sync2(strSupplierId, strUserId, intVersion)

            If Not lstContacts Is Nothing Then
                For Each objContact As ContactInfo In lstContacts
                    If Not objContact Is Nothing Then
                        ProcessResponse(Modify(objContact), objTempResponse)
                    End If
                Next
            End If

            objResponse.Contacts = objTempResponse.Contacts
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Contacts)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
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
        Return objResponse
    End Function

    Private Function ReadContacts(ByVal objReader As SqlDataReader) As ContactInfo()
        Dim objContacts As ContactInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objContacts(intCounter)
                    objContacts(intCounter) = New ContactInfo
                    With objContacts(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .Counter = CheckInteger(objReader("Counter"))
                        .Name = CheckString(objReader("Name"))
                        .Position = CheckString(objReader("Position"))
                        .Tel = CheckString(objReader("Tel"))
                        .Mobile = CheckString(objReader("Mobile"))
                        .Email = CheckString(objReader("Email"))
                        .UserField1 = CheckString(objReader("UserField1"))
                        .UserField2 = CheckString(objReader("UserField2"))
                        .UserField3 = CheckString(objReader("UserField3"))
                        .UserField4 = CheckString(objReader("UserField4"))
                        .UserField5 = CheckString(objReader("UserField5"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objContacts
    End Function

End Class