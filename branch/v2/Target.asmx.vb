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
Public Class Target
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Target))
    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objTargetInfo As TargetInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_target_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objTargetInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objTargetInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objTargetInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objTargetInfo.PeriodKey)
            cmdCommand.Parameters.AddWithValue("@PeriodDay", objTargetInfo.PeriodDay)
            cmdCommand.Parameters.AddWithValue("@PeriodWeek", objTargetInfo.PeriodWeek)
            cmdCommand.Parameters.AddWithValue("@PeriodMonth", objTargetInfo.PeriodMonth)
            cmdCommand.Parameters.AddWithValue("@PeriodQuarter", objTargetInfo.PeriodQuarter)
            cmdCommand.Parameters.AddWithValue("@PeriodYear", objTargetInfo.PeriodYear)
            cmdCommand.Parameters.AddWithValue("@Data", Decimal.Parse(objTargetInfo.Data))

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
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objTargetInfo), ex)
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
    Public Function Delete(ByVal objTargetInfo As TargetInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_target_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objTargetInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objTargetInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objTargetInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@PeriodKey", objTargetInfo.PeriodKey)

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
    Public Function ReadList(ByVal strSupplierId As String) As TargetReadListResponse
        Dim objResponse As New TargetReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objTargetInfo As TargetInfo()
            Dim cmdCommand As New SqlCommand("usp_target_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objTargetInfo = ReadTargets(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objTargetInfo Is Nothing AndAlso objTargetInfo.GetUpperBound(0) >= 0 Then
                objResponse.Targets = objTargetInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As TargetReadListResponse
        Dim objResponse As New TargetReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objTargetInfo As TargetInfo()
            Dim cmdCommand As New SqlCommand("usp_target_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objTargetInfo = ReadTargets(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objTargetInfo Is Nothing AndAlso objTargetInfo.GetUpperBound(0) >= 0 Then
                objResponse.Targets = objTargetInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstTargets As List(Of TargetInfo)) As TargetSync3Response
        Dim objResponse As New TargetSync3Response
        Dim objTempResponse As New TargetReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstTargets Is Nothing Then
                For Each objTarget As TargetInfo In lstTargets
                    If Not objTarget Is Nothing Then
                        ProcessResponse(Modify(objTarget), objTempResponse)
                    End If
                Next
            End If

            objResponse.Targets = objTempResponse.Targets
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Target)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
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

    Private Function ReadTargets(ByVal objReader As SqlDataReader) As TargetInfo()
        Dim objTargets As TargetInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objTargets(intCounter)
                    objTargets(intCounter) = New TargetInfo
                    With objTargets(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .UserID = CheckString(objReader("UserID"))
                        .TargetTypeID = CheckString(objReader("TargetTypeID"))
                        .PeriodKey = CheckString(objReader("PeriodKey"))
                        .PeriodDay = CheckString(objReader("PeriodDay"))
                        .PeriodWeek = CheckString(objReader("PeriodWeek"))
                        .PeriodMonth = CheckString(objReader("PeriodMonth"))
                        .PeriodQuarter = CheckString(objReader("PeriodQuarter"))
                        .PeriodYear = CheckString(objReader("PeriodYear"))
                        .Data = CheckDecimal(objReader("Data")).ToString()
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objTargets
    End Function

End Class