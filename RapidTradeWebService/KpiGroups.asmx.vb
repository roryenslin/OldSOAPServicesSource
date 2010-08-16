Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Text.RegularExpressions
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
Public Class KpiGroups
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objKpiGroupInfo As KpiGroupInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            Dim iCounter As Integer = 0
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpigroup_add")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objKpiGroupInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiGroupInfo.Description)
            cmdCommand.Parameters.AddWithValue("@PeriodType", CType(objKpiGroupInfo.PeriodType, Decimal))
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objKpiGroupInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccountId", objKpiGroupInfo.ForAccountId)
            cmdCommand.Parameters.AddWithValue("@ForProductgrp", objKpiGroupInfo.ForProductgrp)
            cmdCommand.Parameters.AddWithValue("@ForProductID", objKpiGroupInfo.ForProductID)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objKpiGroupInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@HideByDefault", objKpiGroupInfo.HideByDefault)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            cmdCommand.Transaction = trnTransaction

            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows inserted in database. Error returned" + intResult.ToString()
                Exit Try
            End If

            'Update into Kpiversions
            For iCounter = 0 To objKpiGroupInfo.KpiVersions.GetUpperBound(0)
                cmdCommand.CommandText = "usp_kpiversion_add"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiGroupInfo.KpiVersions(iCounter).SupplierID)
                cmdCommand.Parameters.AddWithValue("@VersionID", objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                cmdCommand.Parameters.AddWithValue("@Description", objKpiGroupInfo.KpiVersions(iCounter).Description)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    ReDim objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("Failed to insert KpiVersion: {0}", objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                    Exit Try
                End If

                'Insert into KpiGroupVersions table
                cmdCommand.CommandText = "usp_kpigroupversions_add"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiGroupInfo.KpiVersions(iCounter).SupplierID)
                cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiGroupID)
                cmdCommand.Parameters.AddWithValue("@KpiVersionId", objKpiGroupInfo.KpiVersions(iCounter).VersionID)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue

                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    objResponse.Status = False
                    ReDim Preserve objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("No KpiGroupVersion inserted in database. KpiGroupID: {0} Version Id: {1}", objKpiGroupInfo.KpiGroupID, objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                    Exit Try
                End If
            Next

            'Insert into KpiTypes table
            For iCounter = 0 To objKpiGroupInfo.KpiTypes.GetUpperBound(0)
                cmdCommand.CommandText = "usp_kpitype_add"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.KpiTypes(iCounter).SupplierId)
                cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiTypes(iCounter).KpiGroupID)
                cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiGroupInfo.KpiTypes(iCounter).KpiTypeID)
                cmdCommand.Parameters.AddWithValue("@FieldType", objKpiGroupInfo.KpiTypes(iCounter).FieldType)
                cmdCommand.Parameters.AddWithValue("@Size", objKpiGroupInfo.KpiTypes(iCounter).Size)
                cmdCommand.Parameters.AddWithValue("@SortOrder", objKpiGroupInfo.KpiTypes(iCounter).SortOrder)
                cmdCommand.Parameters.AddWithValue("@Label", objKpiGroupInfo.KpiTypes(iCounter).Label)
                cmdCommand.Parameters.AddWithValue("@DefaultData", objKpiGroupInfo.KpiTypes(iCounter).DefaultData)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    objResponse.Status = False
                    ReDim objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("Failed to insert KpiType: {0}", objKpiGroupInfo.KpiTypes(iCounter).KpiTypeID)
                    Exit Try
                End If
            Next

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            trnTransaction.Rollback()
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing AndAlso Not conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Change(ByVal objKpiGroupInfo As KpiGroupInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpigroup_change")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objKpiGroupInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiGroupInfo.Description)
            cmdCommand.Parameters.AddWithValue("@PeriodType", CType(objKpiGroupInfo.PeriodType, Decimal))
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objKpiGroupInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccountId", objKpiGroupInfo.ForAccountId)
            cmdCommand.Parameters.AddWithValue("@ForProductgrp", objKpiGroupInfo.ForProductgrp)
            cmdCommand.Parameters.AddWithValue("@ForProductID", objKpiGroupInfo.ForProductID)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objKpiGroupInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@HideByDefault", objKpiGroupInfo.HideByDefault)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            cmdCommand.Transaction = trnTransaction

            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows updated in database. Error returned" + intResult.ToString()
            End If

            'Insert into the KpiVersions table.
            For iCounter = 0 To objKpiGroupInfo.KpiVersions.GetUpperBound(0)
                cmdCommand.CommandText = "usp_kpiversion_change2"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiGroupID)
                cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiGroupInfo.KpiVersions(iCounter).SupplierID)
                cmdCommand.Parameters.AddWithValue("@VersionID", objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                cmdCommand.Parameters.AddWithValue("@Description", objKpiGroupInfo.KpiVersions(iCounter).Description)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    ReDim objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("Failed to update KpiVersion: {0}", objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                    Exit Try
                End If
            Next

            'Update KpiTypes table
            For iCounter = 0 To objKpiGroupInfo.KpiTypes.GetUpperBound(0)
                cmdCommand.CommandText = "usp_kpitype_change2"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.KpiTypes(iCounter).SupplierId)
                cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiTypes(iCounter).KpiGroupID)
                cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiGroupInfo.KpiTypes(iCounter).KpiTypeID)
                cmdCommand.Parameters.AddWithValue("@FieldType", objKpiGroupInfo.KpiTypes(iCounter).FieldType)
                cmdCommand.Parameters.AddWithValue("@Size", objKpiGroupInfo.KpiTypes(iCounter).Size)
                cmdCommand.Parameters.AddWithValue("@SortOrder", objKpiGroupInfo.KpiTypes(iCounter).SortOrder)
                cmdCommand.Parameters.AddWithValue("@Label", objKpiGroupInfo.KpiTypes(iCounter).Label)
                cmdCommand.Parameters.AddWithValue("@DefaultData", objKpiGroupInfo.KpiTypes(iCounter).DefaultData)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    objResponse.Status = False
                    ReDim objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("Failed to update KpiType: {0}", objKpiGroupInfo.KpiTypes(iCounter).KpiTypeID)
                    Exit Try
                End If
            Next

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            trnTransaction.Rollback()
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing AndAlso Not conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Modify(ByVal objKpiGroupInfo As KpiGroupInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            Dim iCounter As Integer = 0
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpigroup_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiGroupID)
            cmdCommand.Parameters.AddWithValue("@TargetTypeID", objKpiGroupInfo.TargetTypeID)
            cmdCommand.Parameters.AddWithValue("@Description", objKpiGroupInfo.Description)
            cmdCommand.Parameters.AddWithValue("@PeriodType", CType(objKpiGroupInfo.PeriodType, Decimal))
            cmdCommand.Parameters.AddWithValue("@ForAccntGrp", objKpiGroupInfo.ForAccntGrp)
            cmdCommand.Parameters.AddWithValue("@ForAccountId", objKpiGroupInfo.ForAccountId)
            cmdCommand.Parameters.AddWithValue("@ForProductgrp", objKpiGroupInfo.ForProductgrp)
            cmdCommand.Parameters.AddWithValue("@ForProductID", objKpiGroupInfo.ForProductID)
            cmdCommand.Parameters.AddWithValue("@LongDescription", objKpiGroupInfo.LongDescription)
            cmdCommand.Parameters.AddWithValue("@HideByDefault", objKpiGroupInfo.HideByDefault)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            cmdCommand.Transaction = trnTransaction

            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
                Exit Try
            End If

            'Update into Kpiversions
            If Not objKpiGroupInfo.KpiVersions Is Nothing Then
                For iCounter = 0 To objKpiGroupInfo.KpiVersions.GetUpperBound(0)
                    If objKpiGroupInfo.KpiVersions(iCounter) Is Nothing Then
                        Continue For
                    End If
                    cmdCommand.CommandText = "usp_kpiversion_modify2"
                    cmdCommand.Parameters.Clear()
                    cmdCommand.Parameters.AddWithValue("@SupplierID", objKpiGroupInfo.KpiVersions(iCounter).SupplierID)
                    cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiGroupID)
                    cmdCommand.Parameters.AddWithValue("@VersionID", objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                    cmdCommand.Parameters.AddWithValue("@Description", objKpiGroupInfo.KpiVersions(iCounter).Description)

                    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                    oReturnParam.Direction = ParameterDirection.ReturnValue
                    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                    If intResult <> 0 Then
                        ReDim objResponse.Errors(0)
                        objResponse.Errors(0) = String.Format("Failed to modify KpiVersion: {0}", objKpiGroupInfo.KpiVersions(iCounter).VersionID)
                        Exit Try
                    End If
                Next
            End If

            'Insert into KpiTypes table
            If Not objKpiGroupInfo.KpiTypes Is Nothing Then
                For iCounter = 0 To objKpiGroupInfo.KpiTypes.GetUpperBound(0)
                    If objKpiGroupInfo.KpiTypes(iCounter) Is Nothing Then
                        Continue For
                    End If
                    cmdCommand.CommandText = "usp_kpitype_modify"
                    cmdCommand.Parameters.Clear()
                    cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.KpiTypes(iCounter).SupplierId)
                    cmdCommand.Parameters.AddWithValue("@KpiGroupID", objKpiGroupInfo.KpiTypes(iCounter).KpiGroupID)
                    cmdCommand.Parameters.AddWithValue("@KpiTypeID", objKpiGroupInfo.KpiTypes(iCounter).KpiTypeID)
                    cmdCommand.Parameters.AddWithValue("@FieldType", objKpiGroupInfo.KpiTypes(iCounter).FieldType)
                    cmdCommand.Parameters.AddWithValue("@Size", objKpiGroupInfo.KpiTypes(iCounter).Size)
                    cmdCommand.Parameters.AddWithValue("@SortOrder", objKpiGroupInfo.KpiTypes(iCounter).SortOrder)
                    cmdCommand.Parameters.AddWithValue("@Label", objKpiGroupInfo.KpiTypes(iCounter).Label)
                    cmdCommand.Parameters.AddWithValue("@DefaultData", objKpiGroupInfo.KpiTypes(iCounter).DefaultData)

                    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                    oReturnParam.Direction = ParameterDirection.ReturnValue
                    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                    If intResult <> 0 Then
                        objResponse.Status = False
                        ReDim objResponse.Errors(0)
                        objResponse.Errors(0) = String.Format("Failed to modify KpiType: {0}", objKpiGroupInfo.KpiTypes(iCounter).KpiTypeID)
                        Exit Try
                    End If
                Next
            End If

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            trnTransaction.Rollback()
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing AndAlso Not conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal objKpiGroupInfo As KpiGroupInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_kpigroup_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objKpiGroupInfo.SupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", objKpiGroupInfo.KpiGroupID)
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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strKpiGroupId As String) As KpiGroupReadSingleResponse
        Dim objResponse As New KpiGroupReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_kpigroup_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@KpiGroupId", strKpiGroupId)
            Dim objKpiGroups As KpiGroupInfo() = Nothing
            objKpiGroups = ReadKpiGroups(objDBHelper.ExecuteReader(cmdCommand))

            objResponse.Status = True
            If Not objKpiGroups Is Nothing AndAlso objKpiGroups.GetUpperBound(0) >= 0 Then
                objResponse.KpiGroup = objKpiGroups(0)
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
   Public Function ReadList(ByVal strSupplierId As String) As KpiGroupReadListResponse
        Dim objResponse As New KpiGroupReadListResponse
        Try
            Dim objKpiGroupInfo As KpiGroupInfo()
            Dim cmdCommand As New SqlCommand("usp_kpigroup_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objKpiGroupInfo = ReadKpiGroups(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiGroupInfo Is Nothing AndAlso objKpiGroupInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiGroups = objKpiGroupInfo
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
  Public Function ReadList2(ByVal strSupplierId As String, ByVal strUserId As String) As KpiGroupReadListResponse
        Dim objResponse As New KpiGroupReadListResponse
        Try
            Dim objKpiGroupInfo As KpiGroupInfo()
            Dim cmdCommand As New SqlCommand("usp_kpigroup_readlistforuser")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objKpiGroupInfo = ReadKpiGroups(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiGroupInfo Is Nothing AndAlso objKpiGroupInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiGroups = objKpiGroupInfo
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

    ' <WebMethod()> _
    'Public Function Sync(ByVal strSupplierId As String, ByVal dtLastUpdtTime As DateTime) As KpiGroupReadListResponse
    '     Dim objResponse As New KpiGroupReadListResponse
    '     Try
    '         Dim objKpiGroupInfo As KpiGroupInfo()
    '         Dim cmdCommand As New SqlCommand("usp_kpigroup_sync")
    '         cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '         cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '         objKpiGroupInfo = ReadKpiGroups(objDBHelper.ExecuteReader(cmdCommand))
    '         If Not objKpiGroupInfo Is Nothing AndAlso objKpiGroupInfo.GetUpperBound(0) >= 0 Then
    '             objResponse.Status = True
    '             objResponse.KpiGroups = objKpiGroupInfo
    '         Else
    '             objResponse.Status = False
    '             ReDim objResponse.Errors(0)
    '             objResponse.Errors(0) = String.Format("No records retrieved for Supplier ID: {0} later than Date: {1}", strSupplierId, dtLastUpdtTime)
    '         End If
    '     Catch ex As Exception
    '         objResponse.Status = False
    '         Dim intCounter As Integer = 0
    '         While Not ex Is Nothing
    '             ReDim Preserve objResponse.Errors(intCounter)
    '             objResponse.Errors(intCounter) = ex.Message
    '             ex = ex.InnerException
    '         End While
    '     End Try
    '     Return objResponse
    ' End Function

    <WebMethod()> _
   Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As KpiGroupReadListResponse
        Dim objResponse As New KpiGroupReadListResponse
        Try
            Dim objKpiGroupInfo As KpiGroupInfo()
            Dim cmdCommand As New SqlCommand("usp_kpigroup_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objKpiGroupInfo = ReadKpiGroups(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objKpiGroupInfo Is Nothing AndAlso objKpiGroupInfo.GetUpperBound(0) >= 0 Then
                objResponse.KpiGroups = objKpiGroupInfo
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
   Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstKpiGroups As List(Of KpiGroupInfo)) As KpiGroupSync3Response
        Dim objResponse As New KpiGroupSync3Response
        Dim objTempResponse As New KpiGroupReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstKpiGroups Is Nothing Then
                For Each objKpiGroup As KpiGroupInfo In lstKpiGroups
                    If Not objKpiGroup Is Nothing Then
                        ProcessResponse(Modify(objKpiGroup), objTempResponse)
                    End If
                Next
            End If

            objResponse.KpiGroups = objTempResponse.KpiGroups
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.KpiGroups)
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

    Private Function ReadKpiGroups(ByVal objReader As SqlDataReader) As KpiGroupInfo()
        Dim objKpiGroups As KpiGroupInfo() = Nothing
        Dim intCounter As Integer = 0
        Dim objHash As New Hashtable
        Dim strKpiGroupId As String = String.Empty
        Dim objTempKpiGroup As KpiGroupInfo = Nothing

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objKpiGroups(intCounter)
                    objKpiGroups(intCounter) = New KpiGroupInfo
                    With objKpiGroups(intCounter)
                        .Description = CheckString(objReader("Description"))
                        .PeriodType = CType(CheckDecimal(objReader("PeriodType")), Integer)
                        .ForAccntGrp = CheckString(objReader("ForAccntGrp"))
                        .ForAccountId = CheckString(objReader("ForAccountId"))
                        .ForProductgrp = CheckString(objReader("ForProductgrp"))
                        .ForProductID = CheckString(objReader("ForProductID"))
                        .KpiGroupID = CheckString(objReader("KpiGroupID"))
                        .LongDescription = CheckString(objReader("LongDescription"))
                        .SupplierId = CheckString(objReader("SupplierID"))
                        .TargetTypeID = CheckString(objReader("TargetTypeID"))
                        .HideByDefault = CheckBoolean(objReader("HideByDefault"))
                        .Deleted = CheckDeletedField(objReader)
                        objHash.Add(String.Format("{0}{1}", Trim(.SupplierId), Trim(.KpiGroupID)), intCounter)

                    End With
                    intCounter = intCounter + 1
                End While

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strKpiGroupId = String.Format("{0}{1}", Trim(CheckString(objReader("SupplierID"))), Trim(CheckString(objReader("KpiGroupID"))))
                        If objHash.ContainsKey(strKpiGroupId) Then
                            objTempKpiGroup = objKpiGroups(CInt(objHash(strKpiGroupId)))
                            If objTempKpiGroup.KpiVersions Is Nothing OrElse objTempKpiGroup.KpiVersions.Length = 0 Then
                                ReDim objTempKpiGroup.KpiVersions(0)
                            Else
                                ReDim Preserve objTempKpiGroup.KpiVersions(objTempKpiGroup.KpiVersions.Length)
                            End If
                            objTempKpiGroup.KpiVersions(objTempKpiGroup.KpiVersions.GetUpperBound(0)) = New KpiVersionInfo
                            With objTempKpiGroup.KpiVersions(objTempKpiGroup.KpiVersions.GetUpperBound(0))
                                .SupplierID = CheckString(objReader("SupplierID"))
                                .VersionID = CheckString(objReader("VersionID"))
                                .Description = CheckString(objReader("Description"))
                                .Deleted = CheckDeletedField(objReader)
                            End With
                        End If
                    End While
                End If

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strKpiGroupId = String.Format("{0}{1}", Trim(CheckString(objReader("SupplierID"))), Trim(CheckString(objReader("KpiGroupID"))))
                        If objHash.ContainsKey(strKpiGroupId) Then
                            objTempKpiGroup = objKpiGroups(CInt(objHash(strKpiGroupId)))
                            If objTempKpiGroup.KpiTypes Is Nothing OrElse objTempKpiGroup.KpiTypes.Length = 0 Then
                                ReDim objTempKpiGroup.KpiTypes(0)
                            Else
                                ReDim Preserve objTempKpiGroup.KpiTypes(objTempKpiGroup.KpiTypes.Length)
                            End If
                            objTempKpiGroup.KpiTypes(objTempKpiGroup.KpiTypes.GetUpperBound(0)) = New KpiTypeInfo
                            With objTempKpiGroup.KpiTypes(objTempKpiGroup.KpiTypes.GetUpperBound(0))
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
                        End If
                    End While
                End If
            End If
        Finally
            objReader.Close()
        End Try
        Return objKpiGroups
    End Function

    

End Class