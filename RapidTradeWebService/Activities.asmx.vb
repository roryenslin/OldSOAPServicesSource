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
Public Class Activities
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Activities))

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
    Public Function Modify(ByVal objActivityInfo As ActivityInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_modify")
            cmdCommand.Parameters.AddWithValue("@EventID", objActivityInfo.ActivityID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objActivityInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objActivityInfo.ActivityTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowOnEvent", objActivityInfo.FollowOnActivity)
            cmdCommand.Parameters.AddWithValue("@Data", objActivityInfo.Data)
            cmdCommand.Parameters.AddWithValue("@EndDate", ConvertDate(objActivityInfo.EndDate))
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate(objActivityInfo.DueDate))
            cmdCommand.Parameters.AddWithValue("@UserID", objActivityInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objActivityInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objActivityInfo.Status)
            cmdCommand.Parameters.AddWithValue("@ContactID", objActivityInfo.ContactID)
            cmdCommand.Parameters.AddWithValue("@Latitude", objActivityInfo.Latitude)
            cmdCommand.Parameters.AddWithValue("@Longitude", objActivityInfo.Longitude)
            cmdCommand.Parameters.AddWithValue("@Deleted", objActivityInfo.Deleted)

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objActivityInfo), ex)
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
    Public Function Delete(ByVal objActivityInfo As ActivityInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_delete")
            cmdCommand.Parameters.AddWithValue("@EventID", objActivityInfo.ActivityID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objActivityInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objActivityInfo.ActivityTypeID)

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
    Public Function ReadSingle(ByVal strActivityID As String, ByVal strSupplierId As String, ByVal strActivityTypeId As String) As ActivityReadSingleResponse
        Dim objResponse As New ActivityReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_event_readsingle")
            cmdCommand.Parameters.AddWithValue("@EventID", strActivityID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", strActivityTypeId)

            Dim objActivitys As ActivityInfo() = Nothing
            objActivitys = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivitys Is Nothing AndAlso objActivitys.GetUpperBound(0) >= 0 Then
                objResponse.ActivityRecord = objActivitys(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As ActivityReadListResponse
        Dim objResponse As New ActivityReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityInfo As ActivityInfo()
            Dim cmdCommand As New SqlCommand("usp_event_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objActivityInfo = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityInfo Is Nothing AndAlso objActivityInfo.GetUpperBound(0) >= 0 Then
                objResponse.Activitys = objActivityInfo
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
    Public Function ReadListForAccountDate(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strAccountId As String, ByVal strFromDate As String, ByVal strToDate As String) As ActivityReadListResponse
        Dim objResponse As New ActivityReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityInfo As ActivityInfo()
            Dim cmdCommand As New SqlCommand("usp_event_readlistforaccountdate")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@FromDate", strFromDate)
            cmdCommand.Parameters.AddWithValue("@ToDate", strToDate)
            objActivityInfo = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityInfo Is Nothing AndAlso objActivityInfo.GetUpperBound(0) >= 0 Then
                objResponse.Activitys = objActivityInfo
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
    Public Function GetActivityDatesForAccount(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strAccountId As String, ByVal strFromDate As String, ByVal strToDate As String) As ActivityDatesResponse
        Dim objResponse As New ActivityDatesResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityInfo As ActivityInfo()
            Dim cmdCommand As New SqlCommand("usp_event_readlistforaccountdate")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@FromDate", strFromDate)
            cmdCommand.Parameters.AddWithValue("@ToDate", strToDate)
            objActivityInfo = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            objResponse.ActivityDates = New List(Of Integer)
            Dim uniqueList As New Collections.Generic.Dictionary(Of Integer, Integer)
            If Not objActivityInfo Is Nothing AndAlso objActivityInfo.GetUpperBound(0) >= 0 Then
                For Each evt As ActivityInfo In objActivityInfo
                    Dim value As Integer = Integer.Parse(evt.DueDate.Substring(0, 8))
                    If Not objResponse.ActivityDates.Contains(value) Then objResponse.ActivityDates.Add(value)
                Next
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
    Public Function ReadDay(ByVal supplierId As String, ByVal userId As String, ByVal accountId As String, ByVal [date] As String, ByVal timeFormat As String) As List(Of DisplayActivityInfo)
        Dim result As New List(Of DisplayActivityInfo)
        Dim reader As SqlDataReader = Nothing
        Dim aDate As DateTime = Nothing
        Dim displayActivity As DisplayActivityInfo = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_event_readday")
            cmdCommand.Parameters.AddWithValue("@SupplierID", supplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", userId)
            cmdCommand.Parameters.AddWithValue("@AccountID", accountId)
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate([date]))
            reader = objDBHelper.ExecuteReader(cmdCommand)
            If reader IsNot Nothing AndAlso reader.HasRows Then
                While (reader.Read())
                    displayActivity = New DisplayActivityInfo
                    displayActivity.AccountName = CheckString(reader("AccountName"))
                    displayActivity.Data = CheckString(reader("Data"))
                    displayActivity.Label = CheckString(reader("Label"))
                    displayActivity.ActivityTypeID = CheckString(reader("EventTypeID"))
                    displayActivity.Longitude = CheckString(reader("Longitude"))
                    displayActivity.Latitude = CheckString(reader("Latitude"))
                    displayActivity.EventID = CheckString(reader("EventID"))
                    displayActivity.ContactID = CheckString(reader("ContactID"))

                    If Not reader("DueDate") Is Nothing AndAlso Not IsDBNull(reader("DueDate")) Then
                        aDate = CType(reader("DueDate"), DateTime)
                        displayActivity.Time = aDate.ToString(timeFormat)
                    End If
                    If Not reader("EndDate") Is Nothing AndAlso Not IsDBNull(reader("EndDate")) Then
                        aDate = CType(reader("EndDate"), DateTime)
                        displayActivity.EndTime = aDate.ToString(timeFormat)
                    End If
                    result.Add(displayActivity)
                End While
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & supplierId & userId, ex)
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try
        Return result
    End Function

    <WebMethod()> _
        Public Function ReadDay2(ByVal supplierId As String, ByVal userId As String, ByVal accountId As String, ByVal [date] As String, ByVal timeFormat As String) As ActivityReadListResponse
        Dim objResponse As New ActivityReadListResponse
        Dim reader As SqlDataReader = Nothing
        Dim aDate As DateTime = Nothing
        Dim displayActivity As ActivityInfo = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_event_readday")
            cmdCommand.Parameters.AddWithValue("@SupplierID", supplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", userId)
            cmdCommand.Parameters.AddWithValue("@AccountID", accountId)
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate([date]))
            reader = objDBHelper.ExecuteReader(cmdCommand)
            objResponse.Status = True
            If reader IsNot Nothing AndAlso reader.HasRows Then
                objResponse.Activitys = ReadActivitys(reader)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & supplierId & userId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ActivityReadListResponse
        Dim objResponse As New ActivityReadListResponse
        Try
            Dim objActivityInfo As ActivityInfo()
            Dim cmdCommand As New SqlCommand("usp_event_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)

            objActivityInfo = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityInfo Is Nothing AndAlso objActivityInfo.GetUpperBound(0) >= 0 Then
                objResponse.Activitys = objActivityInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstActivitys As List(Of ActivityInfo)) As ActivitySync3Response
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")

        Dim objResponse As New ActivitySync3Response
        Dim objTempResponse As New ActivityReadListResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("SupplierID: " & strSupplierId & " // UserID: " & strUserId & " // Version: " & intVersion)
            If _Log.IsDebugEnabled And lstActivitys IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstActivitys))

            objTempResponse = Sync2(strSupplierId, strUserId, intVersion)
            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Sync2: {0} ", objResponse.Status))

            If _Log.IsDebugEnabled Then _Log.Debug("Starting Sync3 Updates...")

            Dim objModifyResponse As BaseResponse
            If Not lstActivitys Is Nothing Then
                For Each objActivity As ActivityInfo In lstActivitys
                    If Not objActivity Is Nothing Then
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Input to Modify: {0} ", SerializationManager.Serialize(objActivity)))
                        objModifyResponse = Modify(objActivity)
                        ProcessResponse(objModifyResponse, objTempResponse)
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response from Modify: {0} ", SerializationManager.Serialize(objModifyResponse)))
                    End If
                Next
            End If

            If _Log.IsDebugEnabled Then _Log.Debug("Sync3 Updates completed...")

            objResponse.Activitys = objTempResponse.Activitys
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Events)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
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
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    Private Function ReadActivitys(ByVal objReader As SqlDataReader) As ActivityInfo()
        Dim objActivitys As ActivityInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                Dim aDate As DateTime = DateTime.MinValue
                While (objReader.Read())
                    ReDim Preserve objActivitys(intCounter)
                    objActivitys(intCounter) = New ActivityInfo
                    With objActivitys(intCounter)
                        .ActivityID = CheckString(objReader("EventID"))
                        .SupplierID = CheckString(objReader("SupplierID"))

                        .ActivityTypeID = CheckString(objReader("EventTypeID"))
                        .FollowOnActivity = CheckString(objReader("FollowOnEvent"))
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
                        .ContactID = CheckString(objReader("ContactID"))
                        .Deleted = CheckDeletedField(objReader)
                        .Latitude = CheckString(objReader("Latitude"))
                        .Longitude = CheckString(objReader("Longitude"))

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objActivitys
    End Function

    <WebMethod()> _
  Public Function Test(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ActivitySync3Response
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim br As ActivitySync3Response = Sync3(strSupplierId, strUserId, intVersion, Nothing)
        Dim rs As New Generic.List(Of String)
        Return br
    End Function
End Class