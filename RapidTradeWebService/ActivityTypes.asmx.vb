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
Public Class ActivityTypes
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ActivityTypes))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objActivityTypeInfo As ActivityTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_eventtype_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objActivityTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", objActivityTypeInfo.ActivityTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowonEventTypeID", objActivityTypeInfo.FollowonActivityTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objActivityTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objActivityTypeInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objActivityTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Label", objActivityTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DueDateAllowed", objActivityTypeInfo.DueDateAllowed)
            cmdCommand.Parameters.AddWithValue("@DueTimeAllowed", objActivityTypeInfo.DueTimeAllowed)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objActivityTypeInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@Size", objActivityTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objActivityTypeInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccntID", objActivityTypeInfo.ForAccntID)
            cmdCommand.Parameters.AddWithValue("@KPITypeID", objActivityTypeInfo.KPITypeID)
            cmdCommand.Parameters.AddWithValue("@KPIVersionID", objActivityTypeInfo.KPIVersionID)
            cmdCommand.Parameters.AddWithValue("@SendToCalendar", objActivityTypeInfo.SendToCalendar)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objActivityTypeInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@KPIAddData", objActivityTypeInfo.KPIAddData)
            cmdCommand.Parameters.AddWithValue("@EventGroup", objActivityTypeInfo.EventGroup)
            cmdCommand.Parameters.AddWithValue("@AllowNote", objActivityTypeInfo.AllowNote)
            cmdCommand.Parameters.AddWithValue("@AllowPicture", objActivityTypeInfo.AllowPicture)
            cmdCommand.Parameters.AddWithValue("@AllowContact", objActivityTypeInfo.AllowContact)
            cmdCommand.Parameters.AddWithValue("@AllowGPS", objActivityTypeInfo.AllowGPS)
            cmdCommand.Parameters.AddWithValue("@Deleted", objActivityTypeInfo.Deleted)

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
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function Delete(ByVal objActivityTypeInfo As ActivityTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_eventtype_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objActivityTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", objActivityTypeInfo.ActivityTypeID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strActivityTypeId As String) As ActivityTypeReadSingleResponse
        Dim objResponse As New ActivityTypeReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_eventtype_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", strActivityTypeId)
            Dim objActivityTypes As ActivityTypeInfo() = Nothing
            objActivityTypes = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypes Is Nothing AndAlso objActivityTypes.GetUpperBound(0) >= 0 Then
                objResponse.ActivityType = objActivityTypes(0)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function ReadList(ByVal strSupplierId As String) As ActivityTypeReadListResponse
        Dim objResponse As New ActivityTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityTypeInfo As ActivityTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.ActivityTypes = objActivityTypeInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function ReadList2(ByVal strSupplierId As String, ByVal strUserId As String) As ActivityTypeReadListResponse
        Dim objResponse As New ActivityTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityTypeInfo As ActivityTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readlistforuser")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.ActivityTypes = objActivityTypeInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function ReadMyList(ByVal strSupplierId As String, ByVal strUserId As String) As ActivityTypeReadListResponse
        Dim objResponse As New ActivityTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityTypeInfo As ActivityTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readmylist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.ActivityTypes = objActivityTypeInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function ReadListAccount(ByVal strSupplierId As String, ByVal strAccountID As String, ByVal strAccountGrp As String) As ActivityTypeReadListResponse
        Dim objResponse As New ActivityTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objActivityTypeInfo As ActivityTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readlistaccount")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", strAccountGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccntID", strAccountID)

            objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.ActivityTypes = objActivityTypeInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As ActivityTypeReadListResponse
    '    Dim objResponse As New ActivityTypeReadListResponse
    '    Try
    '        Dim objActivityTypeInfo As ActivityTypeInfo()
    '        Dim cmdCommand As New SqlCommand("usp_eventtype_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.ActivityTypes = objActivityTypeInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ActivityTypeReadListResponse
        Dim objResponse As New ActivityTypeReadListResponse
        Try
            Dim objActivityTypeInfo As ActivityTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.ActivityTypes = objActivityTypeInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstActivityTypes As List(Of ActivityTypeInfo)) As ActivityTypeSync3Response
        Dim objResponse As New ActivityTypeSync3Response
        Dim objTempResponse As New ActivityTypeReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")

            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstActivityTypes Is Nothing Then
                For Each objActivityType As ActivityTypeInfo In lstActivityTypes
                    If Not objActivityType Is Nothing Then
                        ProcessResponse(Modify(objActivityType), objTempResponse)
                    End If
                Next
            End If

            objResponse.ActivityTypes = objTempResponse.ActivityTypes
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.EventTypes)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for UserID: ", ex)
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

    <WebMethod()> _
    Public Function Test(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ActivityTypeSync4Response
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim br As ActivityTypeSync4Response = Sync4(strSupplierId, strUserId, intVersion, Nothing)
        Dim rs As New Generic.List(Of String)
        Return br
    End Function

    ''' <summary>
    ''' Syncronised eventtypes for a user
    ''' </summary>
    ''' <param name="strSupplierId">Enter a valid supplierid</param>
    ''' <param name="strUserId">The userid. If this is left blank then all valid activitytypes are returned</param>
    ''' <param name="intVersion">The last downloaded version</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Function Sync4(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstActivityTypes As List(Of ActivityTypeInfo)) As ActivityTypeSync4Response
        Dim objResponse As New ActivityTypeSync4Response
        Dim objTempResponse As New ActivityTypeReadListResponse

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strUserId & " // Version: " & intVersion)

            Dim objActivityTypeInfo As ActivityTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_sync4")
            If String.IsNullOrEmpty(strUserId) Then cmdCommand.CommandText = "usp_eventtype_sync2"
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            If Not String.IsNullOrEmpty(strUserId) Then cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objActivityTypeInfo = ReadActivityTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objActivityTypeInfo Is Nothing AndAlso objActivityTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.ActivityTypes = objActivityTypeInfo
            End If

            If Not lstActivityTypes Is Nothing Then
                Dim wentin As Boolean = False
                For Each objActivityType As ActivityTypeInfo In lstActivityTypes
                    If Not objActivityType Is Nothing Then
                        ProcessResponse(Modify(objActivityType), objTempResponse)
                        wentin = True
                    End If
                Next
                If wentin Then
                    objResponse.Errors = objTempResponse.Errors
                    objResponse.Status = objTempResponse.Status
                End If
            End If

            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.EventTypes)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for UserID: " & strUserId, ex)
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

    Private Function ReadActivityTypes(ByVal objReader As SqlDataReader) As ActivityTypeInfo()
        Dim objActivityTypes As ActivityTypeInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objActivityTypes(intCounter)
                    objActivityTypes(intCounter) = New ActivityTypeInfo
                    With objActivityTypes(intCounter)
                        .SupplierId = CheckString(objReader("SupplierID"))
                        .ActivityTypeID = CheckString(objReader("EventID"))
                        .FollowonActivityTypeID = CheckString(objReader("FollowonEventTypeID"))
                        .KpiGroupID = CheckString(objReader("KpiGroupID"))
                        .TargetTypeID = CheckString(objReader("TargetTypeID"))
                        .DefaultData = CheckString(objReader("DefaultData"))
                        .DueDateAllowed = CheckBoolean(objReader("DueDateAllowed"))
                        .DueTimeAllowed = CheckBoolean(objReader("DueTimeAllowed"))
                        .FieldType = CheckInteger(objReader("FieldType"))
                        .ForAccntGrp = CheckString(objReader("ForAccntGrp"))
                        .ForAccntID = CheckString(objReader("ForAccntID"))
                        .KPITypeID = CheckString(objReader("KPITypeID"))
                        .KPIVersionID = CheckString(objReader("KPIVersionID"))
                        .Label = CheckString(objReader("Label"))
                        .SendToCalendar = CheckBoolean(objReader("SendToCalendar"))
                        .LongDescription = CheckString(objReader("LongDescription"))
                        .Size = CheckInteger(objReader("Size"))
                        .Deleted = CheckDeletedField(objReader)
                        .KPIAddData = CheckBoolean(objReader("KPIAddData"))
                        .EventGroup = CheckString(objReader("EventGroup"))
                        .AllowNote = CheckBoolean(objReader("AllowNote"))
                        .AllowPicture = CheckBoolean(objReader("AllowPicture"))
                        .AllowGPS = CheckBoolean(objReader("AllowGPS"))
                        .AllowContact = CheckBoolean(objReader("AllowContact"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objActivityTypes
    End Function

End Class