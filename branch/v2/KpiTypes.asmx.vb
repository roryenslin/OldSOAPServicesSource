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
Public Class KpiTypes
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objKpiTypeInfo As KpiTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpitype_add")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiTypeInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objKpiTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Size", objKpiTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@SortOrder", objKpiTypeInfo.SortOrder)
            cmdCommand.Parameters.AddWithValue("@Label", objKpiTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objKpiTypeInfo.DefaultData)

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
    Public Function Change(ByVal objKpiTypeInfo As KpiTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpitype_change")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiTypeInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objKpiTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Size", objKpiTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@SortOrder", objKpiTypeInfo.SortOrder)
            cmdCommand.Parameters.AddWithValue("@Label", objKpiTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objKpiTypeInfo.DefaultData)

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
    Public Function Modify(ByVal objKpiTypeInfo As KpiTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpitype_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiTypeInfo.KpiTypeID)
            cmdCommand.Parameters.AddWithValue("@FieldType", objKpiTypeInfo.FieldType)
            cmdCommand.Parameters.AddWithValue("@Size", objKpiTypeInfo.Size)
            cmdCommand.Parameters.AddWithValue("@SortOrder", objKpiTypeInfo.SortOrder)
            cmdCommand.Parameters.AddWithValue("@Label", objKpiTypeInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objKpiTypeInfo.DefaultData)

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
    Public Function Delete(ByVal objKpiTypeInfo As KpiTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpitype_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiTypeInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiTypeInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiTypeInfo.KpiTypeID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strKpiGroupId As String, ByVal strKpiTypeId As String) As KpiTypeReadSingleResponse
        Dim objResponse As New KpiTypeReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_kpitype_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", strKpiGroupId)
            cmdCommand.Parameters.AddWithValue("@KpiTypeID", strKpiTypeId)
            Dim objKpiTypes As KpiTypeInfo() = Nothing
            objKpiTypes = ReadKpiTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiTypes Is Nothing AndAlso objKpiTypes.GetUpperBound(0) >= 0 Then
                objResponse.KpiType = objKpiTypes(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As KpiTypeReadListResponse
        Dim objResponse As New KpiTypeReadListResponse
        Try
            Dim objKpiTypeInfo As KpiTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_kpitype_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objKpiTypeInfo = ReadKpiTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiTypeInfo Is Nothing AndAlso objKpiTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiTypes = objKpiTypeInfo
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
    Public Function ReadListPerKpiGroupID(ByVal strSupplierId As String, ByVal strKpiGroupID As String) As KpiTypeReadListResponse
        Dim objResponse As New KpiTypeReadListResponse
        Try
            Dim objKpiTypeInfo As KpiTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_kpitype_readlistkpigroupid")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", strKpiGroupID)
            objResponse.Status = True
            objKpiTypeInfo = ReadKpiTypes(objDBHelper.ExecuteReader(cmdCommand))
            If Not objKpiTypeInfo Is Nothing AndAlso objKpiTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiTypes = objKpiTypeInfo
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As KpiTypeReadListResponse
    '    Dim objResponse As New KpiTypeReadListResponse
    '    Try
    '        Dim objKpiTypeInfo As KpiTypeInfo()
    '        Dim cmdCommand As New SqlCommand("usp_kpitype_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objKpiTypeInfo = ReadKpiTypes(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objKpiTypeInfo Is Nothing AndAlso objKpiTypeInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.KpiTypes = objKpiTypeInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As KpiTypeReadListResponse
        Dim objResponse As New KpiTypeReadListResponse
        Try
            Dim objKpiTypeInfo As KpiTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_kpitype_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objKpiTypeInfo = ReadKpiTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiTypeInfo Is Nothing AndAlso objKpiTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiTypes = objKpiTypeInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstKpiTypes As List(Of KpiTypeInfo)) As KpiTypeSync3Response
        Dim objResponse As New KpiTypeSync3Response
        Dim objTempResponse As New KpiTypeReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstKpiTypes Is Nothing Then
                For Each objKpiType As KpiTypeInfo In lstKpiTypes
                    If Not objKpiType Is Nothing Then
                        ProcessResponse(Modify(objKpiType), objTempResponse)
                    End If
                Next
            End If

            objResponse.KpiTypes = objTempResponse.KpiTypes
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.KpiTypes)
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

    Private Function ReadKpiTypes(ByVal objReader As SqlDataReader) As KpiTypeInfo()
        Dim objKpiTypes As KpiTypeInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objKpiTypes(intCounter)
                    objKpiTypes(intCounter) = New KpiTypeInfo
                    With objKpiTypes(intCounter)
                        .SupplierId = CheckString(objReader("SupplierID"))
                        .KpiGroupID = CheckString(objReader("KpiGroupID"))
                        .KpiTypeID = CheckString(objReader("KpiTypeID"))
                        .FieldType = CheckInteger(objReader("FieldType"))
                        .Size = CheckInteger(objReader("Size"))
                        .SortOrder = CheckInteger(objReader("SortOrder"))
                        .Label = CheckString(objReader("Label"))
                        .DefaultData = CheckString(objReader("DefaultData"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objKpiTypes
    End Function

End Class