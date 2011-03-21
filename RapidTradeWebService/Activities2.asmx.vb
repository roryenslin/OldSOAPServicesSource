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
Public Class Activities2
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
        _Log.Error("Error converting date from " & str & ". Will use current time instead")

        Return Now
    End Function
    Public Shared Function ConvertDate(ByVal [date] As DateTime) As String
        Return [date].ToString(DATEFORMAT)
    End Function

    <WebMethod()> _
    Public Function Modify(ByVal objActivityInfo2 As ActivityInfo2) As BaseResponse
        Dim objResponse2 As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_modify")
            cmdCommand.Parameters.AddWithValue("@EventID", objActivityInfo2.ActivityID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objActivityInfo2.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objActivityInfo2.ActivityTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowOnEvent", objActivityInfo2.FollowOnActivity)
            cmdCommand.Parameters.AddWithValue("@Data", objActivityInfo2.Data)
            cmdCommand.Parameters.AddWithValue("@EndDate", ConvertDate(objActivityInfo2.EndDate))
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate(objActivityInfo2.DueDate))
            cmdCommand.Parameters.AddWithValue("@UserID", objActivityInfo2.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objActivityInfo2.AccountID)
            cmdCommand.Parameters.AddWithValue("@Status", objActivityInfo2.Status)
            cmdCommand.Parameters.AddWithValue("@ContactID", objActivityInfo2.ContactID)
            cmdCommand.Parameters.AddWithValue("@Latitude", objActivityInfo2.Latitude)
            cmdCommand.Parameters.AddWithValue("@Longitude", objActivityInfo2.Longitude)
            cmdCommand.Parameters.AddWithValue("@Deleted", objActivityInfo2.Deleted)
            cmdCommand.Parameters.AddWithValue("@Notes", objActivityInfo2.Notes)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse2.Status = intResult = 0
            If Not objResponse2.Status Then
                ReDim Preserve objResponse2.Errors(0)
                objResponse2.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objActivityInfo2), ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal objActivityInfo2 As ActivityInfo2) As BaseResponse
        Dim objResponse2 As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_event_delete")
            cmdCommand.Parameters.AddWithValue("@EventID", objActivityInfo2.ActivityID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objActivityInfo2.SupplierID)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", objActivityInfo2.ActivityTypeID)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse2.Status = intResult = 0
            If Not objResponse2.Status Then
                ReDim Preserve objResponse2.Errors(0)
                objResponse2.Errors(0) = "No rows deleted in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function ReadSingle(ByVal strActivityID As String, ByVal strSupplierId As String, ByVal strActivityTypeId As String) As ActivityReadSingleResponse2
        Dim objResponse2 As New ActivityReadSingleResponse2
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_event_readsingle")
            cmdCommand.Parameters.AddWithValue("@EventID", strActivityID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@EventTypeID", strActivityTypeId)

            Dim objActivitys As ActivityInfo2() = Nothing
            objActivitys = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse2.Status = True
            If Not objActivitys Is Nothing AndAlso objActivitys.GetUpperBound(0) >= 0 Then
                objResponse2.ActivityRecord = objActivitys(0)
            End If
        Catch ex As Exception
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As ActivityReadListResponse2
        Dim objResponse2 As New ActivityReadListResponse2
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityInfo2 As ActivityInfo2()
            Dim cmdCommand As New SqlCommand("usp_event_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objActivityInfo2 = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse2.Status = True
            If Not objActivityInfo2 Is Nothing AndAlso objActivityInfo2.GetUpperBound(0) >= 0 Then
                objResponse2.Activitys = objActivityInfo2
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function ReadListForAccountDate(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strAccountId As String, ByVal strFromDate As String, ByVal strToDate As String) As ActivityReadListResponse2
        Dim objResponse2 As New ActivityReadListResponse2
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityInfo2 As ActivityInfo2()
            Dim cmdCommand As New SqlCommand("usp_event_readlistforaccountdate")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@FromDate", strFromDate)
            cmdCommand.Parameters.AddWithValue("@ToDate", strToDate)
            objActivityInfo2 = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse2.Status = True
            If Not objActivityInfo2 Is Nothing AndAlso objActivityInfo2.GetUpperBound(0) >= 0 Then
                objResponse2.Activitys = objActivityInfo2
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function GetActivityDatesForAccount(ByVal strSupplierId As String, ByVal strUserId As String, _
                ByVal strAccountId As String, ByVal strFromDate As String, ByVal strToDate As String) As ActivityDatesResponse2
        Dim objResponse2 As New ActivityDatesResponse2
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info(strSupplierId & "," & strUserId & "," & strAccountId & "," & strFromDate & "," & strToDate)

            Dim objActivityInfo2 As ActivityInfo2()
            Dim cmdCommand As New SqlCommand("usp_event_readlistforaccountdate")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@FromDate", strFromDate)
            cmdCommand.Parameters.AddWithValue("@ToDate", strToDate)
            objActivityInfo2 = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse2.Status = True
            objResponse2.ActivityDates = New List(Of Integer)
            Dim uniqueList As New Collections.Generic.Dictionary(Of Integer, Integer)
            If Not objActivityInfo2 Is Nothing AndAlso objActivityInfo2.GetUpperBound(0) >= 0 Then
                For Each evt As ActivityInfo2 In objActivityInfo2
                    Dim value As Integer = Integer.Parse(evt.DueDate.Substring(0, 8))
                    If Not objResponse2.ActivityDates.Contains(value) Then objResponse2.ActivityDates.Add(value)
                Next
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function ReadDay(ByVal supplierId As String, ByVal userId As String, ByVal accountId As String, ByVal [date] As String, ByVal timeFormat As String) As List(Of DisplayActivityInfo)
        Dim result As New List(Of DisplayActivityInfo)
        Dim reader As SqlDataReader = Nothing
        Dim aDate As DateTime = Nothing
        Dim displayActivity As DisplayActivityInfo = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info(supplierId & "," & userId & "," & accountId & "," & [date] & "," & ConvertDate([date]))

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
                    displayActivity.ContactName = CheckString(reader("Name"))
                    displayActivity.Notes = CheckString(reader("Notes"))

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
        Public Function ReadDay2(ByVal supplierId As String, ByVal userId As String, ByVal accountId As String, ByVal [date] As String, ByVal timeFormat As String) As ActivityReadListResponse2
        Dim objResponse2 As New ActivityReadListResponse2
        Dim reader As SqlDataReader = Nothing
        Dim aDate As DateTime = Nothing
        Dim displayActivity As ActivityInfo2 = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_event_readday")
            cmdCommand.Parameters.AddWithValue("@SupplierID", supplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", userId)
            cmdCommand.Parameters.AddWithValue("@AccountID", accountId)
            cmdCommand.Parameters.AddWithValue("@DueDate", ConvertDate([date]))
            reader = objDBHelper.ExecuteReader(cmdCommand)
            objResponse2.Status = True
            If reader IsNot Nothing AndAlso reader.HasRows Then
                objResponse2.Activitys = ReadActivitys(reader)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & supplierId & userId, ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ActivityReadListResponse2
        Dim objResponse2 As New ActivityReadListResponse2
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityInfo2 As ActivityInfo2()
            Dim cmdCommand As New SqlCommand("usp_event_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)

            objActivityInfo2 = ReadActivitys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse2.Status = True
            If Not objActivityInfo2 Is Nothing AndAlso objActivityInfo2.GetUpperBound(0) >= 0 Then
                objResponse2.Activitys = objActivityInfo2
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse2
    End Function

    <WebMethod()> _
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstActivitys As List(Of ActivityInfo2)) As ActivitySync3Response2
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")

        Dim objResponse2 As New ActivitySync3Response2
        Dim objTempResponse2 As New ActivityReadListResponse2
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("SupplierID: " & strSupplierId & " // UserID: " & strUserId & " // Version: " & intVersion)
            If _Log.IsDebugEnabled And lstActivitys IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstActivitys))

            objTempResponse2 = Sync2(strSupplierId, strUserId, intVersion)
            If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response2 from Sync2: {0} ", objTempResponse2.Status))
            If _Log.IsDebugEnabled Then _Log.Debug("Starting Sync3 Updates...")

            Dim objModifyResponse2 As BaseResponse
            If Not lstActivitys Is Nothing Then
                For Each objActivity As ActivityInfo2 In lstActivitys
                    If Not objActivity Is Nothing Then
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Input to Modify: {0} ", SerializationManager.Serialize(objActivity)))
                        objModifyResponse2 = Modify(objActivity)
                        ProcessResponse(objModifyResponse2, objTempResponse2)
                        If _Log.IsDebugEnabled Then _Log.Debug(String.Format("Response2 from Modify: {0} ", SerializationManager.Serialize(objModifyResponse2)))
                    End If
                Next
            End If

            If _Log.IsDebugEnabled Then _Log.Debug("Sync3 Updates completed...")

            objResponse2.Activitys = objTempResponse2.Activitys
            objResponse2.Errors = objTempResponse2.Errors
            objResponse2.Status = objTempResponse2.Status
            Dim objTableVersionResponse2 As TableVersionResponse = New Tables().GetTableVersion(TableNames.Events)
            If objTableVersionResponse2.Status Then
                objResponse2.LastVersion = objTableVersionResponse2.TableVersion
            Else
                ProcessResponse(objTableVersionResponse2, objResponse2)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
            objResponse2.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse2.Errors(intCounter)
                objResponse2.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse2))
        Return objResponse2
    End Function

    Private Function ReadActivitys(ByVal objReader As SqlDataReader) As ActivityInfo2()
        Dim objActivitys As ActivityInfo2() = Nothing
        Dim intCounter As Integer = 0

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")

            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                Dim aDate As DateTime = DateTime.MinValue
                While (objReader.Read())
                    ReDim Preserve objActivitys(intCounter)
                    objActivitys(intCounter) = New ActivityInfo2
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
                        .Notes = CheckString(objReader("Notes"))

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
  Public Function Test(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ActivitySync3Response2
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim ai As New ActivityInfo2("", strSupplierId, "01", "", "DATA", Now.ToString("yyyyMMdd HH:mm:ss"), Now.ToString("yyyyMMdd HH:mm:ss"), strUserId, strSupplierId, "", "", "", "", "Note")
        Dim lst As New Generic.List(Of ActivityInfo2)
        lst.Add(ai)
        Dim br As ActivitySync3Response2 = Sync3(strSupplierId, strUserId, intVersion, lst)
        Dim rs As New Generic.List(Of String)
        Return br
    End Function
End Class