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
Public Class TargetTypes
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Tables))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objTargetTypeInfo As TargetTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_targettypes_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objTargetTypeInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objTargetTypeInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@PeriodType", objTargetTypeInfo.PeriodType)
            cmdCommand.Parameters.AddWithValue("@Description", objTargetTypeInfo.Description)
            cmdCommand.Parameters.AddWithValue("@UseUsersData", objTargetTypeInfo.UseUsersData)

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objTargetTypeInfo), ex)
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
    Public Function Delete(ByVal objTargetTypeInfo As TargetTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_targettype_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objTargetTypeInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objTargetTypeInfo.TargetTypeID)

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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As TargetTypeReadListResponse
        Dim objResponse As New TargetTypeReadListResponse
        Try
            Dim objTargetTypeInfo As TargetTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_targettype_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objTargetTypeInfo = ReadTargetTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objTargetTypeInfo Is Nothing AndAlso objTargetTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.TargetTypes = objTargetTypeInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As TargetTypeReadListResponse
        Dim objResponse As New TargetTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objTargetTypeInfo As TargetTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_targettypes_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objTargetTypeInfo = ReadTargetTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objTargetTypeInfo Is Nothing AndAlso objTargetTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.TargetTypes = objTargetTypeInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstTargetTypes As List(Of TargetTypeInfo)) As TargetTypeSync3Response
        Dim objResponse As New TargetTypeSync3Response
        Dim objTempResponse As New TargetTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstTargetTypes Is Nothing Then
                For Each objTargetType As TargetTypeInfo In lstTargetTypes
                    If Not objTargetType Is Nothing Then
                        ProcessResponse(Modify(objTargetType), objTempResponse)
                    End If
                Next
            End If

            objResponse.TargetTypes = objTempResponse.TargetTypes
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.TargetTypes)
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

    Private Function ReadTargetTypes(ByVal objReader As SqlDataReader) As TargetTypeInfo()
        Dim objTargetTypes As TargetTypeInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objTargetTypes(intCounter)
                    objTargetTypes(intCounter) = New TargetTypeInfo
                    With objTargetTypes(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .TargetTypeID = CheckString(objReader("TargetTypeID"))
                        .PeriodType = CheckString(objReader("PeriodType"))
                        .FieldType = CheckInteger(objReader("FieldType"))
                        .Description = CheckString(objReader("Description"))
                        .Deleted = CheckDeletedField(objReader)
                        .UseUsersData = CheckBoolean(objReader("UseUsersData"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objTargetTypes
    End Function
End Class