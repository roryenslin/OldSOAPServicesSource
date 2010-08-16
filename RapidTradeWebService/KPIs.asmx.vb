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
Public Class KPIs
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objKpiInfo As KpiInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpi_add")
            cmdCommand.Parameters.AddWithValue("@KpiInfoID", objKpiInfo.KpiInfoID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objKpiInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", objKpiInfo.KpiGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", objKpiInfo.KpiVersionID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objKpiInfo.PeriodKey)
            cmdCommand.Parameters.AddWithValue("@Data", CType(objKpiInfo.Data, Decimal))
            cmdCommand.Parameters.AddWithValue("@PeriodDay", objKpiInfo.PeriodDay)
            cmdCommand.Parameters.AddWithValue("@PeriodWeek", objKpiInfo.PeriodWeek)
            cmdCommand.Parameters.AddWithValue("@PeriodMonth", objKpiInfo.PeriodMonth)
            cmdCommand.Parameters.AddWithValue("@AccntGrp", objKpiInfo.AccntGroup)
            cmdCommand.Parameters.AddWithValue("@AccntID", objKpiInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductGrp", objKpiInfo.ProductGroup)
            cmdCommand.Parameters.AddWithValue("@ProductID", objKpiInfo.ProductID)

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
    Public Function Change(ByVal objKpiInfo As KpiInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpi_change")
            cmdCommand.Parameters.AddWithValue("@KpiInfoID", objKpiInfo.KpiInfoID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objKpiInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", objKpiInfo.KpiGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", objKpiInfo.KpiVersionID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objKpiInfo.PeriodKey)
            cmdCommand.Parameters.AddWithValue("@Data", CType(objKpiInfo.Data, Decimal))
            cmdCommand.Parameters.AddWithValue("@PeriodDay", objKpiInfo.PeriodDay)
            cmdCommand.Parameters.AddWithValue("@PeriodWeek", objKpiInfo.PeriodWeek)
            cmdCommand.Parameters.AddWithValue("@PeriodMonth", objKpiInfo.PeriodMonth)
            cmdCommand.Parameters.AddWithValue("@AccntGrp", objKpiInfo.AccntGroup)
            cmdCommand.Parameters.AddWithValue("@AccntID", objKpiInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductGrp", objKpiInfo.ProductGroup)
            cmdCommand.Parameters.AddWithValue("@ProductID", objKpiInfo.ProductID)

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
    Public Function Modify(ByVal objKpiInfo As KpiInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpi_modify")
            cmdCommand.Parameters.AddWithValue("@KpiInfoID", objKpiInfo.KpiInfoID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objKpiInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", objKpiInfo.KpiGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", objKpiInfo.KpiVersionID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objKpiInfo.PeriodKey)
            cmdCommand.Parameters.AddWithValue("@Data", CType(objKpiInfo.Data, Decimal))
            cmdCommand.Parameters.AddWithValue("@PeriodDay", objKpiInfo.PeriodDay)
            cmdCommand.Parameters.AddWithValue("@PeriodWeek", objKpiInfo.PeriodWeek)
            cmdCommand.Parameters.AddWithValue("@PeriodMonth", objKpiInfo.PeriodMonth)
            cmdCommand.Parameters.AddWithValue("@AccntGrp", objKpiInfo.AccntGroup)
            cmdCommand.Parameters.AddWithValue("@AccntID", objKpiInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductGrp", objKpiInfo.ProductGroup)
            cmdCommand.Parameters.AddWithValue("@ProductID", objKpiInfo.ProductID)

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
    Public Function Delete(ByVal objKpiInfo As KpiInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpi_delete")
            cmdCommand.Parameters.AddWithValue("@KpiInfoID", objKpiInfo.KpiInfoID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objKpiInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", objKpiInfo.KpiGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", objKpiInfo.KpiVersionID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objKpiInfo.PeriodKey)

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
    Public Function ReadSingle(ByVal strKpiInfoID As String, ByVal strSupplierId As String, ByVal strUserId As String, ByVal strKpiTypeId As String, _
                               ByVal strKpiGroupId As String, ByVal strKpiVersionId As String, ByVal strPeriodKey As String) As KpiReadSingleResponse
        Dim objResponse As New KpiReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_kpi_readsingle")
            cmdCommand.Parameters.AddWithValue("@KpiInfoID", strKpiInfoID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", strKpiTypeId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", strKpiGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", strKpiVersionId)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", strPeriodKey)

            Dim objKpis As KpiInfo() = Nothing
            objKpis = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpis Is Nothing AndAlso objKpis.GetUpperBound(0) >= 0 Then
                objResponse.Kpi = objKpis(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As KpiReadListResponse
        Dim objResponse As New KpiReadListResponse
        Try
            Dim objKpiInfo As KpiInfo()
            Dim cmdCommand As New SqlCommand("usp_kpi_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objKpiInfo = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiInfo Is Nothing AndAlso objKpiInfo.GetUpperBound(0) >= 0 Then
                objResponse.Kpis = objKpiInfo
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
        Public Function ReadListPerGroupVersionPeriod(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strGroupId As String, ByVal strVersionID As String, ByVal strPeriodKey As String) As KpiReadListResponse
        Dim objResponse As New KpiReadListResponse
        Try
            Dim objKpiInfo As KpiInfo()
            Dim cmdCommand As New SqlCommand("usp_kpi_readlistpergroupversionperiod")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", strGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", strVersionID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", strPeriodKey)

            objKpiInfo = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiInfo Is Nothing AndAlso objKpiInfo.GetUpperBound(0) >= 0 Then
                objResponse.Kpis = objKpiInfo
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
        Public Function ReadListPerTypeGroupVersionPeriod(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strTypeId As String, ByVal strGroupId As String, ByVal strVersionID As String, ByVal strPeriodKey As String) As KpiReadSingleResponse
        Dim objResponse As New KpiReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_kpi_readlistpertypegroupversionperiod")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@KpiTypeId", strTypeId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", strGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiVersionID", strVersionID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", strPeriodKey)

            Dim objKpis As KpiInfo() = Nothing
            objKpis = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
            If Not objKpis Is Nothing AndAlso objKpis.GetUpperBound(0) >= 0 Then
                objResponse.Status = True
                objResponse.Kpi = objKpis(0)
            Else
                objResponse.Status = True
                objResponse.Kpi = New KpiInfo("", strSupplierId, strUserId, strTypeId, strGroupId, strVersionID, strPeriodKey, 0, "", "", "", "", "", "", "")
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
    Public Function ReadListMultiplePeriods(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strTypeId As String, ByVal strGroupId As String, ByVal strVersionID As String, ByVal strPeriodKeys As String()) As KpiReadListResponse
        Dim objResponse As New KpiReadListResponse
        Dim result As New List(Of KpiInfo)
        Dim objKpis As KpiInfo() = Nothing
        Try
            Dim cmdCommand As New SqlCommand("usp_kpi_readlistpertypegroupversionperiod")
            For Each periodKey As String In strPeriodKeys
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
                cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
                cmdCommand.Parameters.AddWithValue("@KpiTypeId", strTypeId)
                cmdCommand.Parameters.AddWithValue("@KpiGroupId", strGroupId)
                cmdCommand.Parameters.AddWithValue("@KpiVersionID", strVersionID)
                cmdCommand.Parameters.AddWithValue("@PeriodKey", periodKey)
                objKpis = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
                If Not objKpis Is Nothing AndAlso objKpis.GetUpperBound(0) >= 0 Then
                    result.Add(objKpis(0))
                Else
                    result.Add(New KpiInfo("", strSupplierId, strUserId, strTypeId, strGroupId, strVersionID, periodKey, 0, "", "", "", "", "", "", ""))
                End If
            Next
            objResponse.Status = True
            objResponse.Kpis = result.ToArray
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
    Public Function ReadListMultipleTypes(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strTypeIds As String(), ByVal strGroupId As String, ByVal strVersionID As String, ByVal strPeriodKey As String) As KpiReadListResponse
        Dim objResponse As New KpiReadListResponse
        Dim result As New List(Of KpiInfo)
        Dim objKpis As KpiInfo() = Nothing
        Try
            Dim cmdCommand As New SqlCommand("usp_kpi_readlistpertypegroupversionperiod")
            For Each typeID As String In strTypeIds
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
                cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
                cmdCommand.Parameters.AddWithValue("@KpiTypeId", typeID)
                cmdCommand.Parameters.AddWithValue("@KpiGroupId", strGroupId)
                cmdCommand.Parameters.AddWithValue("@KpiVersionID", strVersionID)
                cmdCommand.Parameters.AddWithValue("@PeriodKey", strPeriodKey)
                objKpis = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
                If Not objKpis Is Nothing AndAlso objKpis.GetUpperBound(0) >= 0 Then
                    result.Add(objKpis(0))
                Else
                    result.Add(New KpiInfo("", strSupplierId, strUserId, typeID, strGroupId, strVersionID, strPeriodKey, 0, "", "", "", "", "", "", ""))
                End If
            Next
            objResponse.Status = True
            objResponse.Kpis = result.ToArray
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal strUserId As String, ByVal dtLastUpdtTime As DateTime) As KpiReadListResponse
    '    Dim objResponse As New KpiReadListResponse
    '    Try
    '        Dim objKpiInfo As KpiInfo()
    '        Dim cmdCommand As New SqlCommand("usp_kpi_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objKpiInfo = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objKpiInfo Is Nothing AndAlso objKpiInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.Kpis = objKpiInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As KpiReadListResponse
        Dim objResponse As New KpiReadListResponse
        Try
            Dim objKpiInfo As KpiInfo()
            Dim cmdCommand As New SqlCommand("usp_kpi_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objKpiInfo = ReadKpis(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiInfo Is Nothing AndAlso objKpiInfo.GetUpperBound(0) >= 0 Then
                objResponse.Kpis = objKpiInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstKpis As List(Of KpiInfo)) As KpiSync3Response
        Dim objResponse As New KpiSync3Response
        Dim objTempResponse As New KpiReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, strUserId, intVersion)

            If Not lstKpis Is Nothing Then
                For Each objKpi As KpiInfo In lstKpis
                    If Not objKpi Is Nothing Then
                        ProcessResponse(Modify(objKpi), objTempResponse)
                    End If
                Next
            End If

            objResponse.Kpis = objTempResponse.Kpis
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.KpiInfo)
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

    Private Function ReadKpis(ByVal objReader As SqlDataReader) As KpiInfo()
        Dim objKpis As KpiInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objKpis(intCounter)
                    objKpis(intCounter) = New KpiInfo
                    With objKpis(intCounter)
                        .KpiInfoID = CheckString(objReader("KpiInfoID"))
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .UserID = CheckString(objReader("UserID"))
                        .KpiTypeID = CheckString(objReader("KpiTypeID"))
                        .KpiGroupId = CheckString(objReader("KpiGroupId"))
                        .KpiVersionID = CheckString(objReader("KpiVersionID"))
                        .PeriodKey = CheckString(objReader("PeriodKey"))
                        .Data = CType(CheckDecimal(objReader("Data")), Double)
                        .PeriodDay = CheckString(objReader("PeriodDay"))
                        .PeriodWeek = CheckString(objReader("PeriodWeek"))
                        .PeriodMonth = CheckString(objReader("PeriodMonth"))
                        .AccntGroup = CheckString(objReader("AccntGrp"))
                        .AccountID = CheckString(objReader("AccntID"))
                        .ProductGroup = CheckString(objReader("ProductGrp"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objKpis
    End Function

End Class