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
Public Class UserToID
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(UserToID))

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objUserToIDInfo As UserToIDInfo) As BaseResponse
        If _Log.IsInfoEnabled Then _Log.Info("UserToID Modify bypassed----------->")
        Dim objResponse As New BaseResponse
        Return objResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("UserToID Modify Entered----------->")
            If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objUserToIDInfo))
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_usertoid_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserToIDInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objUserToIDInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@TypeID", objUserToIDInfo.TypeID)
            cmdCommand.Parameters.AddWithValue("@ID", objUserToIDInfo.ID)
            cmdCommand.Parameters.AddWithValue("@Deleted", objUserToIDInfo.Deleted)
            '** this is log deletes as not sure when its deleting
            If _Log.IsInfoEnabled Then
                If objUserToIDInfo.Deleted Then
                    _Log.Info("Deleted:" & objUserToIDInfo.UserID & objUserToIDInfo.TypeID & objUserToIDInfo.TypeID)
                End If
            End If

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objUserToIDInfo), ex)
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
    Public Function Delete(ByVal objUserToIDInfo As UserToIDInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objUserToIDInfo))

            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_usertoid_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserToIDInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objUserToIDInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@TypeID", objUserToIDInfo.TypeID)

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objUserToIDInfo), ex)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As UserToIDReadListResponse
        Dim objResponse As New UserToIDReadListResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Try
            Dim objUserToIDInfo As UserToIDInfo()
            Dim cmdCommand As New SqlCommand("usp_usertoid_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objUserToIDInfo = ReadUserToIDs(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserToIDInfo Is Nothing AndAlso objUserToIDInfo.GetUpperBound(0) >= 0 Then
                objResponse.UserToIDs = objUserToIDInfo
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
    Public Function ReadListAll(ByVal strSupplierId As String, ByVal ID As String, ByVal TypeID As Integer) As UserToIDReadListResponse2
        Dim objResponse As New UserToIDReadListResponse2
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Try
            Dim objUserToIDInfo As UserToIDInfo2()
            Dim cmdCommand As New SqlCommand("usp_usertoid_readlistall")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@TypeID", TypeID)
            cmdCommand.Parameters.AddWithValue("@ID", ID)
            objUserToIDInfo = ReadUserToIDs2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserToIDInfo Is Nothing AndAlso objUserToIDInfo.GetUpperBound(0) >= 0 Then
                objResponse.UserToIDs = objUserToIDInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As UserToIDReadListResponse
        Dim objResponse As New UserToIDReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objUserToIDInfo As UserToIDInfo()
            Dim cmdCommand As New SqlCommand("usp_usertoid_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objUserToIDInfo = ReadUserToIDs(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserToIDInfo Is Nothing AndAlso objUserToIDInfo.GetUpperBound(0) >= 0 Then
                objResponse.UserToIDs = objUserToIDInfo
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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse), ex)
        End Try
        Return objResponse
    End Function
    <WebMethod()> _
    Public Function TEST(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal userID As String, ByVal strID As String, ByVal typeid As Integer, ByVal deleted As Boolean) As UserToIDSync3Response
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim lst As New List(Of UserToIDInfo)
        Dim info As New UserToIDInfo(strSupplierId, userID, typeid, strID)
        info.Deleted = deleted
        lst.Add(info)
        Dim br As UserToIDSync3Response = Sync3(strSupplierId, intVersion, lst)
        Return br
    End Function

    <WebMethod()> _
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstUserToIDs As List(Of UserToIDInfo)) As UserToIDSync3Response
        Dim objResponse As New UserToIDSync3Response
        Dim objTempResponse As New UserToIDReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            objTempResponse = Sync2(strSupplierId, intVersion)
            '*** no longer sent from desktop so no need to update
            'If Not lstUserToIDs Is Nothing Then
            '    If _Log.IsDebugEnabled Then _Log.Debug("Input Elements : " + lstUserToIDs.Count.ToString())

            '    '*** now update all records
            '    For Each objUserToID As UserToIDInfo In lstUserToIDs
            '        Dim obase As New BaseResponse
            '        If Not objUserToID Is Nothing Then
            '            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Request : []User ID: {0} [Supplier ID]: {1} [ID]: {2} [TypeID] : {3}", objUserToID.UserID, objUserToID.SupplierID, objUserToID.ID, objUserToID.TypeID))
            '            obase = Modify(objUserToID)
            '            ProcessResponse(obase, objTempResponse)
            '        End If
            '    Next
            'End If

            objResponse.UserToIDs = objTempResponse.UserToIDs
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.UserToID)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse), ex)
        End Try

        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    Private Function ReadUserToIDs(ByVal objReader As SqlDataReader) As UserToIDInfo()
        Dim objUserToIDs As UserToIDInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            Dim iTemp As Integer
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objUserToIDs(intCounter)
                    objUserToIDs(intCounter) = New UserToIDInfo
                    With objUserToIDs(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .UserID = CheckString(objReader("UserID"))
                        Integer.TryParse(objReader("TypeID").ToString(), iTemp)
                        .TypeID = CType(iTemp, TypeIDEnum)
                        .ID = CheckString(objReader("ID"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objUserToIDs
    End Function

    Private Function ReadUserToIDs2(ByVal objReader As SqlDataReader) As UserToIDInfo2()
        Dim objUserToIDs As UserToIDInfo2() = Nothing
        Dim intCounter As Integer = 0

        Try
            Dim iTemp As Integer
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objUserToIDs(intCounter)
                    objUserToIDs(intCounter) = New UserToIDInfo2
                    With objUserToIDs(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .UserID = CheckString(objReader("UserID"))
                        Integer.TryParse(objReader("TypeID").ToString(), iTemp)
                        .TypeID = CType(iTemp, TypeIDEnum)
                        .ID = CheckString(objReader("ID"))
                        .Deleted = CheckDeletedField(objReader)
                        .Name = CheckString(objReader("Name"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objUserToIDs
    End Function
End Class