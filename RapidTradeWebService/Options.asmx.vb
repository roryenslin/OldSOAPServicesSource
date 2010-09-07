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
Public Class Options
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Options))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objOptionInfo As OptionInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_option_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objOptionInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@Name", objOptionInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Group", objOptionInfo.Group)
            cmdCommand.Parameters.AddWithValue("@Type", objOptionInfo.Type)
            cmdCommand.Parameters.AddWithValue("@Value", objOptionInfo.Value)
            cmdCommand.Parameters.AddWithValue("@Platform", objOptionInfo.Platform)

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objOptionInfo), ex)
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
    Public Function Delete(ByVal objOptionInfo As OptionInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_option_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objOptionInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@Name", objOptionInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Group", objOptionInfo.Group)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strName As String, ByVal strGroup As String) As OptionReadSingleResponse
        Dim objResponse As New OptionReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_option_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Name", strName)
            cmdCommand.Parameters.AddWithValue("@Group", strGroup)
            Dim objOptions As OptionInfo() = Nothing
            objOptions = ReadOptions(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOptions Is Nothing AndAlso objOptions.GetUpperBound(0) >= 0 Then
                objResponse.OptionData = objOptions(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As OptionReadListResponse
        Dim objResponse As New OptionReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objOptionInfo As OptionInfo()
            Dim cmdCommand As New SqlCommand("usp_option_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            objOptionInfo = ReadOptions(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOptionInfo Is Nothing AndAlso objOptionInfo.GetUpperBound(0) >= 0 Then
                objResponse.Options = objOptionInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As OptionReadListResponse
        Dim objResponse As New OptionReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)
            Dim objOptionInfo As OptionInfo()
            Dim cmdCommand As New SqlCommand("usp_option_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objOptionInfo = ReadOptions(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOptionInfo Is Nothing AndAlso objOptionInfo.GetUpperBound(0) >= 0 Then
                objResponse.Options = objOptionInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstOption As List(Of OptionInfo)) As OptionSync3Response
        Dim objResponse As New OptionSync3Response
        Dim objTempResponse As New OptionReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)

            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstOption Is Nothing Then
                For Each objOption As OptionInfo In lstOption
                    If Not objOption Is Nothing Then
                        ProcessResponse(Modify(objOption), objTempResponse)
                    End If
                Next
            End If

            objResponse.Options = objTempResponse.Options
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.OptionInfo)
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
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    Private Function ReadOptions(ByVal objReader As SqlDataReader) As OptionInfo()
        Dim objOptions As OptionInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objOptions(intCounter)
                    objOptions(intCounter) = New OptionInfo
                    With objOptions(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .Name = CheckString(objReader("Name"))
                        .Group = CheckString(objReader("Group"))
                        .Type = CheckString(objReader("Type"))
                        .Value = CheckString(objReader("Value"))
                        .Platform = CheckShort(objReader("Platform"))
                        .Deleted = CheckDeletedField(objReader)
                        
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objOptions
    End Function

End Class