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
Public Class Kpi2
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Kpi2))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objKpi2Info As Kpi2Info) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpi2_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpi2Info.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objKpi2Info.UserID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objKpi2Info.PeriodKey)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpi2Info.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@Data", objKpi2Info.Data)
            cmdCommand.Parameters.AddWithValue("@PeriodDay", objKpi2Info.PeriodDay)
            cmdCommand.Parameters.AddWithValue("@PeriodWeek", objKpi2Info.PeriodWeek)
            cmdCommand.Parameters.AddWithValue("@PeriodMonth", objKpi2Info.PeriodMonth)
            cmdCommand.Parameters.AddWithValue("@PeriodQuarter", objKpi2Info.PeriodQuarter)
            cmdCommand.Parameters.AddWithValue("@PeriodYear", objKpi2Info.PeriodYear)
            cmdCommand.Parameters.AddWithValue("@Status", objKpi2Info.Status)

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
    Public Function Delete(ByVal objKpi2Info As Kpi2Info) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpi2_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpi2Info.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objKpi2Info.UserID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objKpi2Info.PeriodKey)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpi2Info.KpiTypeID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strUserId As String, _
                               ByVal strPeriodKey As String, ByVal strKpiTypeId As String) As Kpi2ReadSingleResponse
        Dim objResponse As New Kpi2ReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_kpi2_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", strPeriodKey)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", strKpiTypeId)

            Dim objKpi2 As Kpi2Info() = Nothing
            objKpi2 = ReadKpi2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpi2 Is Nothing AndAlso objKpi2.GetUpperBound(0) >= 0 Then
                objResponse.Kpi2 = objKpi2(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As Kpi2ReadListResponse
        Dim objResponse As New Kpi2ReadListResponse
        Try
            Dim objKpi2Info As Kpi2Info()
            Dim cmdCommand As New SqlCommand("usp_kpi2_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            objKpi2Info = ReadKpi2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpi2Info Is Nothing AndAlso objKpi2Info.GetUpperBound(0) >= 0 Then
                objResponse.Kpi2s = objKpi2Info
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As Kpi2ReadListResponse
        Dim objResponse As New Kpi2ReadListResponse
        Try
            Dim objKpi2FieldInfo As Kpi2Info()
            Dim cmdCommand As New SqlCommand("usp_kpi2_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objKpi2FieldInfo = ReadKpi2(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpi2FieldInfo Is Nothing AndAlso objKpi2FieldInfo.GetUpperBound(0) >= 0 Then
                objResponse.Kpi2s = objKpi2FieldInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstKpi2 As List(Of Kpi2Info)) As Kpi2Sync3Response
        If _Log.IsDebugEnabled Then _Log.Debug("Entered Kpi2.Sync3 Method...")
        Dim objResponse As New Kpi2Sync3Response
        Dim objTempResponse As New Kpi2ReadListResponse
        Try

            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Method Parameters: Supplier ID: {0} User ID: {1} Version: {2}", strSupplierId, strUserId, intVersion))

            objTempResponse = Sync2(strSupplierId, strUserId, intVersion)

            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Sync2: {0} ", SerializationManager.Serialize(objTempResponse)))

            If Not lstKpi2 Is Nothing Then

                If _Log.IsDebugEnabled Then _Log.Debug("Starting Sync3 Updates...")
                Dim objModifyResponse As BaseResponse

                For Each objKpi2 As Kpi2Info In lstKpi2
                    If Not objKpi2 Is Nothing Then
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Input to Modify: {0} ", SerializationManager.Serialize(objKpi2)))

                        objModifyResponse = Modify(objKpi2)
                        ProcessResponse(objModifyResponse, objTempResponse)
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Modify: {0} ", SerializationManager.Serialize(objModifyResponse)))
                    End If
                Next

                If _Log.IsDebugEnabled Then _Log.Debug("Sync3 Updates completed...")

            Else
                If _Log.IsDebugEnabled Then _Log.Debug("No data to update. input list is emtpy...")
            End If

            objResponse.Kpi2s = objTempResponse.Kpi2s
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.kpi2)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Exception: {0} ", ex.ToString()))

            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try

        If _Log.IsDebugEnabled Then _Log.Debug("Exiting Kpi2.Sync3 Method")

        Return objResponse
    End Function

    Private Function ReadKpi2(ByVal objReader As SqlDataReader) As Kpi2Info()
        Dim objKpi2 As Kpi2Info() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objKpi2(intCounter)
                    objKpi2(intCounter) = New Kpi2Info
                    With objKpi2(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .UserID = CheckString(objReader("UserID"))
                        .PeriodKey = CheckString(objReader("PeriodKey"))
                        .KpiTypeID = CheckString(objReader("KpiTypeID"))
                        .Data = CheckDecimal(objReader("Data"))
                        .PeriodDay = CheckString(objReader("PeriodDay"))
                        .PeriodWeek = CheckString(objReader("PeriodWeek"))
                        .PeriodMonth = CheckString(objReader("PeriodMonth"))
                        .PeriodQuarter = CheckString(objReader("PeriodQuarter"))
                        .PeriodYear = CheckString(objReader("PeriodYear"))
                        .Deleted = CheckDeletedField(objReader)
                        .Status = CheckString(objReader("Status"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objKpi2
    End Function

End Class