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
Public Class Forms
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objFormInfo As FormInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_form_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objFormInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@FormID", objFormInfo.FormID)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", objFormInfo.FormTypeID)
            cmdCommand.Parameters.AddWithValue("@FormFieldID", objFormInfo.FormFieldID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objFormInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@Date", objFormInfo.FormDate)
            cmdCommand.Parameters.AddWithValue("@Data", objFormInfo.Data)

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
    Public Function Delete(ByVal objFormInfo As FormInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_form_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objFormInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@FormID", objFormInfo.FormID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal strFormId As String) As FormReadSingleResponse
        Dim objResponse As New FormReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_form_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@FormID", strFormId)
            Dim objForms As FormInfo() = Nothing
            objForms = ReadForms(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objForms Is Nothing AndAlso objForms.GetUpperBound(0) >= 0 Then
                objResponse.Form = objForms(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As FormsReadListResponse
        Dim objResponse As New FormsReadListResponse
        Try
            Dim objFormInfo As FormInfo()
            Dim cmdCommand As New SqlCommand("usp_form_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            objFormInfo = ReadForms(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormInfo Is Nothing AndAlso objFormInfo.GetUpperBound(0) >= 0 Then
                objResponse.Forms = objFormInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As FormsReadListResponse
        Dim objResponse As New FormsReadListResponse
        Try
            Dim objFormFieldInfo As FormInfo()
            Dim cmdCommand As New SqlCommand("usp_form_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objFormFieldInfo = ReadForms(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormFieldInfo Is Nothing AndAlso objFormFieldInfo.GetUpperBound(0) >= 0 Then
                objResponse.Forms = objFormFieldInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstForms As List(Of FormInfo)) As FormsSync3Response
        Dim objResponse As New FormsSync3Response
        Dim objTempResponse As New FormsReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstForms Is Nothing Then
                For Each objForm As FormInfo In lstForms
                    If Not objForm Is Nothing Then
                        ProcessResponse(Modify(objForm), objTempResponse)
                    End If
                Next
            End If

            objResponse.Forms = objTempResponse.Forms
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Form)
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

    Private Function ReadForms(ByVal objReader As SqlDataReader) As FormInfo()
        Dim objForms As FormInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objForms(intCounter)
                    objForms(intCounter) = New FormInfo
                    With objForms(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .FormID = CheckString(objReader("FormID"))
                        .FormTypeID = CheckInteger(objReader("FormTypeID"))
                        .FormFieldID = CheckInteger(objReader("FormFieldID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .Data = CheckString(objReader("Data"))
                        .Deleted = CheckDeletedField(objReader)
                        If Not objReader("Date") Is Nothing AndAlso Not IsDBNull(objReader("Date")) Then
                            .FormDate = CType(objReader("Date"), DateTime)
                        End If
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objForms
    End Function
End Class