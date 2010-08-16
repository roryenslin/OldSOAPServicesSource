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
Public Class EventTypes
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(EventTypes))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objEventTypeInfo As EventTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_eventtype_add2")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", objEventTypeInfo.EventTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowonEventTypeID", objEventTypeInfo.FollowonEventTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objEventTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objEventTypeInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objEventTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Label", objEventTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DueDateAllowed", objEventTypeInfo.DueDateAllowed)
            cmdCommand.Parameters.AddWithValue("@DueTimeAllowed", objEventTypeInfo.DueTimeAllowed)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objEventTypeInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@Size", objEventTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objEventTypeInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccntID", objEventTypeInfo.ForAccntID)
            cmdCommand.Parameters.AddWithValue("@KPITypeID", objEventTypeInfo.KPITypeID)
            cmdCommand.Parameters.AddWithValue("@KPIVersionID", objEventTypeInfo.KPIVersionID)
            cmdCommand.Parameters.AddWithValue("@SendToCalendar", objEventTypeInfo.SendToCalendar)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objEventTypeInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@KPIAddData", objEventTypeInfo.KPIAddData)
            cmdCommand.Parameters.AddWithValue("@EventGroup", objEventTypeInfo.EventGroup)
            cmdCommand.Parameters.AddWithValue("@AllowNote", objEventTypeInfo.AllowNote)

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
    Public Function Change(ByVal objEventTypeInfo As EventTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_eventtype_change2")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", objEventTypeInfo.EventTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowonEventTypeID", objEventTypeInfo.FollowonEventTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objEventTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objEventTypeInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objEventTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Label", objEventTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DueDateAllowed", objEventTypeInfo.DueDateAllowed)
            cmdCommand.Parameters.AddWithValue("@DueTimeAllowed", objEventTypeInfo.DueTimeAllowed)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objEventTypeInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@Size", objEventTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objEventTypeInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccntID", objEventTypeInfo.ForAccntID)
            cmdCommand.Parameters.AddWithValue("@KPITypeID", objEventTypeInfo.KPITypeID)
            cmdCommand.Parameters.AddWithValue("@KPIVersionID", objEventTypeInfo.KPIVersionID)
            cmdCommand.Parameters.AddWithValue("@SendToCalendar", objEventTypeInfo.SendToCalendar)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objEventTypeInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@KPIAddData", objEventTypeInfo.KPIAddData)
            cmdCommand.Parameters.AddWithValue("@EventGroup", objEventTypeInfo.EventGroup)
            cmdCommand.Parameters.AddWithValue("@AllowNote", objEventTypeInfo.AllowNote)

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
    Public Function Modify(ByVal objEventTypeInfo As EventTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_eventtype_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", objEventTypeInfo.EventTypeID)
            cmdCommand.Parameters.AddWithValue("@FollowonEventTypeID", objEventTypeInfo.FollowonEventTypeID)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objEventTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objEventTypeInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objEventTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Label", objEventTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DueDateAllowed", objEventTypeInfo.DueDateAllowed)
            cmdCommand.Parameters.AddWithValue("@DueTimeAllowed", objEventTypeInfo.DueTimeAllowed)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objEventTypeInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@Size", objEventTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objEventTypeInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccntID", objEventTypeInfo.ForAccntID)
            cmdCommand.Parameters.AddWithValue("@KPITypeID", objEventTypeInfo.KPITypeID)
            cmdCommand.Parameters.AddWithValue("@KPIVersionID", objEventTypeInfo.KPIVersionID)
            cmdCommand.Parameters.AddWithValue("@SendToCalendar", objEventTypeInfo.SendToCalendar)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objEventTypeInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@KPIAddData", objEventTypeInfo.KPIAddData)
            cmdCommand.Parameters.AddWithValue("@EventGroup", objEventTypeInfo.EventGroup)
            cmdCommand.Parameters.AddWithValue("@AllowNote", objEventTypeInfo.AllowNote)

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
    Public Function Delete(ByVal objEventTypeInfo As EventTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_eventtype_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objEventTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", objEventTypeInfo.EventTypeID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strEventTypeId As String) As EventTypeReadSingleResponse
        Dim objResponse As New EventTypeReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_eventtype_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@EventID", strEventTypeId)
            Dim objEventTypes As EventTypeInfo() = Nothing
            objEventTypes = ReadEventTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventTypes Is Nothing AndAlso objEventTypes.GetUpperBound(0) >= 0 Then
                objResponse.EventType = objEventTypes(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As EventTypeReadListResponse
        Dim objResponse As New EventTypeReadListResponse
        Try
            Dim objEventTypeInfo As EventTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objEventTypeInfo = ReadEventTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventTypeInfo Is Nothing AndAlso objEventTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.EventTypes = objEventTypeInfo
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
    Public Function ReadList2(ByVal strSupplierId As String, ByVal strUserId As String) As EventTypeReadListResponse
        Dim objResponse As New EventTypeReadListResponse
        Try
            Dim objEventTypeInfo As EventTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readlistforuser")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objEventTypeInfo = ReadEventTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventTypeInfo Is Nothing AndAlso objEventTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.EventTypes = objEventTypeInfo
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
    Public Function ReadListAccount(ByVal strSupplierId As String, ByVal strAccountID As String, ByVal strAccountGrp As String) As EventTypeReadListResponse
        Dim objResponse As New EventTypeReadListResponse
        Try
            Dim objEventTypeInfo As EventTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_readlistaccount")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", strAccountGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccntID", strAccountID)

            objEventTypeInfo = ReadEventTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventTypeInfo Is Nothing AndAlso objEventTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.EventTypes = objEventTypeInfo
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As EventTypeReadListResponse
    '    Dim objResponse As New EventTypeReadListResponse
    '    Try
    '        Dim objEventTypeInfo As EventTypeInfo()
    '        Dim cmdCommand As New SqlCommand("usp_eventtype_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objEventTypeInfo = ReadEventTypes(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objEventTypeInfo Is Nothing AndAlso objEventTypeInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.EventTypes = objEventTypeInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As EventTypeReadListResponse
        Dim objResponse As New EventTypeReadListResponse
        Try
            Dim objEventTypeInfo As EventTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_eventtype_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objEventTypeInfo = ReadEventTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objEventTypeInfo Is Nothing AndAlso objEventTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.EventTypes = objEventTypeInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstEventTypes As List(Of EventTypeInfo)) As EventTypeSync3Response
        Dim objResponse As New EventTypeSync3Response
        Dim objTempResponse As New EventTypeReadListResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("SupplierID: " & strSupplierId & " // Version: " & intVersion)
            If _Log.IsDebugEnabled And lstEventTypes IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstEventTypes))

            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstEventTypes Is Nothing Then
                For Each objEventType As EventTypeInfo In lstEventTypes
                    If Not objEventType Is Nothing Then
                        ProcessResponse(Modify(objEventType), objTempResponse)
                    End If
                Next
            End If

            objResponse.EventTypes = objTempResponse.EventTypes
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.EventTypes)
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
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    Private Function ReadEventTypes(ByVal objReader As SqlDataReader) As EventTypeInfo()
        Dim objEventTypes As EventTypeInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objEventTypes(intCounter)
                    objEventTypes(intCounter) = New EventTypeInfo
                    With objEventTypes(intCounter)
                        .SupplierId = CheckString(objReader("SupplierID"))
                        .EventTypeID = CheckString(objReader("EventID"))
                        .FollowonEventTypeID = CheckString(objReader("FollowonEventTypeID"))
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
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objEventTypes
    End Function

End Class