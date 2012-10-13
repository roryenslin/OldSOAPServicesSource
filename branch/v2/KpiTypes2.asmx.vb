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
<System.Web.Services.WebServiceBinding(ConFormsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class KpiTypes2
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Kpi2))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objKpiType2Info As KpiType2Info) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpitypes2_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiType2Info.SupplierID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiType2Info.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objKpiType2Info.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@PeriodType", objKpiType2Info.PeriodType)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiType2Info.Description)
            cmdCommand.Parameters.AddWithValue("@FieldType", objKpiType2Info.FieldType)
            cmdCommand.Parameters.AddWithValue("@Status", objKpiType2Info.Status)
            cmdCommand.Parameters.AddWithValue("@UseUsersData", objKpiType2Info.UseUsersData)

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
    Public Function Delete(ByVal objKpiType2Info As KpiType2Info) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpitypes2_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiType2Info.SupplierID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiType2Info.KpiTypeID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strKpiTypeId As String) As KpiType2ReadSingleResponse
        Dim objResponse As New KpiType2ReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_kpitypes2_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", strKpiTypeId)
            Dim objKpiTypes2 As KpiType2Info() = Nothing
            objKpiTypes2 = ReadKpiTypes2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiTypes2 Is Nothing AndAlso objKpiTypes2.GetUpperBound(0) >= 0 Then
                objResponse.KpiType2 = objKpiTypes2(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As KpiType2ReadListResponse
        Dim objResponse As New KpiType2ReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objKpiType2Info As KpiType2Info()
            Dim cmdCommand As New SqlCommand("usp_kpitypes2_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            objKpiType2Info = ReadKpiTypes2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiType2Info Is Nothing AndAlso objKpiType2Info.GetUpperBound(0) >= 0 Then
                objResponse.KpiTypes2 = objKpiType2Info
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As KpiType2ReadListResponse
        Dim objResponse As New KpiType2ReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)

            Dim objKpiType2FieldInfo As KpiType2Info()
            Dim cmdCommand As New SqlCommand("usp_kpitypes2_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objKpiType2FieldInfo = ReadKpiTypes2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiType2FieldInfo Is Nothing AndAlso objKpiType2FieldInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiTypes2 = objKpiType2FieldInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstKpiTypes2 As List(Of KpiType2Info)) As KpiType2Sync3Response
        If _Log.IsDebugEnabled Then _Log.Debug("Entered KpiTypes2.Sync3 Method...")

        Dim objResponse As New KpiType2Sync3Response
        Dim objTempResponse As New KpiType2ReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)

            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstKpiTypes2 Is Nothing Then
                Dim objModifyResponse As BaseResponse
                For Each objKpiType2 As KpiType2Info In lstKpiTypes2
                    If Not objKpiType2 Is Nothing Then
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Input to Modify: {0} ", SerializationManager.Serialize(objKpiType2)))
                        objModifyResponse = Modify(objKpiType2)
                        ProcessResponse(objModifyResponse, objTempResponse)
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Modify: {0} ", SerializationManager.Serialize(objModifyResponse)))
                    End If
                Next
            Else
                If _Log.IsDebugEnabled Then _Log.Debug("No data to update. input list is emtpy...")
            End If

            objResponse.KpiTypes2 = objTempResponse.KpiTypes2
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.KpiTypes2)
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

    Private Function ReadKpiTypes2(ByVal objReader As SqlDataReader) As KpiType2Info()
        Dim objKpiTypes2 As KpiType2Info() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objKpiTypes2(intCounter)
                    objKpiTypes2(intCounter) = New KpiType2Info
                    With objKpiTypes2(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .KpiTypeID = CheckString(objReader("KpiTypeID"))
                        .TargetTypeID = CheckString(objReader("TargetTypeID"))
                        .PeriodType = CheckInteger(objReader("PeriodType"))
                        .Description = CheckString(objReader("Description"))
                        .FieldType = CheckInteger(objReader("FieldType"))
                        .Status = CheckString(objReader("Status"))
                        .Deleted = CheckDeletedField(objReader)
                        .UseUsersData = CheckBoolean(objReader("UseUsersData"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objKpiTypes2
    End Function

End Class