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
Public Class ProductStats
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objProductStatInfo As ProductStatInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_productstat_add2")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductStatInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objProductStatInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductStatInfo.ProductID)
            cmdCommand.Parameters.AddWithValue("@Year", objProductStatInfo.Year)
            cmdCommand.Parameters.AddWithValue("@Month", objProductStatInfo.Month)
            cmdCommand.Parameters.AddWithValue("@Day", objProductStatInfo.Day)
            cmdCommand.Parameters.AddWithValue("@Value", objProductStatInfo.Value)
            cmdCommand.Parameters.AddWithValue("@Value2", objProductStatInfo.Value2)
            cmdCommand.Parameters.AddWithValue("@Value3", objProductStatInfo.Value3)
            cmdCommand.Parameters.AddWithValue("@Value4", objProductStatInfo.Value4)
            cmdCommand.Parameters.AddWithValue("@Value5", objProductStatInfo.Value5)
            cmdCommand.Parameters.AddWithValue("@Userfield1", objProductStatInfo.UserField1)
            cmdCommand.Parameters.AddWithValue("@Userfield2", objProductStatInfo.UserField2)
            cmdCommand.Parameters.AddWithValue("@Userfield3", objProductStatInfo.UserField3)
            cmdCommand.Parameters.AddWithValue("@Userfield4", objProductStatInfo.UserField4)
            cmdCommand.Parameters.AddWithValue("@Userfield5", objProductStatInfo.UserField5)

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
    Public Function Change(ByVal objProductStatInfo As ProductStatInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_productstat_change")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductStatInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objProductStatInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductStatInfo.ProductID)
            cmdCommand.Parameters.AddWithValue("@Year", objProductStatInfo.Year)
            cmdCommand.Parameters.AddWithValue("@Month", objProductStatInfo.Month)
            cmdCommand.Parameters.AddWithValue("@Day", objProductStatInfo.Day)
            cmdCommand.Parameters.AddWithValue("@Value", objProductStatInfo.Value)
            cmdCommand.Parameters.AddWithValue("@Value2", objProductStatInfo.Value2)
            cmdCommand.Parameters.AddWithValue("@Value3", objProductStatInfo.Value3)
            cmdCommand.Parameters.AddWithValue("@Value4", objProductStatInfo.Value4)
            cmdCommand.Parameters.AddWithValue("@Value5", objProductStatInfo.Value5)
            cmdCommand.Parameters.AddWithValue("@Userfield1", objProductStatInfo.UserField1)
            cmdCommand.Parameters.AddWithValue("@Userfield2", objProductStatInfo.UserField2)
            cmdCommand.Parameters.AddWithValue("@Userfield3", objProductStatInfo.UserField3)
            cmdCommand.Parameters.AddWithValue("@Userfield4", objProductStatInfo.UserField4)
            cmdCommand.Parameters.AddWithValue("@Userfield5", objProductStatInfo.UserField5)

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
    Public Function Modify(ByVal objProductStatInfo As ProductStatInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_productstat_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductStatInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objProductStatInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductStatInfo.ProductID)
            cmdCommand.Parameters.AddWithValue("@Year", objProductStatInfo.Year)
            cmdCommand.Parameters.AddWithValue("@Month", objProductStatInfo.Month)
            cmdCommand.Parameters.AddWithValue("@Day", objProductStatInfo.Day)
            cmdCommand.Parameters.AddWithValue("@Value", objProductStatInfo.Value)
            cmdCommand.Parameters.AddWithValue("@Value2", objProductStatInfo.Value2)
            cmdCommand.Parameters.AddWithValue("@Value3", objProductStatInfo.Value3)
            cmdCommand.Parameters.AddWithValue("@Value4", objProductStatInfo.Value4)
            cmdCommand.Parameters.AddWithValue("@Value5", objProductStatInfo.Value5)
            cmdCommand.Parameters.AddWithValue("@Userfield1", objProductStatInfo.UserField1)
            cmdCommand.Parameters.AddWithValue("@Userfield2", objProductStatInfo.UserField2)
            cmdCommand.Parameters.AddWithValue("@Userfield3", objProductStatInfo.UserField3)
            cmdCommand.Parameters.AddWithValue("@Userfield4", objProductStatInfo.UserField4)
            cmdCommand.Parameters.AddWithValue("@Userfield5", objProductStatInfo.UserField5)

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
    Public Function Delete(ByVal objProductStatInfo As ProductStatInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_productstat_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductStatInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objProductStatInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductStatInfo.ProductID)
            cmdCommand.Parameters.AddWithValue("@Year", objProductStatInfo.Year)
            cmdCommand.Parameters.AddWithValue("@Month", objProductStatInfo.Month)
            cmdCommand.Parameters.AddWithValue("@Day", objProductStatInfo.Day)

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
   Public Function ReadSingle(ByVal strSupplierId As String, ByVal strAccountId As String, ByVal strProductId As String, _
                               ByVal strYear As String, ByVal strMonth As String, ByVal strDay As DateTime) As ProductStatReadSingleResponse
        Dim objResponse As New ProductStatReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_productstat_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)
            cmdCommand.Parameters.AddWithValue("@ProductID", strProductId)
            cmdCommand.Parameters.AddWithValue("@Year", strYear)
            cmdCommand.Parameters.AddWithValue("@Month", strMonth)
            cmdCommand.Parameters.AddWithValue("@Day", strDay)

            Dim objProductStats As ProductStatInfo() = Nothing
            objProductStats = ReadProductStats(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductStats Is Nothing AndAlso objProductStats.GetUpperBound(0) >= 0 Then
                objResponse.ProductStat = objProductStats(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strAccountId As String) As ProductStatReadListResponse
        Dim objResponse As New ProductStatReadListResponse
        Try
            Dim objProductStatInfo As ProductStatInfo()
            Dim cmdCommand As New SqlCommand("usp_productstat_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)

            objProductStatInfo = ReadProductStats(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductStatInfo Is Nothing AndAlso objProductStatInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductStats = objProductStatInfo
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
    'Public Function Sync(ByVal strSupplierId As String, ByVal strUserId As String, ByVal dtLastUpdtTime As DateTime) As ProductStatReadListResponse
    '    Dim objResponse As New ProductStatReadListResponse
    '    Try
    '        Dim objProductStatInfo As ProductStatInfo()
    '        Dim cmdCommand As New SqlCommand("usp_productstat_sync")
    '        cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
    '        cmdCommand.Parameters.AddWithValue("@userId", strUserId)
    '        cmdCommand.Parameters.AddWithValue("@LastUpdateTime", dtLastUpdtTime)
    '        objProductStatInfo = ReadProductStats(objDBHelper.ExecuteReader(cmdCommand))
    '        If Not objProductStatInfo Is Nothing AndAlso objProductStatInfo.GetUpperBound(0) >= 0 Then
    '            objResponse.Status = True
    '            objResponse.ProductStats = objProductStatInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer) As ProductStatReadListResponse
        Dim objResponse As New ProductStatReadListResponse
        Try
            Dim objProductStatInfo As ProductStatInfo()
            Dim cmdCommand As New SqlCommand("usp_productstat_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@userId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objProductStatInfo = ReadProductStats(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductStatInfo Is Nothing AndAlso objProductStatInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductStats = objProductStatInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstProductStats As List(Of ProductStatInfo)) As ProductStatSync3Response
        Dim objResponse As New ProductStatSync3Response
        Dim objTempResponse As New ProductStatReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, strUserId, intVersion)

            If Not lstProductStats Is Nothing Then
                For Each objProductStat As ProductStatInfo In lstProductStats
                    If Not objProductStat Is Nothing Then
                        ProcessResponse(Modify(objProductStat), objTempResponse)
                    End If
                Next
            End If

            objResponse.ProductStats = objTempResponse.ProductStats
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.ProductStats)
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

    Private Function ReadProductStats(ByVal objReader As SqlDataReader) As ProductStatInfo()
        Dim objProductStats As ProductStatInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objProductStats(intCounter)
                    objProductStats(intCounter) = New ProductStatInfo
                    With objProductStats(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .Year = CheckString(objReader("Year"))
                        .Month = CheckString(objReader("Month"))
                        If Not objReader("Day") Is Nothing AndAlso Not IsDBNull(objReader("Day")) Then
                            .Day = CType(objReader("Day"), DateTime)
                        End If
                        .Value = CheckDecimal(objReader("Value"))
                        .Value2 = CheckDecimal(objReader("Value2"))
                        .Value3 = CheckDecimal(objReader("Value3"))
                        .Value4 = CheckDecimal(objReader("Value4"))
                        .Value5 = CheckDecimal(objReader("Value5"))
                        .UserField1 = CheckString(objReader("Userfield1"))
                        .UserField2 = CheckString(objReader("Userfield2"))
                        .UserField3 = CheckString(objReader("Userfield3"))
                        .UserField4 = CheckString(objReader("Userfield4"))
                        .UserField5 = CheckString(objReader("Userfield5"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objProductStats
    End Function

End Class