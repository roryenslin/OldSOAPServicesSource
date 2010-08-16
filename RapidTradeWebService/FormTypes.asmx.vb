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
Public Class FormTypes
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objFormTypeInfo As FormTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            Dim iCounter As Integer = 0
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_formtype_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objFormTypeInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@FormName", objFormTypeInfo.FormName)
            cmdCommand.Parameters.AddWithValue("@ForAccountID", objFormTypeInfo.ForAccountID)
            cmdCommand.Parameters.AddWithValue("@ForAccountGrp", objFormTypeInfo.ForAccountGrp)
            cmdCommand.Parameters.AddWithValue("@MustSelectAccount", objFormTypeInfo.MustSelectAccount)

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

            'Update into FormFields
            If Not objFormTypeInfo.FormFields Is Nothing Then
                For iCounter = 0 To objFormTypeInfo.FormFields.GetUpperBound(0)
                    If objFormTypeInfo.FormFields(iCounter) Is Nothing Then
                        Continue For
                    End If
                    cmdCommand.CommandText = "usp_formfield_modify"
                    cmdCommand.Parameters.Clear()
                    cmdCommand.Parameters.AddWithValue("@SupplierID", objFormTypeInfo.FormFields(iCounter).SupplierID)
                    cmdCommand.Parameters.AddWithValue("@FormTypeID", objFormTypeInfo.FormFields(iCounter).FormTypeID)
                    cmdCommand.Parameters.AddWithValue("@FormGroup", objFormTypeInfo.FormFields(iCounter).FormGroup)
                    cmdCommand.Parameters.AddWithValue("@SortOrder", objFormTypeInfo.FormFields(iCounter).SortOrder)
                    cmdCommand.Parameters.AddWithValue("@Label", objFormTypeInfo.FormFields(iCounter).Label)
                    cmdCommand.Parameters.AddWithValue("@DefaultData", objFormTypeInfo.FormFields(iCounter).DefaultData)
                    cmdCommand.Parameters.AddWithValue("@DefaultSQL", objFormTypeInfo.FormFields(iCounter).DefaultSQL)
                    cmdCommand.Parameters.AddWithValue("@FieldType", objFormTypeInfo.FormFields(iCounter).FieldType)

                    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                    oReturnParam.Direction = ParameterDirection.ReturnValue
                    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                    If intResult <> 0 Then
                        ReDim objResponse.Errors(0)
                        objResponse.Errors(0) = String.Format("Failed to modify Formfield: {0}", objFormTypeInfo.FormFields(iCounter).FormFieldID)
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
    Public Function Delete(ByVal objFormTypeInfo As FormTypeInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_formtype_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierId", objFormTypeInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", objFormTypeInfo.FormTypeID)
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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal intFormTypeID As Integer) As FormTypeReadSingleResponse
        Dim objResponse As New FormTypeReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_formtype_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@FormTypeID", intFormTypeID)
            Dim objFormTypes As FormTypeInfo() = Nothing
            objFormTypes = ReadFormTypes(objDBHelper.ExecuteReader(cmdCommand))

            objResponse.Status = True
            If Not objFormTypes Is Nothing AndAlso objFormTypes.GetUpperBound(0) >= 0 Then
                objResponse.FormType = objFormTypes(0)
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
   Public Function ReadList(ByVal strSupplierId As String) As FormTypeReadListResponse
        Dim objResponse As New FormTypeReadListResponse
        Try
            Dim objFormTypeInfo As FormTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_formtype_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objFormTypeInfo = ReadFormTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormTypeInfo Is Nothing AndAlso objFormTypeInfo.GetUpperBound(0) >= 0 Then
                objResponse.FormTypes = objFormTypeInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As FormTypeReadListResponse
        Dim objResponse As New FormTypeReadListResponse
        Try
            Dim objFormTypeFieldInfo As FormTypeInfo()
            Dim cmdCommand As New SqlCommand("usp_formtype_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objFormTypeFieldInfo = ReadFormTypes(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objFormTypeFieldInfo Is Nothing AndAlso objFormTypeFieldInfo.GetUpperBound(0) >= 0 Then
                objResponse.FormTypes = objFormTypeFieldInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstFormTypes As List(Of FormTypeInfo)) As FormTypeSync3Response
        Dim objResponse As New FormTypeSync3Response
        Dim objTempResponse As New FormTypeReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstFormTypes Is Nothing Then
                For Each objFormtype As FormTypeInfo In lstFormTypes
                    If Not objFormtype Is Nothing Then
                        ProcessResponse(Modify(objFormtype), objTempResponse)
                    End If
                Next
            End If

            objResponse.FormTypes = objTempResponse.FormTypes
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.FormType)
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

    Private Function ReadFormTypes(ByVal objReader As SqlDataReader) As FormTypeInfo()
        Dim objFormTypes As FormTypeInfo() = Nothing
        Dim intCounter As Integer = 0
        Dim objHash As New Hashtable
        Dim strFormTypeKey As String
        Dim objTempFormType As FormTypeInfo = Nothing

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objFormTypes(intCounter)
                    objFormTypes(intCounter) = New FormTypeInfo
                    With objFormTypes(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .FormTypeID = CheckInteger(objReader("FormTypeID"))
                        .FormName = CheckString(objReader("FormName"))
                        .ForAccountID = CheckString(objReader("ForAccountID"))
                        .ForAccountGrp = CheckString(objReader("ForAccountGrp"))
                        .MustSelectAccount = CheckBoolean(objReader("MustSelectAccount"))
                        .Deleted = CheckDeletedField(objReader)
                        objHash.Add(String.Format("{0}{1}", Trim(.SupplierID), .FormTypeID), intCounter)
                    End With
                    intCounter = intCounter + 1
                End While

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strFormTypeKey = String.Format("{0}{1}", Trim(CheckString(objReader("SupplierID"))), Trim(CheckString(objReader("FormTypeID"))))
                        If objHash.ContainsKey(strFormTypeKey) Then
                            objTempFormType = objFormTypes(CInt(objHash(strFormTypeKey)))
                            If objTempFormType.FormFields Is Nothing OrElse objTempFormType.FormFields.Length = 0 Then
                                ReDim objTempFormType.FormFields(0)
                            Else
                                ReDim Preserve objTempFormType.FormFields(objTempFormType.FormFields.Length)
                            End If
                            objTempFormType.FormFields(objTempFormType.FormFields.GetUpperBound(0)) = New FormFieldInfo
                            With objTempFormType.FormFields(objTempFormType.FormFields.GetUpperBound(0))
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
                        End If
                    End While
                End If
            End If
        Finally
            objReader.Close()
        End Try
        Return objFormTypes
    End Function
End Class