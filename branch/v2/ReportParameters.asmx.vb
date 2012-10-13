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
Public Class ReportParameters
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objParameterInfo As ParameterInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_parameter_add")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objParameterInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objParameterInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objParameterInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@Label", objParameterInfo.Label)
            cmdCommand.Parameters.AddWithValue("@FieldType", objParameterInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objParameterInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@DefaultSQL", objParameterInfo.DefaultSQL)
            cmdCommand.Parameters.AddWithValue("@Hidden", objParameterInfo.Hidden)

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
    Public Function Change(ByVal objParameterInfo As ParameterInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_parameter_change")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objParameterInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objParameterInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objParameterInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@Label", objParameterInfo.Label)
            cmdCommand.Parameters.AddWithValue("@FieldType", objParameterInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objParameterInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@DefaultSQL", objParameterInfo.DefaultSQL)
            cmdCommand.Parameters.AddWithValue("@Hidden", objParameterInfo.Hidden)

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
    Public Function Modify(ByVal objParameterInfo As ParameterInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_parameter_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objParameterInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objParameterInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objParameterInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@Label", objParameterInfo.Label)
            cmdCommand.Parameters.AddWithValue("@FieldType", objParameterInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objParameterInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@DefaultSQL", objParameterInfo.DefaultSQL)
            cmdCommand.Parameters.AddWithValue("@Hidden", objParameterInfo.Hidden)

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
    Public Function Delete(ByVal objParameterInfo As ParameterInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_parameter_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objParameterInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objParameterInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objParameterInfo.Counter)

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
    Public Function GetSQLParameter(ByVal strSupplierID As String, ByVal strReportID As String, ByVal intCounter As Integer) As GetParameterResponse
        Dim objResponse As New GetParameterResponse
        Dim objReader As SqlDataReader = Nothing
        Try
            Dim lstResult As New List(Of String)
            Dim cmdCommand As New SqlCommand("usp_parameter_getsqlparameter")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", strReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", intCounter)

            objReader = objDBHelper.ExecuteReader(cmdCommand)
            objResponse.Status = True
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    lstResult.Add(objReader(0).ToString())
                End While
                ReDim objResponse.Results(lstResult.Count - 1)
                lstResult.CopyTo(objResponse.Results)
            End If
        Catch ex As Exception
            objResponse.Status = False
            Dim intCount As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCount)
                objResponse.Errors(intCount) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not objReader Is Nothing Then
                objReader.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function ReadList(ByVal strSupplierId As String, ByVal strReportId As String) As ParameterReadListResponse
        Dim objResponse As New ParameterReadListResponse
        Try
            Dim objParameterInfo As ParameterInfo()
            Dim cmdCommand As New SqlCommand("usp_parameter_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ReportID", strReportId)

            objParameterInfo = ReadParameters(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objParameterInfo Is Nothing AndAlso objParameterInfo.GetUpperBound(0) >= 0 Then
                objResponse.Parameters = objParameterInfo
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

    '<WebMethod()> _
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As ParameterReadListResponse
    '    Dim objResponse As New ParameterReadListResponse
    '    Try
    '        Dim objParameterInfo As ParameterInfo()
    '        Dim cmdCommand As New SqlCommand("usp_parameter_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objParameterInfo = ReadParameters(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objParameterInfo Is Nothing AndAlso objParameterInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.Parameters = objParameterInfo
    '        Else
    '            objResponse.Status = False
    '            ReDim objResponse.Errors(0)
    '            objResponse.Errors(0) = String.Format("No records retrieved for Supplier ID: {0} later than Date: {1}", strSupplierId, dtLastUpdtTime)
    '        End If
    '    Catch ex As Exception
    '        objResponse.Status = False
    '        Dim intCounter As Integer = 0
    '        While Not ex Is Nothing
    '            ReDim Preserve objResponse.Errors(intCounter)
    '            objResponse.Errors(intCounter) = ex.Message
    '            ex = ex.InnerException
    '        End While
    '    End Try
    '    Return objResponse
    'End Function

    <WebMethod()> _
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ParameterReadListResponse
        Dim objResponse As New ParameterReadListResponse
        Try
            Dim objParameterInfo As ParameterInfo()
            Dim cmdCommand As New SqlCommand("usp_parameter_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objParameterInfo = ReadParameters(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objParameterInfo Is Nothing AndAlso objParameterInfo.GetUpperBound(0) >= 0 Then
                objResponse.Parameters = objParameterInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstReportParameters As List(Of ParameterInfo)) As ParameterSync3Response
        Dim objResponse As New ParameterSync3Response
        Dim objTempResponse As New ParameterReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstReportParameters Is Nothing Then
                For Each objParameter As ParameterInfo In lstReportParameters
                    If Not objParameter Is Nothing Then
                        ProcessResponse(Modify(objParameter), objTempResponse)
                    End If
                Next
            End If

            objResponse.Parameters = objTempResponse.Parameters
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.ReportParameters)
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

    Private Function ReadParameters(ByVal objReader As SqlDataReader) As ParameterInfo()
        Dim objParameters As ParameterInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objParameters(intCounter)
                    objParameters(intCounter) = New ParameterInfo
                    With objParameters(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ReportID = CheckString(objReader("ReportID"))
                        .Counter = CheckInteger(objReader("Counter"))
                        .Label = CheckString(objReader("Label"))
                        .FieldType = CheckInteger(objReader("FieldType"))
                        .DefaultData = CheckString(objReader("DefaultData"))
                        .DefaultSQL = CheckString(objReader("DefaultSQL"))
                        .Hidden = CheckBoolean(objReader("Hidden"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objParameters
    End Function

End Class