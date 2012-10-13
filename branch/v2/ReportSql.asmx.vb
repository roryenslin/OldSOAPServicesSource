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
Public Class ReportSql
    Inherits System.Web.Services.WebService
    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objReportSqlInfo As ReportSqlInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_reportsql_add")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportSqlInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportSqlInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objReportSqlInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@SQLStatement", objReportSqlInfo.SQLStatement)

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
    Public Function Change(ByVal objReportSqlInfo As ReportSqlInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_reportsql_change")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportSqlInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportSqlInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objReportSqlInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@SQLStatement", objReportSqlInfo.SQLStatement)

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
    Public Function Modify(ByVal objReportSqlInfo As ReportSqlInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_reportsql_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportSqlInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportSqlInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objReportSqlInfo.Counter)
            cmdCommand.Parameters.AddWithValue("@SQLStatement", objReportSqlInfo.SQLStatement)

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
    Public Function Delete(ByVal objReportSqlInfo As ReportSqlInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_reportsql_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportSqlInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportSqlInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Counter", objReportSqlInfo.Counter)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strReportId As String, ByVal iCounter As Integer) As ReportSqlReadSingleResponse
        Dim objResponse As New ReportSqlReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_reportsql_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ReportID", strReportId)
            cmdCommand.Parameters.AddWithValue("@Counter", iCounter)
            Dim objReportSqls As ReportSqlInfo() = Nothing
            objReportSqls = ReadReportSqls(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReportSqls Is Nothing AndAlso objReportSqls.GetUpperBound(0) >= 0 Then
                objResponse.ReportSql = objReportSqls(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strReportId As String) As ReportSqlReadListResponse
        Dim objResponse As New ReportSqlReadListResponse
        Try
            Dim objReportSqlInfo As ReportSqlInfo()
            Dim cmdCommand As New SqlCommand("usp_reportsql_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ReportID", strReportId)
            objReportSqlInfo = ReadReportSqls(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReportSqlInfo Is Nothing AndAlso objReportSqlInfo.GetUpperBound(0) >= 0 Then
                objResponse.ReportSqls = objReportSqlInfo
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As ReportSqlReadListResponse
    '    Dim objResponse As New ReportSqlReadListResponse
    '    Try
    '        Dim objReportSqlInfo As ReportSqlInfo()
    '        Dim cmdCommand As New SqlCommand("usp_reportsql_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objReportSqlInfo = ReadReportSqls(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objReportSqlInfo Is Nothing AndAlso objReportSqlInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.ReportSqls = objReportSqlInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ReportSqlReadListResponse
        Dim objResponse As New ReportSqlReadListResponse
        Try
            Dim objReportSqlInfo As ReportSqlInfo()
            Dim cmdCommand As New SqlCommand("usp_reportsql_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objReportSqlInfo = ReadReportSqls(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReportSqlInfo Is Nothing AndAlso objReportSqlInfo.GetUpperBound(0) >= 0 Then
                objResponse.ReportSqls = objReportSqlInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstReportSQL As List(Of ReportSqlInfo)) As ReportSqlSync3Response
        Dim objResponse As New ReportSqlSync3Response
        Dim objTempResponse As New ReportSqlReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstReportSQL Is Nothing Then
                For Each objReportSQL As ReportSqlInfo In lstReportSQL
                    If Not objReportSQL Is Nothing Then
                        ProcessResponse(Modify(objReportSQL), objTempResponse)
                    End If
                Next
            End If

            objResponse.ReportSqls = objTempResponse.ReportSqls
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.ReportSql)
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

    Private Function ReadReportSqls(ByVal objReader As SqlDataReader) As ReportSqlInfo()
        Dim objReportSqls As ReportSqlInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objReportSqls(intCounter)
                    objReportSqls(intCounter) = New ReportSqlInfo
                    With objReportSqls(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ReportID = CheckString(objReader("ReportID"))
                        .SQLStatement = CheckString(objReader("SQLStatement"))
                        .Counter = CheckInteger(objReader("Counter"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objReportSqls
    End Function
End Class