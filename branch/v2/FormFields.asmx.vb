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
Public Class FormFields
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objFormFieldInfo As FormFieldInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_formfield_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objFormFieldInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", objFormFieldInfo.FormTypeID)
            cmdCommand.Parameters.AddWithValue("@FormGroup", objFormFieldInfo.FormGroup)
            cmdCommand.Parameters.AddWithValue("@SortOrder", objFormFieldInfo.SortOrder)
            cmdCommand.Parameters.AddWithValue("@Label", objFormFieldInfo.Label)
            cmdCommand.Parameters.AddWithValue("@DefaultData", objFormFieldInfo.DefaultData)
            cmdCommand.Parameters.AddWithValue("@DefaultSQL", objFormFieldInfo.DefaultSQL)
            cmdCommand.Parameters.AddWithValue("@FieldType", objFormFieldInfo.FieldType)

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
    Public Function Delete(ByVal objFormFieldInfo As FormFieldInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_formfield_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objFormFieldInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", objFormFieldInfo.FormTypeID)
            cmdCommand.Parameters.AddWithValue("@FormFieldID", objFormFieldInfo.FormFieldID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal intFormTypeId As Integer, ByVal intFormFieldId As Integer) As FormFieldReadSingleResponse
        Dim objResponse As New FormFieldReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_formfield_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", intFormTypeId)
            cmdCommand.Parameters.AddWithValue("@FormFieldID", intFormFieldId)
            Dim objFormFields As FormFieldInfo() = Nothing
            objFormFields = ReadFormFields(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormFields Is Nothing AndAlso objFormFields.GetUpperBound(0) >= 0 Then
                objResponse.FormField = objFormFields(0)
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal intFormTypeId As Integer) As FormFieldsReadListResponse
        Dim objResponse As New FormFieldsReadListResponse
        Try
            Dim objFormFieldInfo As FormFieldInfo()
            Dim cmdCommand As New SqlCommand("usp_formfield_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", intFormTypeId)
            objFormFieldInfo = ReadFormFields(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormFieldInfo Is Nothing AndAlso objFormFieldInfo.GetUpperBound(0) >= 0 Then
                objResponse.FormFields = objFormFieldInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As FormFieldsReadListResponse
        Dim objResponse As New FormFieldsReadListResponse
        Try
            Dim objFormFieldInfo As FormFieldInfo()
            Dim cmdCommand As New SqlCommand("usp_formfield_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objFormFieldInfo = ReadFormFields(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormFieldInfo Is Nothing AndAlso objFormFieldInfo.GetUpperBound(0) >= 0 Then
                objResponse.FormFields = objFormFieldInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstFormFields As List(Of FormFieldInfo)) As FormFieldsSync3Response
        Dim objResponse As New FormFieldsSync3Response
        Dim objTempResponse As New FormFieldsReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstFormFields Is Nothing Then
                For Each objFormField As FormFieldInfo In lstFormFields
                    If Not objFormField Is Nothing Then
                        ProcessResponse(Modify(objFormField), objTempResponse)
                    End If
                Next
            End If

            objResponse.FormFields = objTempResponse.FormFields
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.FormField)
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

    Private Function ReadFormFields(ByVal objReader As SqlDataReader) As FormFieldInfo()
        Dim objFormFields As FormFieldInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objFormFields(intCounter)
                    objFormFields(intCounter) = New FormFieldInfo
                    With objFormFields(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .FormTypeID = CheckInteger(objReader("FormTypeID"))
                        .FormFieldID = CheckInteger(objReader("FormFieldID"))
                        .FormGroup = CheckString(objReader("FormGroup"))
                        .SortOrder = CheckInteger(objReader("SortOrder"))
                        .Label = CheckString(objReader("Label"))
                        .DefaultData = CheckString(objReader("DefaultData"))
                        .DefaultSQL = CheckString(objReader("DefaultSQL"))
                        .FieldType = CheckInteger(objReader("FieldType"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objFormFields
    End Function
End Class