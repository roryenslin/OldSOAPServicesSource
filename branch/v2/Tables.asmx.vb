Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Response
Imports RapidTradeWebService.Entity

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Tables
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Tables))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function GetTableVersion(ByVal strTableName As String) As TableVersionResponse
        Dim objResponse As New TableVersionResponse
        Dim objReader As SqlDataReader = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim cmdCommand As New SqlCommand("usp_table_getversion")
            cmdCommand.Parameters.AddWithValue("@TableName", strTableName)

            objReader = objDBHelper.ExecuteReader(cmdCommand)
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                objReader.Read()
                If Not objReader(0) Is Nothing AndAlso IsNumeric(objReader(0)) Then
                    objResponse.TableVersion = CInt(objReader(0))
                    objResponse.Status = True
                Else
                    objResponse.Status = False
                    ReDim Preserve objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("No version information available for the table: {0}", strTableName)
                End If
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strTableName, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not objReader Is Nothing Then
                objReader.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function GetTableVersions(ByVal strTableNames() As String) As TableVersionsResponse
        Dim objResponse As New TableVersionsResponse
        Dim objReader As SqlDataReader = Nothing
        Dim intVersions() As Integer
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        If _Log.IsDebugEnabled Then _Log.Debug("Tables: " & strTableNames.ToString)

        Try
            Dim intCounter As Integer = 0
            If Not strTableNames Is Nothing AndAlso strTableNames.GetUpperBound(0) >= 0 Then
                ReDim intVersions(strTableNames.GetUpperBound(0))
                For intCounter = 0 To strTableNames.GetUpperBound(0)
                    Try
                        Dim cmdCommand As New SqlCommand("usp_table_getversion")
                        cmdCommand.Parameters.AddWithValue("@TableName", strTableNames(intCounter))

                        objReader = objDBHelper.ExecuteReader(cmdCommand)
                        If Not objReader Is Nothing AndAlso objReader.HasRows Then
                            objReader.Read()
                            If Not objReader(0) Is Nothing AndAlso IsNumeric(objReader(0)) Then
                                intVersions(intCounter) = CInt(objReader(0))
                            End If
                        End If
                        objReader.Close()
                    Catch
                    End Try
                Next

                objResponse.Status = True
                If Not intVersions Is Nothing AndAlso intVersions.GetUpperBound(0) >= 0 Then
                    objResponse.TableVersions = intVersions
                End If
            Else
                objResponse.Status = False
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = String.Format("Input string array is empty.")
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strTableNames.ToString, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
            If _Log.IsErrorEnabled Then _Log.Debug("Error in getTableversions: " & ex.Message)
        Finally
            If Not objReader Is Nothing AndAlso Not objReader.IsClosed Then
                objReader.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Test(ByVal strUserID As String, ByVal strTableName As String, ByVal version As Integer) As List(Of TableSyncResponse)
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim tu As New TableSyncInfo
        tu.TableName = strTableName
        tu.LastVersion = version.ToString
        tu.UserId = strUserID
        Dim lst As New List(Of TableSyncInfo)
        lst.Add(tu)
        Dim br As List(Of TableSyncResponse) = CheckTableVersions3(lst)

        Return br
    End Function

    <WebMethod()> _
    Public Function CheckTableVersions(ByVal listTableVersions As List(Of TableUpdateInfo)) As TableUpdateResponse
        Dim objResponse As New TableUpdateResponse
        Dim objConnection As SqlConnection = Nothing
        Dim tempVersions() As TableUpdateInfo
        Try
            If _Log.IsDebugEnabled Then _Log.Info("-------------------------------->" & vbCrLf & RapidTradeWebService.Common.SerializationManager.Serialize(listTableVersions))

            Dim intCounter As Integer = 0
            If Not listTableVersions Is Nothing AndAlso listTableVersions.Count > 0 Then
                ReDim tempVersions(listTableVersions.Count - 1)
                objConnection = objDBHelper.GetConnection()
                objConnection.Open()
                Dim objResult As Object
                Dim intVersion As Integer
                Dim intInputVersion As Integer

                For intCounter = 0 To listTableVersions.Count - 1
                    Try
                        Dim cmdCommand As New SqlCommand("usp_table_getversion")
                        cmdCommand.Parameters.AddWithValue("@TableName", listTableVersions(intCounter).TableName)
                        tempVersions(intCounter) = New TableUpdateInfo
                        tempVersions(intCounter).TableName = listTableVersions(intCounter).TableName

                        objResult = objDBHelper.ExecuteScalar(cmdCommand, objConnection)
                        If Not objResult Is Nothing Then
                            Integer.TryParse(objResult.ToString(), intVersion)
                            Integer.TryParse(listTableVersions(intCounter).LastVersion, intInputVersion)
                            tempVersions(intCounter).LastVersion = intVersion.ToString()

                            If (intVersion > intInputVersion) Then
                                tempVersions(intCounter).MustUpdate = True
                            End If
                        End If
                    Catch
                    End Try
                Next

                objResponse.Status = True
                objResponse.TableVersions = tempVersions
            Else
                objResponse.Status = False
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = String.Format("Input list is empty.")
            End If
        Catch ex As Exception
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter += 1
            End While
        Finally
            If Not objConnection Is Nothing AndAlso Not objConnection.State = ConnectionState.Closed Then
                objConnection.Close()
            End If
        End Try
        If _Log.IsDebugEnabled Then _Log.Info("----------------------Result---------->" & vbCrLf & RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function CheckTableVersions3(ByVal listSyncInfo As List(Of TableSyncInfo)) As List(Of TableSyncResponse)
        Dim objResponse As New List(Of TableSyncResponse)
        Dim objResponseItem As TableSyncResponse = Nothing

        Dim dConfig As New Dictionary(Of String, String)
        dConfig = GetTableVersionsConfig()

        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(listSyncInfo))

        Dim objConnection As SqlConnection = Nothing
        Try
            Dim intCounter As Integer = 0
            If Not listSyncInfo Is Nothing AndAlso listSyncInfo.Count > 0 Then

                Dim intResult As Integer
                Dim oReturnParam As SqlParameter

                For intCounter = 0 To listSyncInfo.Count - 1
                    If Not listSyncInfo(intCounter) Is Nothing AndAlso _
                        Not String.IsNullOrEmpty(listSyncInfo(intCounter).TableName) AndAlso _
                        dConfig.ContainsKey(listSyncInfo(intCounter).TableName.ToLower()) Then

                        Dim gotopoint = "Start" & Now.ToString

                        Try
                            objResponseItem = New TableSyncResponse
                            Dim cmdCommand As New SqlCommand(dConfig(listSyncInfo(intCounter).TableName.ToLower()))
                            cmdCommand.Parameters.AddWithValue("@SupplierId", listSyncInfo(intCounter).SupplierId)
                            cmdCommand.Parameters.AddWithValue("@Version", listSyncInfo(intCounter).LastVersion)
                            If Not String.IsNullOrEmpty(listSyncInfo(intCounter).UserId) Then
                                cmdCommand.Parameters.AddWithValue("@UserId", listSyncInfo(intCounter).UserId)
                            End If
                            gotopoint &= ":Parameters" & Now.ToString
                            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                            oReturnParam.Direction = ParameterDirection.ReturnValue
                            gotopoint &= ":Execute" & Now.ToString
                            objDBHelper.ExecuteNonQuery(cmdCommand)
                            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
                            gotopoint &= ":Build response" & Now.ToString
                            objResponseItem.TableSync.MustUpdate = intResult > 0
                            objResponseItem.TableSync.SupplierId = listSyncInfo(intCounter).SupplierId
                            objResponseItem.TableSync.UserId = listSyncInfo(intCounter).UserId
                            objResponseItem.TableSync.LastVersion = listSyncInfo(intCounter).LastVersion
                            objResponseItem.TableSync.TableName = listSyncInfo(intCounter).TableName
                            objResponseItem.Status = True

                        Catch ex As Exception
                            gotopoint &= ":exception" & Now.ToString
                            objResponseItem.Status = False
                            Dim intCount As Integer = 0
                            While Not ex Is Nothing
                                ReDim Preserve objResponseItem.Errors(intCount)
                                objResponseItem.Errors(intCount) = ex.Message
                                ex = ex.InnerException
                                intCount += 1
                            End While
                            If _Log.IsErrorEnabled Then _Log.Error(listSyncInfo(intCounter).UserId & ":" & listSyncInfo(intCounter).TableName & ":" & listSyncInfo(intCounter).LastVersion & gotopoint, ex)
                        End Try
                    Else
                        objResponseItem = New TableSyncResponse
                        objResponseItem.Status = False
                        ReDim Preserve objResponseItem.Errors(1)
                        objResponseItem.Errors(0) = String.Format("Either input list is empty or the table name is not configured in table TableVersionsConfig.")
                        If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
                    End If

                    objResponse.Add(objResponseItem)
                Next
            End If
        Catch ex As Exception
            objResponse.Clear()
            objResponse.Add(New TableSyncResponse())
            objResponse(0).Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse(0).Errors(intCounter)
                objResponse(0).Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter += 1
            End While
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse), ex)
        Finally
            If Not objConnection Is Nothing AndAlso Not objConnection.State = ConnectionState.Closed Then
                objConnection.Close()
            End If
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("Result----------------->" & vbCrLf & RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    Private Function GetTableVersionsConfig() As Dictionary(Of String, String)
        Dim objresponse As New Dictionary(Of String, String)
        Dim objReader As SqlDataReader = Nothing

        Try
            Dim cmdCommand As New SqlCommand("usp_tableversions_readconfig")

            objReader = objDBHelper.ExecuteReader(cmdCommand, True)

            While objReader.Read()
                objresponse.Add(objReader("TableName").ToString().ToLower(), objReader("NeedSyncSPName").ToString())
            End While
        Finally
            If Not objReader Is Nothing Then
                objReader.Close()
            End If
        End Try
        Return objresponse
    End Function

End Class