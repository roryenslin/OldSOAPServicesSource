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
Public Class RolesToID
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objRoleToIDInfo As RoleToIDInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_roletoid_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objRoleToIDInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RoleID", objRoleToIDInfo.RoleID)
            cmdCommand.Parameters.AddWithValue("@TypeID", objRoleToIDInfo.TypeID)
            cmdCommand.Parameters.AddWithValue("@ID", objRoleToIDInfo.ID)

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
    Public Function Delete(ByVal objRoleToIDInfo As RoleToIDInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_roletoid_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objRoleToIDInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RoleID", objRoleToIDInfo.RoleID)
            cmdCommand.Parameters.AddWithValue("@TypeID", objRoleToIDInfo.TypeID)

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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As RoleToIDReadListResponse
        Dim objResponse As New RoleToIDReadListResponse
        Try
            Dim objRoleToIDInfo As RoleToIDInfo()
            Dim cmdCommand As New SqlCommand("usp_roletoid_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objRoleToIDInfo = ReadRoleToIDs(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objRoleToIDInfo Is Nothing AndAlso objRoleToIDInfo.GetUpperBound(0) >= 0 Then
                objResponse.RoleToIDs = objRoleToIDInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As RoleToIDReadListResponse
        Dim objResponse As New RoleToIDReadListResponse
        Try
            Dim objRoleToIDInfo As RoleToIDInfo()
            Dim cmdCommand As New SqlCommand("usp_roletoid_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objRoleToIDInfo = ReadRoleToIDs(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objRoleToIDInfo Is Nothing AndAlso objRoleToIDInfo.GetUpperBound(0) >= 0 Then
                objResponse.RoleToIDs = objRoleToIDInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstRoleToIDs As List(Of RoleToIDInfo)) As RoleToIDSync3Response
        Dim objResponse As New RoleToIDSync3Response
        Dim objTempResponse As New RoleToIDReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstRoleToIDs Is Nothing Then
                For Each objRoleToID As RoleToIDInfo In lstRoleToIDs
                    If Not objRoleToID Is Nothing Then
                        ProcessResponse(Modify(objRoleToID), objTempResponse)
                    End If
                Next
            End If

            objResponse.RoleToIDs = objTempResponse.RoleToIDs
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.RoleToID)
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

    Private Function ReadRoleToIDs(ByVal objReader As SqlDataReader) As RoleToIDInfo()
        Dim objRoleToIDs As RoleToIDInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            Dim iTemp As Integer
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objRoleToIDs(intCounter)
                    objRoleToIDs(intCounter) = New RoleToIDInfo
                    With objRoleToIDs(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .RoleID = CheckString(objReader("RoleID"))
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
        Return objRoleToIDs
    End Function

End Class