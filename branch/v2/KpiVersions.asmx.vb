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
Public Class KpiVersions
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objKpiVersionInfo As kpiVersionInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpiversion_add")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiVersionInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@VersionID", objKpiVersionInfo.VersionID)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiVersionInfo.Description)
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
    Public Function Change(ByVal objKpiVersionInfo As kpiVersionInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpiversion_change")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiVersionInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@VersionID", objKpiVersionInfo.VersionID)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiVersionInfo.Description)
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
    Public Function Modify(ByVal objKpiVersionInfo As KpiVersionInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpiversion_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiVersionInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@VersionID", objKpiVersionInfo.VersionID)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiVersionInfo.Description)
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
    Public Function Delete(ByVal objKpiVersionInfo As kpiVersionInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpiversion_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiVersionInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@VersionID", objKpiVersionInfo.VersionID)
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
    Public Function ReadSingle(ByVal strVersionId As String, ByVal strSupplierId As String) As KpiVersionReadSingleResponse
        Dim objResponse As New KpiVersionReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_kpiversion_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@VersionID", strVersionId)
            Dim objKpiVersions As kpiVersionInfo() = Nothing
            objKpiVersions = ReadKpiVersions(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiVersions Is Nothing AndAlso objKpiVersions.GetUpperBound(0) >= 0 Then
                objResponse.KpiVersion = objKpiVersions(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As KpiVersionReadListResponse
        Dim objResponse As New KpiVersionReadListResponse
        Try
            Dim objKpiVersionInfo As kpiVersionInfo()
            Dim cmdCommand As New SqlCommand("usp_kpiversion_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objKpiVersionInfo = ReadKpiVersions(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiVersionInfo Is Nothing AndAlso objKpiVersionInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiVersions = objKpiVersionInfo
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As KpiVersionReadListResponse
    '    Dim objResponse As New KpiVersionReadListResponse
    '    Try
    '        Dim objKpiVersionInfo As kpiVersionInfo()
    '        Dim cmdCommand As New SqlCommand("usp_kpiversion_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objKpiVersionInfo = ReadKpiVersions(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objKpiVersionInfo Is Nothing AndAlso objKpiVersionInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.KpiVersions = objKpiVersionInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As KpiVersionReadListResponse
        Dim objResponse As New KpiVersionReadListResponse
        Try
            Dim objKpiVersionInfo As KpiVersionInfo()
            Dim cmdCommand As New SqlCommand("usp_kpiversion_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objKpiVersionInfo = ReadKpiVersions(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiVersionInfo Is Nothing AndAlso objKpiVersionInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiVersions = objKpiVersionInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstKpiVersions As List(Of KpiVersionInfo)) As KpiVersionSync3Response
        Dim objResponse As New KpiVersionSync3Response
        Dim objTempResponse As New KpiVersionReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstKpiVersions Is Nothing Then
                For Each objKpiVersion As KpiVersionInfo In lstKpiVersions
                    If Not objKpiVersion Is Nothing Then
                        ProcessResponse(Modify(objKpiVersion), objTempResponse)
                    End If
                Next
            End If

            objResponse.KpiVersions = objTempResponse.KpiVersions
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.KpiVersions)
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

    Private Function ReadKpiVersions(ByVal objReader As SqlDataReader) As kpiVersionInfo()
        Dim objKpiVersions As kpiVersionInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objKpiVersions(intCounter)
                    objKpiVersions(intCounter) = New kpiVersionInfo
                    With objKpiVersions(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .VersionID = CheckString(objReader("VersionID"))
                        .Description = CheckString(objReader("Description"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objKpiVersions
    End Function
End Class