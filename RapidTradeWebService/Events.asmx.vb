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
Public Class Events
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Events))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    Public Const DATEFORMAT As String = "yyyyMMdd HH:mm:ss"
    Public Const SHORT_DATEFORMAT As String = "yyyyMMdd"
    Public Shared Function ConvertDate(ByVal str As String) As DateTime
        Try
            ConvertDate = DateTime.ParseExact(str, DATEFORMAT, Globalization.CultureInfo.InvariantCulture)
            Return ConvertDate
        Catch ex As Exception

        End Try
        Try
            ConvertDate = DateTime.ParseExact(str, SHORT_DATEFORMAT, Globalization.CultureInfo.InvariantCulture)
            Return ConvertDate
        Catch ex As Exception

        End Try
        Try
            ConvertDate = DateTime.Parse(str)
            Return ConvertDate
        Catch ex As Exception

        End Try
        Return Nothing
    End Function
    Public Shared Function ConvertDate(ByVal [date] As DateTime) As String
        Return [date].ToString(DATEFORMAT)
    End Function

    <WebMethod()> _
    Public Function Add(ByVal objEventInfo As EventInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_add")
            cmdCommand.Parameters.AddWithValue("@EventID", objEventInfo.EventID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objEventInfo.EventTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowOnEvent", objEventInfo.FollowOnEvent)
            cmdCommand.Parameters.AddWithValue("@Data", objEventInfo.Data)
            cmdCommand.Parameters.AddWithValue("@EndDate", ConvertDate(objEventInfo.EndDate))
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate(objEventInfo.DueDate))
            cmdCommand.Parameters.AddWithValue("@UserID", objEventInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objEventInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objEventInfo.Status)
            cmdCommand.Parameters.AddWithValue("@UserID", objEventInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objEventInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objEventInfo.Status)

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
    Public Function Change(ByVal objEventInfo As EventInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_change")
            cmdCommand.Parameters.AddWithValue("@EventID", objEventInfo.EventID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objEventInfo.EventTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowOnEvent", objEventInfo.FollowOnEvent)
            cmdCommand.Parameters.AddWithValue("@Data", objEventInfo.Data)
            
            cmdCommand.Parameters.AddWithValue("@EndDate", ConvertDate(objEventInfo.EndDate))
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate(objEventInfo.DueDate))
            cmdCommand.Parameters.AddWithValue("@UserID", objEventInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objEventInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objEventInfo.Status)
            cmdCommand.Parameters.AddWithValue("@UserID", objEventInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objEventInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objEventInfo.Status)

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
    Public Function Modify(ByVal objEventInfo As EventInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_modify")
            cmdCommand.Parameters.AddWithValue("@EventID", objEventInfo.EventID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objEventInfo.EventTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowOnEvent", objEventInfo.FollowOnEvent)
            cmdCommand.Parameters.AddWithValue("@Data", objEventInfo.Data)
            cmdCommand.Parameters.AddWithValue("@EndDate", ConvertDate(objEventInfo.EndDate))
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate(objEventInfo.DueDate))
            cmdCommand.Parameters.AddWithValue("@UserID", objEventInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objEventInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objEventInfo.Status)

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
    Public Function Delete(ByVal objEventInfo As EventInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_delete")
            cmdCommand.Parameters.AddWithValue("@EventID", objEventInfo.EventID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objEventInfo.EventTypeID)

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
    Public Function ReadSingle(ByVal strEventID As String, ByVal strSupplierId As String, ByVal strEventTypeId As String) As EventReadSingleResponse
        Dim objResponse As New EventReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_event_readsingle")
            cmdCommand.Parameters.AddWithValue("@EventID", strEventID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", strEventTypeId)

            Dim objEvents As EventInfo() = Nothing
            objEvents = ReadEvents(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEvents Is Nothing AndAlso objEvents.GetUpperBound(0) >= 0 Then
                objResponse.EventRecord = objEvents(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As EventReadListResponse
        Dim objResponse As New EventReadListResponse
        Try
            Dim objEventInfo As EventInfo()
            Dim cmdCommand As New SqlCommand("usp_event_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objEventInfo = ReadEvents(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventInfo Is Nothing AndAlso objEventInfo.GetUpperBound(0) >= 0 Then
                objResponse.Events = objEventInfo
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
    Public Function ReadListForAccountDate(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strAccountId As String, ByVal strFromDate As String, ByVal strToDate As String) As EventReadListResponse
        Dim objResponse As New EventReadListResponse
        Try
            Dim objEventInfo As EventInfo()
            Dim cmdCommand As New SqlCommand("usp_event_readlistforaccountdate")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@FromDate", strFromDate)
            cmdCommand.Parameters.AddWithValue("@ToDate", strToDate)
            objEventInfo = ReadEvents(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventInfo Is Nothing AndAlso objEventInfo.GetUpperBound(0) >= 0 Then
                objResponse.Events = objEventInfo
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
    Public Function GetEventDatesForAccount(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strAccountId As String, ByVal strFromDate As String, ByVal strToDate As String) As EventDatesResponse
        Dim objResponse As New EventDatesResponse
        Try
            Dim objEventInfo As EventInfo()
            Dim cmdCommand As New SqlCommand("usp_event_readlistforaccountdate")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@FromDate", strFromDate)
            cmdCommand.Parameters.AddWithValue("@ToDate", strToDate)
            objEventInfo = ReadEvents(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            objResponse.EventDates = New List(Of Integer)
            Dim uniqueList As New Collections.Generic.Dictionary(Of Integer, Integer)
            If Not objEventInfo Is Nothing AndAlso objEventInfo.GetUpperBound(0) >= 0 Then
                For Each evt As EventInfo In objEventInfo
                    Dim value As Integer = Integer.Parse(evt.DueDate.Substring(0, 8))
                    If Not objResponse.EventDates.Contains(value) Then objResponse.EventDates.Add(value)
                Next
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
    Public Function ReadDay(ByVal supplierId As String, ByVal userId As String, ByVal accountId As String, ByVal [date] As String, ByVal timeFormat As String) As List(Of DisplayEventInfo)
        Dim result As New List(Of DisplayEventInfo)
        Dim reader As SqlDataReader = Nothing
        Dim aDate As DateTime = Nothing
        Dim displayEvent As DisplayEventInfo = Nothing
        Try
            Dim cmdCommand As New SqlCommand("usp_event_readday")
            cmdCommand.Parameters.AddWithValue("@SupplierID", supplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", userId)
            cmdCommand.Parameters.AddWithValue("@AccountID", accountId)
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate([date]))
            reader = objDBHelper.ExecuteReader(cmdCommand)
            If reader IsNot Nothing AndAlso reader.HasRows Then
                While (reader.Read())
                    displayEvent = New DisplayEventInfo
                    displayEvent.AccountName = CheckString(reader("AccountName"))
                    displayEvent.Data = CheckString(reader("Data"))
                    displayEvent.Label = CheckString(reader("Label"))
                    displayEvent.EventTypeID = CheckString(reader("EventTypeID"))
                    If Not reader("DueDate") Is Nothing AndAlso Not IsDBNull(reader("DueDate")) Then
                        aDate = CType(reader("DueDate"), DateTime)
                        displayEvent.Time = aDate.ToString(timeFormat)
                    End If
                    result.Add(displayEvent)
                End While
            End If
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try
        Return result
    End Function

    '<WebMethod()> _
    'Public Function Sync(ByVal strSupplierId As String, ByVal strUserId As String, ByVal dtLastUpdtTime As DateTime) As EventReadListResponse
    '    Dim objResponse As New EventReadListResponse
    '    Try
    '        Dim objEventInfo As EventInfo()
    '        Dim cmdCommand As New SqlCommand("usp_event_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)

    '        objEventInfo = ReadEvents(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objEventInfo Is Nothing AndAlso objEventInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.Events = objEventInfo
    '        Else
    '            objResponse.Status = False
    '            ReDim objResponse.Errors(0)
    '            objResponse.Errors(0) = String.Format("No records retrieved for Supplier ID: {0} User ID: {1} later than Date: {2}", strSupplierId, strUserId, dtLastUpdtTime)
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As EventReadListResponse
        Dim objResponse As New EventReadListResponse
        Try
            Dim objEventInfo As EventInfo()
            Dim cmdCommand As New SqlCommand("usp_event_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)

            objEventInfo = ReadEvents(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventInfo Is Nothing AndAlso objEventInfo.GetUpperBound(0) >= 0 Then
                objResponse.Events = objEventInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstEvents As List(Of EventInfo)) As EventSync3Response
        If _Log.IsDebugEnabled Then _Log.Debug("Entered Events.Sync3 Method...")

        Dim objResponse As New EventSync3Response
        Dim objTempResponse As New EventReadListResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("SupplierID: " & strSupplierId & " // UserID: " & strUserId & " // Version: " & intVersion)
            If _Log.IsDebugEnabled And lstEvents IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstEvents))

            objTempResponse = Sync2(strSupplierId, strUserId, intVersion)
            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Sync2: {0} ", objResponse.Status))

            If _Log.IsDebugEnabled Then _Log.Debug("Starting Sync3 Updates...")

            Dim objModifyResponse As BaseResponse
            If Not lstEvents Is Nothing Then
                For Each objEvent As EventInfo In lstEvents
                    If Not objEvent Is Nothing Then
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Input to Modify: {0} ", SerializationManager.Serialize(objEvent)))
                        objModifyResponse = Modify(objEvent)
                        ProcessResponse(objModifyResponse, objTempResponse)
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Modify: {0} ", SerializationManager.Serialize(objModifyResponse)))
                    End If
                Next
            End If

            If _Log.IsDebugEnabled Then _Log.Debug("Sync3 Updates completed...")

            objResponse.Events = objTempResponse.Events
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Events)
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
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    Private Function ReadEvents(ByVal objReader As SqlDataReader) As EventInfo()
        Dim objEvents As EventInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                Dim aDate As DateTime = DateTime.MinValue
                While (objReader.Read())
                    ReDim Preserve objEvents(intCounter)
                    objEvents(intCounter) = New EventInfo
                    With objEvents(intCounter)
                        .EventID = CheckString(objReader("EventID"))
                        .SupplierID = CheckString(objReader("SupplierID"))

                        .EventTypeID = CheckString(objReader("EventTypeID"))
                        .FollowOnEvent = CheckString(objReader("FollowOnEvent"))
                        .Data = CheckString(objReader("Data"))
                        If Not objReader("Enddate") Is Nothing AndAlso Not IsDBNull(objReader("Enddate")) Then
                            aDate = CType(objReader("Enddate"), DateTime)
                            .EndDate = ConvertDate(aDate)
                        End If
                        If Not objReader("DueDate") Is Nothing AndAlso Not IsDBNull(objReader("DueDate")) Then
                            aDate = CType(objReader("DueDate"), DateTime)
                            .DueDate = ConvertDate(aDate)
                        End If
                        .UserID = CheckString(objReader("UserID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .Status = CheckString(objReader("Status"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objEvents
    End Function

End Class