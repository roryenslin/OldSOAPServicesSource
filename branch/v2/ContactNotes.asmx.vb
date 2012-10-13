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
Public Class ContactNotes
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objContactNotesInfo As ContactNotesInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_contactnotes_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objContactNotesInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objContactNotesInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ContactID", objContactNotesInfo.ContactID)
            cmdCommand.Parameters.AddWithValue("@NotesID", objContactNotesInfo.NotesID)
            cmdCommand.Parameters.AddWithValue("@Note", objContactNotesInfo.Notes)

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
    Public Function Delete(ByVal objContactNotesInfo As ContactNotesInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_contactnotes_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objContactNotesInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objContactNotesInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@ContactID", objContactNotesInfo.ContactID)
            cmdCommand.Parameters.AddWithValue("@NotesID", objContactNotesInfo.NotesID)

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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strAccountId As String) As ContactNotesReadListResponse
        Dim objResponse As New ContactNotesReadListResponse
        Try
            Dim objContactNotesInfo As ContactNotesInfo()
            Dim cmdCommand As New SqlCommand("usp_contactnotes_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountId)

            objContactNotesInfo = ReadContactNotess(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objContactNotesInfo Is Nothing AndAlso objContactNotesInfo.GetUpperBound(0) >= 0 Then
                objResponse.ContactNotes = objContactNotesInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ContactNotesReadListResponse
        Dim objResponse As New ContactNotesReadListResponse
        Try
            Dim objContactNotesInfo As ContactNotesInfo()
            Dim cmdCommand As New SqlCommand("usp_contactnotes_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version ", intVersion)

            objContactNotesInfo = ReadContactNotess(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objContactNotesInfo Is Nothing AndAlso objContactNotesInfo.GetUpperBound(0) >= 0 Then
                objResponse.ContactNotes = objContactNotesInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstContactNotes As List(Of ContactNotesInfo)) As ContactNotesSync3Response
        Dim objResponse As New ContactNotesSync3Response
        Dim objTempResponse As New ContactNotesReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstContactNotes Is Nothing Then
                For Each objContactNote As ContactNotesInfo In lstContactNotes
                    If Not objContactNote Is Nothing Then
                        ProcessResponse(Modify(objContactNote), objTempResponse)
                    End If
                Next
            End If

            objResponse.ContactNotes = objTempResponse.ContactNotes
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.ContactNotes)
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

    Private Function ReadContactNotess(ByVal objReader As SqlDataReader) As ContactNotesInfo()
        Dim objContactNotess As ContactNotesInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objContactNotess(intCounter)
                    objContactNotess(intCounter) = New ContactNotesInfo
                    With objContactNotess(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .ContactID = CheckString(objReader("ContactID"))
                        .NotesID = CheckString(objReader("NotesID"))
                        .Notes = CheckString(objReader("Note"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objContactNotess
    End Function

End Class