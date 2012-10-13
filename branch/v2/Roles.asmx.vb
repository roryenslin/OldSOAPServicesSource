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
Public Class Roles
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objRoleInfo As RoleInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_role_add")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objRoleInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RoleID", objRoleInfo.RoleId)
            cmdCommand.Parameters.AddWithValue("@RoleName", objRoleInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Description", objRoleInfo.Description)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows inserted in database. Error returned" + intResult.ToString()
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
    Public Function Change(ByVal objRoleInfo As RoleInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_role_change")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objRoleInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RoleID", objRoleInfo.RoleId)
            cmdCommand.Parameters.AddWithValue("@RoleName", objRoleInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Description", objRoleInfo.Description)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows updated in database. Error returned" + intResult.ToString()
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
    Public Function Modify(ByVal objRoleInfo As RoleInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_role_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objRoleInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RoleID", objRoleInfo.RoleId)
            cmdCommand.Parameters.AddWithValue("@RoleName", objRoleInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Description", objRoleInfo.Description)

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
    Public Function Delete(ByVal objRoleInfo As RoleInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_role_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objRoleInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RoleID", objRoleInfo.RoleId)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strRoleId As String) As RoleReadSingleResponse
        Dim objResponse As New RoleReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_role_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@RoleID", strRoleId)

            Dim objRoles As RoleInfo() = Nothing
            objRoles = ReadRoles(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objRoles Is Nothing AndAlso objRoles.GetUpperBound(0) >= 0 Then
                objResponse.Role = objRoles(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As RoleReadListResponse
        Dim objResponse As New RoleReadListResponse
        Try
            Dim objRoleInfo As RoleInfo()
            Dim cmdCommand As New SqlCommand("usp_role_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objRoleInfo = ReadRoles(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objRoleInfo Is Nothing AndAlso objRoleInfo.GetUpperBound(0) >= 0 Then
                objResponse.Roles = objRoleInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As RoleReadListResponse
        Dim objResponse As New RoleReadListResponse
        Try
            Dim objRoleInfo As RoleInfo()
            Dim cmdCommand As New SqlCommand("usp_role_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objRoleInfo = ReadRoles(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objRoleInfo Is Nothing AndAlso objRoleInfo.GetUpperBound(0) >= 0 Then
                objResponse.Roles = objRoleInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstRoles As List(Of RoleInfo)) As RoleSync3Response
        Dim objResponse As New RoleSync3Response
        Dim objTempResponse As New RoleReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstRoles Is Nothing Then
                For Each objRole As RoleInfo In lstRoles
                    If Not objRole Is Nothing Then
                        ProcessResponse(Modify(objRole), objTempResponse)
                    End If
                Next
            End If

            objResponse.Roles = objTempResponse.Roles
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Role)
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

    Private Function ReadRoles(ByVal objReader As SqlDataReader) As RoleInfo()
        Dim objRoles As RoleInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objRoles(intCounter)
                    objRoles(intCounter) = New RoleInfo
                    With objRoles(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .RoleId = CheckString(objReader("RoleID"))
                        .Name = CheckString(objReader("RoleName"))
                        .Description = CheckString(objReader("Description"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objRoles
    End Function
End Class