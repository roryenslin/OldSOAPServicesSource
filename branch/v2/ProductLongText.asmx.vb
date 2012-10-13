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
Public Class ProductLongText
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ProductLongText))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub
    <WebMethod()> _
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstProducts As List(Of ProductLongTextInfo)) As BaseResponse
        Dim br As New BaseResponse()
        br.Status = True
        Try

            If Not lstProducts Is Nothing Then
                ModifyAll(lstProducts)
                'For Each objAccount As ProductLongTextInfo In lstProducts
                'If Not objAccount Is Nothing Then
                'Modify(lstProducts)
                'End If
                'Next
            End If

        Catch ex As Exception
            br.Status = False
            Dim errors(0) As String
            errors(0) = ex.Message
            br.Errors = errors
        End Try
        Return br
    End Function

    <WebMethod()> _
        Public Function Modify(ByVal objProductLongTextInfo As ProductLongTextInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Dim gotopoint As Integer

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction

            Dim cmdCommand As New SqlCommand("usp_productlongtext_modify", conConnection)
            cmdCommand.Transaction = trnTransaction
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductLongTextInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductLongTextInfo.ProductID)
            cmdCommand.Parameters.AddWithValue("@TabID", objProductLongTextInfo.TabID)
            cmdCommand.Parameters.AddWithValue("@LongText", objProductLongTextInfo.LongText)
            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error at " & gotopoint, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing And conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function
    <WebMethod()> _
     Public Function ModifyAll(ByVal lstProductLongTextInfo As List(Of ProductLongTextInfo)) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Dim gotopoint As Integer

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction

            For Each objProductLongTextInfo In lstProductLongTextInfo
                gotopoint += 1
                Dim cmdCommand As New SqlCommand("usp_productlongtext_modify", conConnection)
                cmdCommand.Transaction = trnTransaction
                cmdCommand.Parameters.AddWithValue("@SupplierID", objProductLongTextInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@ProductID", objProductLongTextInfo.ProductID)
                cmdCommand.Parameters.AddWithValue("@TabID", objProductLongTextInfo.TabID)
                cmdCommand.Parameters.AddWithValue("@LongText", objProductLongTextInfo.LongText)
                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            Next
            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error at " & gotopoint, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing And conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
        Public Function ReadList(ByVal strSupplierId As String, ByVal strProductId As String) As ProductLongTextReadListResponse
        Dim objResponse As New ProductLongTextReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objProductLongTextsInfo As ProductLongTextInfo()
            Dim cmdCommand As New SqlCommand("usp_ProductLongText_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ProductID", strProductId)

            objProductLongTextsInfo = ReadProductLongTexts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductLongTextsInfo Is Nothing AndAlso objProductLongTextsInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductLongTexts = objProductLongTextsInfo
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
     Public Function ReadSingle(ByVal strSupplierId As String, ByVal strProductId As String, ByVal intTabID As Integer) As ProductLongTextReadSingleResponse
        Dim objResponse As New ProductLongTextReadSingleResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objProductLongTextsInfo As ProductLongTextInfo()
            Dim cmdCommand As New SqlCommand("usp_ProductLongText_readSingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ProductID", strProductId)
            cmdCommand.Parameters.AddWithValue("@TabID", intTabID)

            objProductLongTextsInfo = ReadProductLongTexts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductLongTextsInfo Is Nothing AndAlso objProductLongTextsInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductLongText = objProductLongTextsInfo(0)
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

    Private Function ReadProductLongTexts(ByVal objReader As SqlDataReader) As ProductLongTextInfo()
        Dim objProductLongTexts As ProductLongTextInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objProductLongTexts(intCounter)
                    objProductLongTexts(intCounter) = New ProductLongTextInfo
                    With objProductLongTexts(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .TabID = CheckInteger(objReader("TabID"))
                        .LongText = CheckString(objReader("LongText"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objProductLongTexts
    End Function

    <WebMethod()> _
    Public Function TEST(ByVal strSupplierId As String, ByVal strProductId As String, ByVal intTabID As Integer, ByVal longText As String) As List(Of Object)
        'If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim longtextinfo As New ProductLongTextInfo(strSupplierId, strProductId, intTabID, longText)
        Dim lst As New List(Of ProductLongTextInfo)
        lst.Add(longtextinfo)

        Dim rslt As New List(Of Object)
        Dim br As BaseResponse = ModifyAll(lst)
        rslt.Add(br)
        Dim br2 As ProductLongTextReadSingleResponse = ReadSingle(strSupplierId, strProductId, intTabID)
        rslt.Add(br2)
        Dim br3 As ProductLongTextReadListResponse = ReadList(strSupplierId, strProductId)
        rslt.Add(br3)
        Return rslt
    End Function

End Class