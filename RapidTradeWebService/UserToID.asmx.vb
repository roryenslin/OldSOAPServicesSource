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

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objUserToIDInfo As UserToIDInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_usertoid_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserToIDInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objUserToIDInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@TypeID", objUserToIDInfo.TypeID)
            cmdCommand.Parameters.AddWithValue("@ID", objUserToIDInfo.ID)

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
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstUserToIDs As List(Of UserToIDInfo)) As UserToIDSync3Response
        Dim objResponse As New UserToIDSync3Response
        Dim objTempResponse As New UserToIDReadListResponse
        Try
            'TRACE
            Dim strFile As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "UserToIDLog.txt")
            System.IO.File.AppendAllText(strFile, "Before Sync 2")
            System.IO.File.AppendAllText(strFile, Environment.NewLine)
            'TRACE

            objTempResponse = Sync2(strSupplierId, intVersion)

            'TRACE
            System.IO.File.AppendAllText(strFile, "After Sync 2")
            System.IO.File.AppendAllText(strFile, Environment.NewLine)
            'TRACE

            If Not lstUserToIDs Is Nothing Then
                'TRACE
                System.IO.File.AppendAllText(strFile, "Input Elements : " + lstUserToIDs.Count.ToString())
                System.IO.File.AppendAllText(strFile, Environment.NewLine)
                'TRACE

                For Each objUserToID As UserToIDInfo In lstUserToIDs
                    Dim obase As New BaseResponse
                    If Not objUserToID Is Nothing Then
                        'TRACE
                        System.IO.File.AppendAllText(strFile, String.Format("Request : []User ID: {0} [Supplier ID]: {1} [ID]: {2} [TypeID] : {3}", objUserToID.UserID, objUserToID.SupplierID, objUserToID.ID, objUserToID.TypeID))
                        obase = Modify(objUserToID)

                        ProcessResponse(obase, objTempResponse)
                        System.IO.File.AppendAllText(strFile, "Response : " + Common.SerializationManager.Serialize(obase))
                        System.IO.File.AppendAllText(strFile, Environment.NewLine)
                        'TRACE
                        'Modify(objUserToID)
                    End If
                Next
            End If

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
        End Try
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

End Class