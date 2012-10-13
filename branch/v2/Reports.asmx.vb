Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Reports
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objReportInfo As ReportInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_report_add")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Description", objReportInfo.Description)
            cmdCommand.Parameters.AddWithValue("@HideRepeatValues", objReportInfo.HideRepeatValues)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objReportInfo.LongDescription)

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
    Public Function Change(ByVal objReportInfo As ReportInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_report_change")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Description", objReportInfo.Description)
            cmdCommand.Parameters.AddWithValue("@HideRepeatValues", objReportInfo.HideRepeatValues)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objReportInfo.LongDescription)

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
    Public Function Modify(ByVal objReportInfo As ReportInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_report_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportInfo.ReportID)
            cmdCommand.Parameters.AddWithValue("@Description", objReportInfo.Description)
            cmdCommand.Parameters.AddWithValue("@HideRepeatValues", objReportInfo.HideRepeatValues)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objReportInfo.LongDescription)

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
    Public Function Delete(ByVal objReportInfo As ReportInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_report_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objReportInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", objReportInfo.ReportID)

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
    Public Function ExecuteReport(ByVal strSupplierID As String, ByVal strReportID As String, _
                                  ByVal objParams As ParameterInfo()) As String()()
        Dim objResponse As String()() = Nothing
        Try
            Dim objRegex As New Regex("@(?<param>[a-z_\-0-9]+)", RegexOptions.IgnoreCase)
            Dim objMatches As MatchCollection
            Dim bFirstInd As Boolean
            Dim strCurrentSQL As String
            Dim intCounter As Integer
            Dim objReader As SqlDataReader = Nothing
            Dim objCurrentReader As SqlDataReader = Nothing
            Dim cmdCurrent As SqlCommand = Nothing
            Dim objCurrentDBHelper As New DBHelper
            Dim lstMain As New List(Of List(Of String))
            Dim cmdCommand As New SqlCommand("usp_report_getsqls")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierID)
            cmdCommand.Parameters.AddWithValue("@ReportID", strReportID)
            objReader = objDBHelper.ExecuteReader(cmdCommand)
            While objReader.Read()
                strCurrentSQL = objReader("SQLStatement").ToString()
                If Not String.IsNullOrEmpty(strCurrentSQL) Then
                    For intCounter = 0 To objParams.GetUpperBound(0)
                        strCurrentSQL = strCurrentSQL.Replace(String.Format("<{0}>", intCounter + 1), objParams(intCounter).DefaultData)
                    Next

                    cmdCurrent = New SqlCommand(strCurrentSQL)
                    cmdCurrent.CommandType = CommandType.Text
                    objMatches = objRegex.Matches(strCurrentSQL)
                    intCounter = 0
                    For Each objMatch As Match In objMatches
                        If Not objMatch.Groups("param") Is Nothing Then
                            cmdCurrent.Parameters.AddWithValue(String.Format("@{0}", objMatch.Groups("param").Value), objParams(intCounter).DefaultData)
                            intCounter = intCounter + 1
                        End If
                    Next

                    bFirstInd = True
                    Dim lstTemp As New List(Of String)
                    objCurrentReader = objCurrentDBHelper.ExecuteReader(cmdCurrent, False)

                    While objCurrentReader.Read()

                        If bFirstInd Then
                            lstTemp = New List(Of String)
                            For intCounter = 0 To objCurrentReader.FieldCount - 1
                                lstTemp.Add(objCurrentReader.GetName(intCounter))
                            Next
                            lstMain.Add(lstTemp)
                            bFirstInd = False
                        End If
                        lstTemp = New List(Of String)
                        For intCounter = 0 To objCurrentReader.FieldCount - 1
                            If Not objCurrentReader.GetValue(intCounter) Is Nothing Then
                                lstTemp.Add(objCurrentReader.GetValue(intCounter).ToString())
                            Else
                                lstTemp.Add(String.Empty)
                            End If
                        Next

                        lstMain.Add(lstTemp)
                    End While
                    objCurrentReader.Close()
                End If
            End While
            If Not lstMain Is Nothing AndAlso lstMain.Count > 0 Then
                Dim saResult As String()()
                Dim saTemp As String()
                ReDim saResult(lstMain.Count - 1)
                For intCounter = 0 To lstMain.Count - 1
                    ReDim saTemp(lstMain(intCounter).Count - 1)
                    lstMain(intCounter).CopyTo(saTemp)
                    saResult(intCounter) = saTemp
                Next
                objResponse = saResult
            End If
        Catch ex As Exception
            
        Finally

        End Try
        Return objResponse
    End Function


    '<WebMethod()> _
    'Public Function ExecuteReport(ByVal strSupplierID As String, ByVal strReportID As String, _
    '                              ByVal objParams As ParameterInfo()) As ExecuteReportResponse
    '    Dim objResponse As New ExecuteReportResponse
    '    Try
    '        Dim objRegex As New Regex("@(?<param>[a-z_\-0-9]+)", RegexOptions.IgnoreCase)
    '        Dim objMatches As MatchCollection
    '        Dim bFirstInd As Boolean
    '        Dim strCurrentSQL As String
    '        Dim intCounter As Integer
    '        Dim objReader As SqlDataReader = Nothing
    '        Dim objCurrentReader As SqlDataReader = Nothing
    '        Dim cmdCurrent As SqlCommand = Nothing
    '        Dim objCurrentDBHelper As New DBHelper
    '        Dim lstMain As New List(Of List(Of String))
    '        Dim cmdCommand As New SqlCommand("usp_report_getsqls")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierID)
    '        cmdCommand.Parameters.AddWithValue("@ReportID", strReportID)
    '        objReader = objDBHelper.ExecuteReader(cmdCommand)
    '        While objReader.Read()
    '            strCurrentSQL = objReader("SQLStatement").ToString()
    '            If Not String.IsNullOrEmpty(strCurrentSQL) Then
    '                For intCounter = 0 To objParams.GetUpperBound(0)
    '                    strCurrentSQL = strCurrentSQL.Replace(String.Format("<{0}>", intCounter + 1), objParams(intCounter).DefaultData)
    '                Next

    '                cmdCurrent = New SqlCommand(strCurrentSQL)
    '                cmdCurrent.CommandType = CommandType.Text
    '                objMatches = objRegex.Matches(strCurrentSQL)
    '                intCounter = 0
    '                For Each objMatch As Match In objMatches
    '                    If Not objMatch.Groups("param") Is Nothing Then
    '                        cmdCurrent.Parameters.AddWithValue(String.Format("@{0}", objMatch.Groups("param").Value), objParams(intCounter).DefaultData)
    '                        intCounter = intCounter + 1
    '                    End If
    '                Next

    '                bFirstInd = True
    '                Dim lstTemp As New List(Of String)
    '                objCurrentReader = objCurrentDBHelper.ExecuteReader(cmdCurrent, False)

    '                While objCurrentReader.Read()

    '                    If bFirstInd Then
    '                        lstTemp = New List(Of String)
    '                        For intCounter = 0 To objCurrentReader.FieldCount - 1
    '                            lstTemp.Add(objCurrentReader.GetName(intCounter))
    '                        Next
    '                        lstMain.Add(lstTemp)
    '                        bFirstInd = False
    '                    End If
    '                    lstTemp = New List(Of String)
    '                    For intCounter = 0 To objCurrentReader.FieldCount - 1
    '                        If Not objCurrentReader.GetValue(intCounter) Is Nothing Then
    '                            lstTemp.Add(objCurrentReader.GetValue(intCounter).ToString())
    '                        Else
    '                            lstTemp.Add(String.Empty)
    '                        End If
    '                    Next

    '                    lstMain.Add(lstTemp)
    '                End While
    '                objCurrentReader.Close()
    '            End If
    '        End While
    '        objResponse.Status = True
    '        If Not lstMain Is Nothing AndAlso lstMain.Count > 0 Then
    '            Dim saResult As String()()
    '            Dim saTemp As String()
    '            ReDim saResult(lstMain.Count - 1)
    '            For intCounter = 0 To lstMain.Count - 1
    '                ReDim saTemp(lstMain(intCounter).Count - 1)
    '                lstMain(intCounter).CopyTo(saTemp)
    '                saResult(intCounter) = saTemp
    '            Next
    '            objResponse.Result = saResult
    '        End If
    '    Catch ex As Exception
    '        objResponse.Status = False
    '        Dim intCounter As Integer = 0
    '        While Not ex Is Nothing
    '            ReDim Preserve objResponse.Errors(intCounter)
    '            objResponse.Errors(intCounter) = ex.Message
    '            ex = ex.InnerException
    '        End While
    '    Finally

    '    End Try
    '    Return objResponse
    'End Function

    <WebMethod()> _
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strReportId As String) As ReportReadSingleResponse
        Dim objResponse As New ReportReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_report_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ReportID", strReportId)
            Dim objReports As ReportInfo() = Nothing
            objReports = ReadReports(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReports Is Nothing AndAlso objReports.GetUpperBound(0) >= 0 Then
                objResponse.Report = objReports(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As ReportReadListResponse
        Dim objResponse As New ReportReadListResponse
        Try
            Dim objReportInfo As ReportInfo()
            Dim cmdCommand As New SqlCommand("usp_report_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objReportInfo = ReadReports(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReportInfo Is Nothing AndAlso objReportInfo.GetUpperBound(0) >= 0 Then
                objResponse.Reports = objReportInfo
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
      Public Function ReadList2(ByVal strSupplierId As String) As ReportReadListResponse
        Dim objResponse As New ReportReadListResponse
        Try
            Dim objReportInfo As ReportInfo()
            Dim cmdCommand As New SqlCommand("usp_report_readlist2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objReportInfo = ReadReports(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReportInfo Is Nothing AndAlso objReportInfo.GetUpperBound(0) >= 0 Then
                objResponse.Reports = objReportInfo
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As ReportReadListResponse
    '    Dim objResponse As New ReportReadListResponse
    '    Try
    '        Dim objReportInfo As ReportInfo()
    '        Dim cmdCommand As New SqlCommand("usp_report_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objReportInfo = ReadReports(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objReportInfo Is Nothing AndAlso objReportInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.Reports = objReportInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ReportReadListResponse
        Dim objResponse As New ReportReadListResponse
        Try
            Dim objReportInfo As ReportInfo()
            Dim cmdCommand As New SqlCommand("usp_report_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objReportInfo = ReadReports(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objReportInfo Is Nothing AndAlso objReportInfo.GetUpperBound(0) >= 0 Then
                objResponse.Reports = objReportInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstReports As List(Of ReportInfo)) As ReportReadListResponse
        Dim objResponse As New ReportSync3Response
        Dim objTempResponse As New ReportReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstReports Is Nothing Then
                For Each objReport As ReportInfo In lstReports
                    If Not objReport Is Nothing Then
                        ProcessResponse(Modify(objReport), objTempResponse)
                    End If
                Next
            End If

            objResponse.Reports = objTempResponse.Reports
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Reports)
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

    Private Function ReadReports(ByVal objReader As SqlDataReader) As ReportInfo()
        Dim objReports As ReportInfo() = Nothing
        Dim intCounter As Integer = 0
        Dim objHash As New Hashtable
        Dim strReportKey As String = String.Empty
        Dim objTempReport As ReportInfo = Nothing

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objReports(intCounter)
                    objReports(intCounter) = New ReportInfo
                    With objReports(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ReportID = CheckString(objReader("ReportID"))
                        .Description = CheckString(objReader("Description"))
                        .LongDescription = CheckString(objReader("LongDescription"))
                        Try
                            .HideRepeatValues = CType(objReader("HideRepeatValues"), Boolean)
                        Catch ex As Exception
                        End Try
                        .Deleted = CheckDeletedField(objReader)
                        objHash.Add(String.Format("{0}{1}", Trim(.SupplierID), Trim(.ReportID)), intCounter)
                    End With
                    intCounter = intCounter + 1
                End While

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strReportKey = String.Format("{0}{1}", Trim(CheckString(objReader("SupplierID"))), Trim(CheckString(objReader("ReportID"))))
                        If objHash.ContainsKey(strReportKey) Then
                            objTempReport = objReports(CInt(objHash(strReportKey)))
                            If objTempReport.ReportParameters Is Nothing OrElse objTempReport.ReportParameters.Length = 0 Then
                                ReDim objTempReport.ReportParameters(0)
                            Else
                                ReDim Preserve objTempReport.ReportParameters(objTempReport.ReportParameters.Length)
                            End If
                            objTempReport.ReportParameters(objTempReport.ReportParameters.GetUpperBound(0)) = New ParameterInfo
                            With objTempReport.ReportParameters(objTempReport.ReportParameters.GetUpperBound(0))
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
                        End If
                    End While
                End If

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strReportKey = String.Format("{0}{1}", Trim(CheckString(objReader("SupplierID"))), Trim(CheckString(objReader("ReportID"))))
                        If objHash.ContainsKey(strReportKey) Then
                            objTempReport = objReports(CInt(objHash(strReportKey)))
                            If objTempReport.ReportSQLs Is Nothing OrElse objTempReport.ReportSQLs.Length = 0 Then
                                ReDim objTempReport.ReportSQLs(0)
                            Else
                                ReDim Preserve objTempReport.ReportSQLs(objTempReport.ReportSQLs.Length)
                            End If
                            objTempReport.ReportSQLs(objTempReport.ReportSQLs.GetUpperBound(0)) = New ReportSqlInfo
                            With objTempReport.ReportSQLs(objTempReport.ReportSQLs.GetUpperBound(0))
                                .SupplierID = CheckString(objReader("SupplierID"))
                                .ReportID = CheckString(objReader("ReportID"))
                                .Counter = CheckInteger(objReader("Counter"))
                                .SQLStatement = CheckString(objReader("SQLStatement"))
                                .Deleted = CheckDeletedField(objReader)
                            End With
                        End If
                    End While
                End If

            End If
        Finally
            objReader.Close()
        End Try
        Return objReports
    End Function
End Class